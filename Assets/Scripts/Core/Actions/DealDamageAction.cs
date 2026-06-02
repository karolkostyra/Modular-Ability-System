using UnityEngine;

[CreateAssetMenu]
public class DealDamageAction : AbilityActionDefinition
{
    public float Damage => damage;

    [SerializeField] private float damage;

    public override void Execute(AbilityContext context)
    {
        foreach (var target in context.Targets)
        {
            target.TakeDamage(damage);
        }
    }
}