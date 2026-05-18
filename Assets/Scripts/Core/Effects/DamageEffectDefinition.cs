using UnityEngine;

[CreateAssetMenu]
public class DamageEffectDefinition : EffectDefinition
{
    public float damage;

    public override void Apply(AbilityContext context)
    {
        if (context == null)
            return;

        if (context.Targets == null || context.Targets.Count == 0)
            return;

        foreach (var target in context.Targets)
        {
            target.TakeDamage(damage);
        }
    }
}