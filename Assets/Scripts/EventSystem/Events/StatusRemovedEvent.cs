public readonly struct StatusRemovedEvent
{
    public readonly StatusInstance StatusInstance { get; }

    public StatusRemovedEvent(StatusInstance statusInstance)
    {
        StatusInstance = statusInstance;
    }
}