using System;
using UnityEngine;
using System.Collections.Generic;

public class StatusSystem
{
    public event Action<StatusAppliedEvent> StatusAppliedEvent;
    public event Action<StatusRefreshedEvent> StatusRefreshedEvent;
    public event Action<StatusExpiredEvent> StatusExpiredEvent;

    private readonly List<StatusInstance> activeStatuses = new();

    public void Apply(StatusDefinition definition, StatusApplicationContext applicationContext)
    {
        StatusInstance existing = FindExisting(definition);

        if (existing != null && definition.LifetimeType == StatusLifetimeType.Timed)
        {
            existing.Refresh(definition, applicationContext);
            StatusRefreshedEvent?.Invoke(new StatusRefreshedEvent(existing));

            Debug.Log($"Refresh {definition.name}");
            return;
        }

        StatusInstance statusInstance = new StatusInstance(definition, applicationContext);
        activeStatuses.Add(statusInstance);
        StatusAppliedEvent?.Invoke(new StatusAppliedEvent(statusInstance));

        Debug.Log($"Applied {definition.name}");
    }

    public void Tick(float deltaTime)
    {
        for (int i = activeStatuses.Count - 1; i >= 0; i--)
        {
            var status = activeStatuses[i];

            status.Tick(deltaTime);
            Debug.Log($"Ticked {status.Definition.name}");

            while (status.TickTimer >= status.Definition.TickInterval)
            {
                status.TickTimer -= status.Definition.TickInterval;

                ExecuteEffects(status);
            }

            if (status.IsExpired)
            {
                activeStatuses.RemoveAt(i);
                StatusExpiredEvent?.Invoke(new StatusExpiredEvent(status));

                Debug.Log($"Expired {status.Definition.name}");
            }
        }
    }

    private void ExecuteEffects(StatusInstance statusInstance)
    {
        foreach (var effect in statusInstance.Definition.Effects)
        {
            effect.Execute(statusInstance.ApplicationContext);
        }
    }

    private StatusInstance FindExisting(StatusDefinition definition)
    {
        return activeStatuses.Find(s => s.Definition == definition);
    }
}