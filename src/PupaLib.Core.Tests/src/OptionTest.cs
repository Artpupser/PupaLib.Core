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
  public Task OptionTString_FailTupleResult_ReturnsTrue() {
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
  public Task OptionTString_FailOutResult_ReturnsTrue() {
    var option = Option<string>.Fail();
    Assert.False(option.Out(out var content));
    Assert.NotEqual(Message, content);
    return Task.CompletedTask;
  }

  #endregion
}