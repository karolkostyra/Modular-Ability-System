using System;

[Serializable]
public abstract class AbilityAction
{
    public abstract void Execute(AbilityContext context);

    public virtual string GetDisplayName()
    {
        return GetType().Name;
    }
}