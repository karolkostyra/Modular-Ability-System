public readonly struct AbilityCastFinishedEvent
{
    public readonly AbilityExecutionSession Session;

    public AbilityCastFinishedEvent(AbilityExecutionSession session)
    {
        Session = session;
    }
}