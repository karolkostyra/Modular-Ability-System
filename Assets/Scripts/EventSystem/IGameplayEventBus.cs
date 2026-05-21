using System;

public interface IGameplayEventBus
{
    void Publish<T>(T gameplayEvent);
    void Subscribe<T>(Action<T> listener);
    void Unsubscribe<T>(Action<T> listener);
}