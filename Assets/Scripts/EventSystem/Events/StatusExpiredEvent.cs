public readonly struct StatusExpiredEvent
{
    public readonly StatusInstance StatusInstance { get; }

    public StatusExpiredEvent(StatusInstance statusInstance)
    {
        StatusInstance = statusInstance;
    }
}