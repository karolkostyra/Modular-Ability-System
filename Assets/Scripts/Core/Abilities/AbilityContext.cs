using System.Collections.Generic;
using UnityEngine;

public class AbilityContext
{
    public GameObject Caster;
    public Vector3 Origin;
    public AbilityInstance AbilityInstance;
    public List<IAbilityTarget> Targets;
}