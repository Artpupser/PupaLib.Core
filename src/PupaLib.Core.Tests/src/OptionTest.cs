namespace PupaLib.Core.Test;

[Collection("BasicTests")]
public sealed class OptionTest {
  private const string Message = "any text";

  #region Not generic

  [Fact]
  public Task Option_OkImplicitResult_ReturnsTrue() {
    Assert.True(Option.Ok());
    return Task.CompletedTask;
  }

  [Fact]
  public Task Option_FailImplicitResult_ReturnsFalse() {
    Assert.False(Option.Fail());
    return Task.CompletedTask;
  }
  
  [Fact]
  public Task Option_OkMatchResult_ReturnsTrue() {
    var matchResult = false;
    Option.Ok().Match(ok: () => {
      matchResult = true;
    }, fail: () => {
      matchResult = false;
    });
    Assert.True(matchResult);
    return Task.CompletedTask;
  }
  
  [Fact]
  public Task Option_FailMatchResult_ReturnsFalse() {
    var matchResult = false;
    Option.Fail().Match(ok: () => {
      matchResult = true;
    }, fail: () => {
      matchResult = false;
    });
    Assert.False(matchResult);
    return Task.CompletedTask;
  }

  #endregion


  #region Generic

  [Fact]
  public Task OptionTString_FailImplicitResult_ReturnsFalseAndNotEqual() {
    var option = Option<string>.Fail();
    Assert.NotEqual(Message, option.Content);
    Assert.False(Option.Fail());
    return Task.CompletedTask;
  }

  [Fact]
  public Task OptionTString_OkImplicitResult_ReturnsTrueAndEqual() {
    var option = Option<string>.Ok(Message);
    Assert.Equal(Message, option.Content);
    Assert.True(option);
    return Task.CompletedTask;
  }


  [Fact]
  public Task OptionTString_OkTupleResult_ReturnsTrue() {
    var option = Option<string>.Ok(Message);
    var (success, content) = option.Tuple();
    Assert.Equal(Message, content);
    Assert.True(success);
    return Task.CompletedTask;
  }

  [Fact]
  public Task OptionTString_FailTupleResult_ReturnsFalse() {
    var option = Option<string>.Fail();
    var (success, content) = option.Tuple();
    Assert.NotEqual(Message, content);
    Assert.False(success);
    return Task.CompletedTask;
  }

  [Fact]
  public Task OptionTString_OkOutResult_ReturnsTrue() {
    var option = Option<string>.Ok(Message);
    Assert.True(option.Out(out var content));
    Assert.Equal(Message, content);
    return Task.CompletedTask;
  }

  [Fact]
  public Task OptionTString_FailOutResult_ReturnsFalse() {
    var option = Option<string>.Fail();
    Assert.False(option.Out(out var content));
    Assert.NotEqual(Message, content);
    return Task.CompletedTask;
  }
  
  [Fact]
  public Task OptionTString_OkMatchResult_ReturnsTrue() {
    var matchResult = false;
    var matchContent = "";
    Option<string>.Ok(Message).Match(ok: (content) => {
      matchResult = content != string.Empty;
      matchContent = content;
    }, fail: () => {
      matchResult = false;
      matchContent = string.Empty;
    });
    Assert.True(matchResult);
    Assert.Equal(Message, matchContent);
    return Task.CompletedTask;
  }
  
  [Fact]
  public Task OptionTString_FailMatchResult_ReturnsFalse() {
    var matchResult = false;
    var matchContent = "";
    Option<string>.Fail().Match(ok: (content) => {
      matchResult = content != string.Empty;
      matchContent = content;
    }, fail: () => {
      matchResult = false;
      matchContent = string.Empty;
    });
    Assert.False(matchResult);
    Assert.NotEqual(Message, matchContent);
    return Task.CompletedTask;
  }
  
  [Fact]
  public Task OptionTString_OkMapResult_ReturnsTrue() {
    var option = Option<string>.Ok(Message).Map(s => s.Length);
    Assert.True(option.Success);
    Assert.Equal(Message.Length, option.Content);
    return Task.CompletedTask;
  }
  
  [Fact]
  public Task OptionTString_FailMapResult_ReturnsFalse() {
    var option = Option<string>.Fail().Map(s => s.Length);
    Assert.False(option.Success);
    Assert.NotEqual(Message.Length, option.Content);
    return Task.CompletedTask;
  }
  
  [Fact]
  public Task OptionTString_OkBindResult_ReturnsTrue() {
    var option1 = Option<string>.Ok(Message);
    Assert.True(option1.Success);
    Assert.Equal(Message, option1.Content);
    var option2 = option1.Bind(s => s.Length == Message.Length ? Option<int>.Ok(s.Length) : Option<int>.Fail());
    Assert.True(option2.Success);
    Assert.Equal(Message.Length, option2.Content);
    return Task.CompletedTask;
  }
  
  [Fact]
  public Task OptionTString_FailBindResult_ReturnsFalse() {
    var option1 = Option<string>.Fail();
    Assert.False(option1.Success);
    Assert.NotEqual(Message, option1.Content);
    var option2 = option1.Bind(s => s.Length == Message.Length ? Option<int>.Ok(s.Length) : Option<int>.Fail());
    Assert.False(option2.Success);
    Assert.NotEqual(Message.Length, option2.Content);
    return Task.CompletedTask;
  }

  #endregion
}