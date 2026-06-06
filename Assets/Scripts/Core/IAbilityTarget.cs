public interface IAbilityTarget
{
    void TakeDamage(float amount);
    void ApplyStatus(StatusDefinition statusDefinition, StatusApplicationContext statusApplicationContext);
}