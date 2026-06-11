using System;
using UnityEngine;
using System.Collections.Generic;

public class StatusSystem
{
    public event Action<StatusAppliedEvent> StatusAppliedEvent;
    public event Action<StatusRefreshedEvent> StatusRefreshedEvent;
    public event Action<StatusExpiredEvent> StatusExpiredEvent;
    public event Action<StatusRemovedEvent> StatusRemovedEvent;

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

            if (ShouldExpire(status))
            {
                ExpireStatus(status);
                Remove(status);
                StatusExpiredEvent?.Invoke(new StatusExpiredEvent(status));

                Debug.Log($"Expired {status.Definition.name}");
            }
        }
    }

    public bool Remove(StatusInstance status)
    {
        if (status == null)
            return false;

        if (!activeStatuses.Remove(status))
            return false;

        //TODO: implement CleanupStatus(status)

        StatusRemovedEvent?.Invoke(new StatusRemovedEvent(status));

        Debug.Log($"Removed {status.Definition.name}");

        return true;
    }

    public bool Remove(StatusDefinition definition)
    {
        var status = FindExistingStatus(definition);
        return Remove(status);
    }

    public int RemoveAll(Predicate<StatusInstance> predicate)
    {
        int removed = 0;

        for (int i = activeStatuses.Count - 1; i >= 0; i--)
        {
            if (!predicate(activeStatuses[i]))
                continue;

            Remove(activeStatuses[i]);
            removed++;
        }

        return removed;
    }

    public void Clear()
    {
        for (int i = activeStatuses.Count - 1; i >= 0; i--)
        {
            Remove(activeStatuses[i]);
        }
    }

    public bool HasStatus(StatusDefinition definition)
    {
        return FindExistingStatus(definition) != null;
    }

    private bool ShouldExpire(StatusInstance status)
    {
        switch (status.Definition.LifetimeType)
        {
            case StatusLifetimeType.Timed:
                return status.RemainingDuration <= 0;

            case StatusLifetimeType.Conditional:
                return !AreConditionsMet(status);

            case StatusLifetimeType.Permanent:
                return false;

            default:
                return false;
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

    private bool AreConditionsMet(StatusInstance status)
    {
        foreach (var condition in status.Definition.Conditions)
        {
            if (!condition.Evaluate(status))
                return false;
        }

        return true;
    }

    private void ExpireStatus(StatusInstance status)
    {
        status.MarkAsExpired();
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