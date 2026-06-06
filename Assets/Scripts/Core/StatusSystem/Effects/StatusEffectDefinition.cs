using UnityEngine;

public abstract class StatusEffectDefinition : ScriptableObject
{
    public abstract void Execute(StatusApplicationContext context);
}