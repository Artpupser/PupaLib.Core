namespace PupaLib.Core;

public readonly record struct Option(bool Success) {
  private static readonly Option FailOption = new(false);

  public static implicit operator bool(Option option) {
    return option.Success;
  }

  public static Option Fail() {
    return FailOption;
  }
  
  public static Option Ok() {
    return new Option(true);
  }
  
  public void Match(Action? ok = null, Action? fail = null) {
    var action = Success ? ok : fail;
    action?.Invoke();
  }
  
  public static Task<Option> OkTask() {
    return Task.FromResult(new Option(true));
  }
  
  public static ValueTask<Option> OkVTask() {
    return ValueTask.FromResult(new Option(true));
  }
}

public readonly record struct Option<T>(bool Success, T Content) {
  private static readonly Option<T> FailOption = new(false, default!);

  public static implicit operator bool(Option<T> option) {
    return option.Success;
  }

  public static Option<T> Fail() {
    return FailOption;
  }

  public static Option<T> Ok(T content) {
    var test = Option<string>.Fail();
    return new Option<T>(true, content);
  }

  public void Match(Action<T>? ok = null, Action? fail = null) {
    if(Success) {
      ok?.Invoke(Content);
      return;
    }
    fail?.Invoke();
  }
  
  public Option<TResult> Map<TResult>(Func<T, TResult> func) {
    return !this ? Option<TResult>.Fail() : Option<TResult>.Ok(func(Content));
  }
  
  public Option<TResult> Bind<TResult>(Func<T, Option<TResult>> func) {
    return !this ? Option<TResult>.Fail() : func(Content);
  }

  public (bool success, T content) Tuple() {
    return (Success, Content);
  }

  public bool Out(out T content) {
    content = Content;
    return Success;
  }
}