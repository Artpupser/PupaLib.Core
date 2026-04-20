namespace PupaLib.Core;

public sealed class Eventer {
  public delegate void EventerHandler(object? sender);

  private event EventerHandler? Trigger;

  public void Invoke(object? sender) {
    Trigger?.Invoke(sender);
  }

  public EventerHandler Subscribe(EventerHandler handler) {
    Trigger += handler;
    return handler;
  }
  
  public int SubscribeIndex(EventerHandler handler) {
    Trigger += handler;
    return Trigger.GetInvocationList().IndexOf(handler);
  }
  
  public void Unsubscribe(EventerHandler handler) {
    Trigger -= handler;
  }
  
  public void UnsubscribeIndex(int index) {
    if(Trigger != null) {
      Trigger -= (EventerHandler)Trigger.GetInvocationList()[index];
    }
  }
  
  public void UnsubscribeAll() {
    if(Trigger == null) return;
    foreach(var item in Trigger.GetInvocationList()) {
      Trigger -= (EventerHandler)item;
    }
  }
}

public sealed class Eventer<T> {
  public delegate void EventerHandler(object? sender, T args);

  private event EventerHandler? Trigger;

  public void Invoke(object? sender, T args) {
    Trigger?.Invoke(sender, args);
  }

  public EventerHandler Subscribe(EventerHandler handler) {
    Trigger += handler;
    return handler;
  }
  
  public int SubscribeIndex(EventerHandler handler) {
    Trigger += handler;
    return Trigger.GetInvocationList().IndexOf(handler);
  }
  
  public void Unsubscribe(EventerHandler handler) {
    Trigger -= handler;
  }
  
  public void UnsubscribeIndex(int index) {
    if(Trigger != null) {
      Trigger -= (EventerHandler)Trigger.GetInvocationList()[index];
    }
  }

  public void UnsubscribeAll() {
    if(Trigger == null) return;
    foreach(var item in Trigger.GetInvocationList()) {
      Trigger -= (EventerHandler)item;
    }
  }
}