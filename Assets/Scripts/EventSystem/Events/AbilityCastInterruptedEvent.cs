public readonly struct AbilityCastInterruptedEvent
{
    public readonly AbilityExecutionSession Session { get; }

    public AbilityCastInterruptedEvent(AbilityExecutionSession session)
    {
        Session = session;
    }
}