using UnityEngine;
using System.Collections.Generic;

public abstract class StatusDefinition : ScriptableObject
{
    public StatusLifetimeType LifetimeType => lifetimeType;
    public bool CanReapply => canReapply;
    public List<StatusEffectDefinition> Effects => effects;
    public List<StatusCondition> Conditions => conditions;
    public float TickInterval => tickInterval;

    [SerializeField] private StatusLifetimeType lifetimeType;
    [SerializeField] private bool canReapply;
    [SerializeField] private float tickInterval = 1f;
    [SerializeField] private List<StatusEffectDefinition> effects;
    [SerializeReference] private List<StatusCondition> conditions;
}

public enum StatusLifetimeType
{
    Timed,
    Permanent,
    Conditional
}