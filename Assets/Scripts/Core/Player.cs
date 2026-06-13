using UnityEngine;

public class Player : MonoBehaviour, IAbilityTarget, IUnitStats
{
    public float Health => health;
    public UnitStats Stats { get; private set; }

    [SerializeField] private UnitBaseStats baseStats;
    [SerializeField] private float health = 100;

    private StatusSystem statusSystem;

    public void Initialize(StatusSystem statusSystem)
    {
        this.statusSystem = statusSystem;

        Stats = new UnitStats(baseStats, statusSystem);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
    }

    public void ApplyStatus(StatusDefinition statusDefinition, StatusApplicationContext statusApplicationContext)
    {
        statusSystem.Apply(statusDefinition, statusApplicationContext);
    }

    private void Update()
    {
        statusSystem.Tick(Time.deltaTime);
    }
}
