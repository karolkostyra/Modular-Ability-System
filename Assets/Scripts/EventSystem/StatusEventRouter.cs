public class StatusEventRouter
{
    private readonly IGameplayEventBus eventBus;

    public StatusEventRouter(IGameplayEventBus eventBus)
    {
        this.eventBus = eventBus;
    }

    public void Bind(StatusSystem statusSystem)
    {
        if (statusSystem == null)
            return;

        statusSystem.StatusAppliedEvent += Publish<StatusAppliedEvent>;
        statusSystem.StatusRefreshedEvent += Publish<StatusRefreshedEvent>;
        statusSystem.StatusExpiredEvent += Publish<StatusExpiredEvent>;
        statusSystem.StatusRemovedEvent += Publish<StatusRemovedEvent>;
    }

    public void Unbind(StatusSystem statusSystem)
    {
        if (statusSystem == null)
            return;

        statusSystem.StatusAppliedEvent -= Publish<StatusAppliedEvent>;
        statusSystem.StatusRefreshedEvent -= Publish<StatusRefreshedEvent>;
        statusSystem.StatusExpiredEvent -= Publish<StatusExpiredEvent>;
        statusSystem.StatusRemovedEvent -= Publish<StatusRemovedEvent>;
    }

    private void Publish<T>(T eventT)
    {
        eventBus.Publish(eventT);
    }
}