using System;
using UnityEngine;

[Serializable]
public class ApplyStatusAction : AbilityAction
{
    [SerializeField] private StatusDefinition status;

    [SerializeField] private float baseDuration;
    [SerializeField] private bool isStacabkle;
    [SerializeField] private int maxStacks;
    [SerializeField] private float baseDamage;
    [SerializeField] private StatusApplicationRule applicationRule;

    public override void Execute(AbilityContext context)
    {
        foreach (var target in context.Targets)
        {
            var statusContext = new StatusApplicationContext
            {
                SourceAbilityContext = context,
                ApplicationRule = applicationRule,
                BaseDuration = baseDuration,
                //IsStackable = isStacabkle,
                //MaxStacks = maxStacks,
                BaseDamage = baseDamage,
                CasterLevel = 0, //context.Caster.Level,
                CasterPower = 0, //context.Caster.Power,
                DurationMultiplier = 1,
                //StrengthMultiplier = 1,
                //IsCriticalApplication = false
            };

            target.ApplyStatus(status, statusContext);
        }
    }

    public override string GetDisplayName()
    {
        return $"Apply Status ({status.name})";
    }
}