using System;

[Serializable]
public abstract class StatusCondition
{
    public abstract bool Evaluate(StatusInstance status);

    public virtual string GetDisplayName()
    {
        return GetType().Name;
    }
}