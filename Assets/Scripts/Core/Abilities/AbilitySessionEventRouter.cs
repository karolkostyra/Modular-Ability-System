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

        session.CastStartedEvent += Bind<AbilityCastStartedEvent>;
        session.CastInterruptedEvent += Bind<AbilityCastInterruptedEvent>;
        session.CastFinishedEvent += Bind<AbilityCastFinishedEvent>;
    }

    public void Unbind(AbilityExecutionSession session)
    {
        session.Finished -= OnSessionFinished;

        session.CastStartedEvent -= Bind<AbilityCastStartedEvent>;
        session.CastInterruptedEvent -= Bind<AbilityCastInterruptedEvent>;
        session.CastFinishedEvent -= Bind<AbilityCastFinishedEvent>;
    }

    private void Bind<T>(T eventT)
    {
        eventBus.Publish(eventT);
    }

    private void OnSessionFinished(AbilityExecutionSession session)
    {
        Unbind(session);
    }
}