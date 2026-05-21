public readonly struct AbilityCastStartedEvent
{
    public readonly AbilityExecutionSession Session;

    public AbilityCastStartedEvent(AbilityExecutionSession session)
    {
        Session = session;
    }
}