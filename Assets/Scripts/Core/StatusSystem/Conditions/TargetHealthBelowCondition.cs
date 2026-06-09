using System;

[Serializable]
public class TargetHealthBelowCondition : StatusCondition
{
    public float threshold;

    public override bool Evaluate(StatusInstance status)
    {
        return true; //TO_DO
        //return status.ApplicationContext.SourceAbilityContext.Targets.Health <= threshold;
    }
}