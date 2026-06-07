using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusDefinition : ScriptableObject
{
    public StatusLifetimeType LifetimeType => lifetimeType;
    public bool CanReapply => canReapply;
    public List<StatusEffectDefinition> Effects => effects;
    public float TickInterval => tickInterval;

    [SerializeField] private StatusLifetimeType lifetimeType;
    [SerializeField] private bool canReapply;
    [SerializeField] private List<StatusEffectDefinition> effects;
    [SerializeField] private float tickInterval = 1f;
}

public enum StatusLifetimeType
{
    Timed,
    Permanent,
    Conditional
}