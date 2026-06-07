public class StatusInstance
{
    public StatusDefinition Definition { get; }
    public int Stacks { get; private set; }
    public float RemainingDuration { get; private set; }
    public bool IsExpired { get; private set; }
    public StatusApplicationContext ApplicationContext { get; private set; }
    public float TickTimer { get; set; }

    public StatusInstance(StatusDefinition statusDefinition, StatusApplicationContext context)
    {
        Definition = statusDefinition;
        ApplicationContext = context;

        RemainingDuration = CalculateRemainingDuration();
    }

    public void ReduceDuration(float deltaTime)
    {
        if (Definition.LifetimeType != StatusLifetimeType.Timed)
            return;

        RemainingDuration -= deltaTime;
    }

    public void ExtendDuration(float duration)
    {
        if (Definition.LifetimeType != StatusLifetimeType.Timed)
            return;

        RemainingDuration += duration;
    }

    public void RefreshDuration(float duration)
    {
        if (Definition.LifetimeType != StatusLifetimeType.Timed)
            return;

        RemainingDuration = duration;
    }

    public void AddStack()
    {
        Stacks++;
    }

    public void Refresh(StatusDefinition statusDefinition, StatusApplicationContext context)
    {
        RemainingDuration = CalculateRemainingDuration();
    }

    private float CalculateRemainingDuration()
    {
        if (Definition.LifetimeType == StatusLifetimeType.Timed)
        {
            return ApplicationContext.BaseDuration * ApplicationContext.DurationMultiplier;
        }
        else
        {
            return -1;
        }
    }
}