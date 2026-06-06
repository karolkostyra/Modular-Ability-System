public readonly struct AbilityCastFinishedEvent
{
    public readonly AbilityExecutionSession Session { get; }

    public AbilityCastFinishedEvent(AbilityExecutionSession session)
    {
        Session = session;
    }
}