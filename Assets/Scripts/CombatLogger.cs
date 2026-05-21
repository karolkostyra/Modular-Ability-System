using UnityEngine;

public class CombatLogger
{
    public CombatLogger(IGameplayEventBus bus)
    {
        bus.Subscribe<AbilityCastStartedEvent>(OnCastStarted);
        bus.Subscribe<AbilityCastInterruptedEvent>(OnCastInterrupted);
        bus.Subscribe<AbilityCastFinishedEvent>(OnCastFinished);
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
}