public class StatusInstance
{
    public StatusDefinition Definition { get; }
    public float RemainingDuration { get; private set; }
    public bool IsExpired => RemainingDuration <= 0f;

    public StatusInstance(StatusDefinition statusDefinition)
    {
        Definition = statusDefinition;
        RemainingDuration = statusDefinition.Duration;
    }

    public void Tick(float deltaTime)
    {
        RemainingDuration -= deltaTime;
    }
}