using UnityEngine;
using System.Collections.Generic;

public abstract class TargetResolver : ScriptableObject
{
    public abstract List<IAbilityTarget> ResolveTargets(AbilityContext context);
}