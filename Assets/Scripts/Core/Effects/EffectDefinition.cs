using UnityEngine;

public abstract class EffectDefinition : ScriptableObject
{
    public abstract void Apply(AbilityContext context);
}