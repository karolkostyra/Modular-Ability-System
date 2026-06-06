using UnityEngine;

[CreateAssetMenu]
public class DamageOverTimeEffect : StatusEffectDefinition
{
    public override void Execute(StatusApplicationContext context)
    {
        foreach (var target in context.SourceAbilityContext.Targets)
        {
            target.TakeDamage(context.BaseDamage);
        }
    }
}