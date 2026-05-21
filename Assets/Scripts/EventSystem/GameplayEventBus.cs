using System;
using System.Collections.Generic;
using UnityEngine;

public class GameplayEventBus : IGameplayEventBus
{
    private readonly Dictionary<Type, Delegate> listeners = new();

    public void CheckDict()
    {
        if (listeners == null)
        {
            Debug.LogError($"is null");
            return;
        }

        if (listeners.Count == 0)
        {
            Debug.LogError($"is empty");
            return;
        }

        foreach (var item in listeners)
        {
            Debug.LogError($"key: {item.Key} value: {item.Value}");
        }
    }

    public void Publish<T>(T gameplayEvent)
    {
        if (!listeners.TryGetValue(typeof(T), out var del))
            return;

        var invocationList = del.GetInvocationList();

        foreach (var handler in invocationList)
        {
            try
            {
                ((Action<T>)handler)?.Invoke(gameplayEvent);
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
            }
        }
    }

    public void Subscribe<T>(Action<T> listener)
    {
        if (listeners.TryGetValue(typeof(T), out var del))
        {
            listeners[typeof(T)] = Delegate.Combine(del, listener);
        }
        else
        {
            listeners[typeof(T)] = listener;
        }
    }

    public void Unsubscribe<T>(Action<T> listener)
    {
        if (!listeners.TryGetValue(typeof(T), out var del))
            return;

        var current = Delegate.Remove(del, listener);

        if (current == null)
        {
            listeners.Remove(typeof(T));
        }
        else
        {
            listeners[typeof(T)] = current;
        }
    }
}