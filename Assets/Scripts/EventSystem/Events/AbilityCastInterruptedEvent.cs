public readonly struct AbilityCastInterruptedEvent
{
    public readonly AbilityExecutionSession Session;

    public AbilityCastInterruptedEvent(AbilityExecutionSession session)
    {
        Session = session;
    }
}