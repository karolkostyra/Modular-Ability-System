using System;
using UnityEngine;

[Serializable]
public class TargetHealthBelowCondition : StatusCondition
{
    public float threshold;

    public override bool Evaluate(StatusInstance status)
    {
        var targets = status.ApplicationContext.SourceAbilityContext.Targets;

        foreach (var item in targets)
        {
            if (item.Health > threshold)
            {
                return false;
            }
        }

        return true;
    }
}