public readonly struct StatusAppliedEvent
{
    public readonly StatusInstance StatusInstance { get; }

    public StatusAppliedEvent(StatusInstance statusInstance)
    {
        StatusInstance = statusInstance;
    }
}