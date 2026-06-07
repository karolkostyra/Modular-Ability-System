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
        StatusInstance statusInstance = FindExistingStatus(definition);

        if (statusInstance == null)
        {
            statusInstance = CreateNewStatus(definition, applicationContext);
            activeStatuses.Add(statusInstance);
            StatusAppliedEvent?.Invoke(new StatusAppliedEvent(statusInstance));

            Debug.Log($"Applied {definition.name}");
            return;
        }
        else if (!definition.CanReapply)
        {
            return;
        }
        else
        {
            ManageStatusApplicationRule(statusInstance);
        }
    }

    private void ManageStatusApplicationRule(StatusInstance statusInstance)
    {
        var context = statusInstance.ApplicationContext;

        switch (context.ApplicationRule)
        {
            case StatusApplicationRule.Refresh:
                statusInstance.RefreshDuration(context.BaseDuration);
                StatusRefreshedEvent?.Invoke(new StatusRefreshedEvent(statusInstance));
                Debug.Log($"Refresh {statusInstance.Definition.name}");
                return;

            case StatusApplicationRule.ExtendDuration:
                statusInstance.ExtendDuration(context.BaseDuration);
                StatusRefreshedEvent?.Invoke(new StatusRefreshedEvent(statusInstance));
                Debug.Log($"Extend {statusInstance.Definition.name}");
                return;

            case StatusApplicationRule.Stack:
                statusInstance.AddStack();
                statusInstance.ExtendDuration(context.BaseDuration);
                StatusRefreshedEvent?.Invoke(new StatusRefreshedEvent(statusInstance));
                Debug.Log($"Stack {statusInstance.Definition.name}");
                return;

            case StatusApplicationRule.Ignore:
                Debug.Log($"Ignore {statusInstance.Definition.name}");
                return;
        }
    }

    public void Tick(float deltaTime)
    {
        for (int i = activeStatuses.Count - 1; i >= 0; i--)
        {
            var status = activeStatuses[i];

            TickLifetime(status, deltaTime);
            TickEffects(status, deltaTime);

            if (IsExpired(status))
            {
                activeStatuses.RemoveAt(i);
                StatusExpiredEvent?.Invoke(new StatusExpiredEvent(status));

                Debug.Log($"Expired {status.Definition.name}");
            }
        }
    }

    private void TickLifetime(StatusInstance status, float dt)
    {
        switch (status.Definition.LifetimeType)
        {
            case StatusLifetimeType.Timed:
                status.ReduceDuration(dt);
                break;

            case StatusLifetimeType.Conditional:
                //TO_DO: manage conditions
                break;

            case StatusLifetimeType.Permanent:
                break;
        }
    }

    private void TickEffects(StatusInstance status, float dt)
    {
        status.TickTimer += dt;

        while (status.TickTimer >= status.Definition.TickInterval)
        {
            status.TickTimer -= status.Definition.TickInterval;
            ExecuteEffects(status);
        }
    }

    private void ExecuteEffects(StatusInstance statusInstance)
    {
        foreach (var effect in statusInstance.Definition.Effects)
        {
            effect.Execute(statusInstance.ApplicationContext);
        }
    }

    private bool IsExpired(StatusInstance status)
    {
        return status.Definition.LifetimeType == StatusLifetimeType.Timed && status.RemainingDuration <= 0;
    }

    private StatusInstance FindExistingStatus(StatusDefinition definition)
    {
        return activeStatuses.Find(s => s.Definition == definition);
    }

    private StatusInstance CreateNewStatus(StatusDefinition definition, StatusApplicationContext applicationContext)
    {
        return new StatusInstance(definition, applicationContext);
    }
}