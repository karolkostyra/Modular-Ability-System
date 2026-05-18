public class AbilityInstance
{
    public AbilityDefinition Definition { get; }
    public bool IsOnCooldown => cooldownRemaining > 0f;
    public float CooldownRemaining => cooldownRemaining;

    private float cooldownRemaining;

    public AbilityExecutionSession ActiveSession { get; private set; }

    public AbilityInstance(AbilityDefinition abilityDefinition)
    {
        Definition = abilityDefinition;
    }

    public AbilityExecutionSession CreateExecutionSession()
    {
        if (!CanCast())
            return null;

        ActiveSession = new AbilityExecutionSession(this);
        return ActiveSession;
    }

    public void Tick(float deltaTime)
    {
        TickCooldown(deltaTime);
    }

    private void TickCooldown(float deltaTime)
    {
        if (cooldownRemaining <= 0f)
            return;

        cooldownRemaining -= deltaTime;

        if (cooldownRemaining < 0f)
            cooldownRemaining = 0f;
    }

    public bool CanCast()
    {
        return !IsOnCooldown && (ActiveSession == null || ActiveSession.IsFinished);
    }

    public void StartCooldown()
    {
        cooldownRemaining = Definition.Cooldown;
    }

    internal void OnSessionFinished()
    {
        ActiveSession = null;
    }
}