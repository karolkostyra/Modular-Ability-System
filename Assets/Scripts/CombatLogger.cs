using UnityEngine;

public class CombatLogger
{
    public CombatLogger(IGameplayEventBus bus)
    {
        bus.Subscribe<AbilityCastStartedEvent>(OnCastStarted);
        bus.Subscribe<AbilityCastInterruptedEvent>(OnCastInterrupted);
        bus.Subscribe<AbilityCastFinishedEvent>(OnCastFinished);

        bus.Subscribe<StatusAppliedEvent>(OnStatusApplied);
        bus.Subscribe<StatusRefreshedEvent>(OnStatusRefreshed);
        bus.Subscribe<StatusExpiredEvent>(OnStatusExpired);
        bus.Subscribe<StatusRemovedEvent>(OnStatusRemoved);
    }

    private void OnCastStarted(AbilityCastStartedEvent e)
    {
        Debug.Log($"[OnCastStarted] {e.Session.AbilityInstance.Definition.name}");
    }

    private void OnCastInterrupted(AbilityCastInterruptedEvent e)
    {
        Debug.Log($"[OnCastInterrupted] {e.Session.AbilityInstance.Definition.name}");
    }

    private void OnCastFinished(AbilityCastFinishedEvent e)
    {
        Debug.Log($"[OnCastFinished] {e.Session.AbilityInstance.Definition.name}");
    }

    private void OnStatusApplied(StatusAppliedEvent e)
    {
        Debug.Log($"[OnStatusApplied] {e.StatusInstance.Definition.name}");
    }

    private void OnStatusRefreshed(StatusRefreshedEvent e)
    {
        Debug.Log($"[OnStatusRefreshed] {e.StatusInstance.Definition.name}");
    }

    private void OnStatusExpired(StatusExpiredEvent e)
    {
        Debug.Log($"[OnStatusExpired] {e.StatusInstance.Definition.name}");
    }

    private void OnStatusRemoved(StatusRemovedEvent e)
    {
        Debug.Log($"[OnStatusRemoved] {e.StatusInstance.Definition.name}");
    }
}