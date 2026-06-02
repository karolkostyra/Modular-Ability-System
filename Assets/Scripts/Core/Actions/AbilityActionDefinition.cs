using UnityEngine;

public abstract class AbilityActionDefinition : ScriptableObject
{
    public abstract void Execute(AbilityContext context);
}