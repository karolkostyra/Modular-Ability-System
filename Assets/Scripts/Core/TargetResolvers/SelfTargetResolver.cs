using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SelfTargetResolver : TargetResolver
{
    public override List<IDamageable> ResolveTargets(AbilityContext context)
    {
        var result = new List<IDamageable>();

        if (context.Caster.TryGetComponent<IDamageable>(out var damageable))
        {
            result.Add(damageable);
        }

        return result;
    }
}