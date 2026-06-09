public interface IAbilityTarget
{
    float Health { get; }

    void TakeDamage(float amount);
    void ApplyStatus(StatusDefinition statusDefinition, StatusApplicationContext statusApplicationContext);
}