public readonly struct AbilityCastStartedEvent
{
    public readonly AbilityExecutionSession Session { get; }

    public AbilityCastStartedEvent(AbilityExecutionSession session)
    {
        Session = session;
    }
}