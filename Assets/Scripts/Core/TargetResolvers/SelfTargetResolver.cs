using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SelfTargetResolver : TargetResolver
{
    public override List<IAbilityTarget> ResolveTargets(AbilityContext context)
    {
        var result = new List<IAbilityTarget>();

        if (context.Caster.TryGetComponent<IAbilityTarget>(out var damageable))
        {
            result.Add(damageable);
        }

        return result;
    }
}