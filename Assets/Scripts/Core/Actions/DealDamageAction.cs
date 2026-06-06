using System;
using UnityEngine;

[Serializable]
public class DealDamageAction : AbilityAction
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

    public override string GetDisplayName()
    {
        return $"Deal Damage ({damage})";
    }
}