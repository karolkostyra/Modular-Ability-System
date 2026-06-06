public class AbilitySessionEventRouter
{
    private readonly IGameplayEventBus eventBus;

    public AbilitySessionEventRouter(IGameplayEventBus eventBus)
    {
        this.eventBus = eventBus;
    }

    public void Bind(AbilityExecutionSession session)
    {
        session.Finished += OnSessionFinished;

        session.CastStartedEvent += Publish<AbilityCastStartedEvent>;
        session.CastInterruptedEvent += Publish<AbilityCastInterruptedEvent>;
        session.CastFinishedEvent += Publish<AbilityCastFinishedEvent>;
    }

    public void Unbind(AbilityExecutionSession session)
    {
        session.Finished -= OnSessionFinished;

        session.CastStartedEvent -= Publish<AbilityCastStartedEvent>;
        session.CastInterruptedEvent -= Publish<AbilityCastInterruptedEvent>;
        session.CastFinishedEvent -= Publish<AbilityCastFinishedEvent>;
    }

    private void Publish<T>(T eventT)
    {
        eventBus.Publish(eventT);
    }

    private void OnSessionFinished(AbilityExecutionSession session)
    {
        Unbind(session);
    }
}