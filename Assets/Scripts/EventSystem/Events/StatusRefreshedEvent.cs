public readonly struct StatusRefreshedEvent
{
    public readonly StatusInstance StatusInstance { get; }

    public StatusRefreshedEvent(StatusInstance statusInstance)
    {
        StatusInstance = statusInstance;
    }
}