using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class AbilityDefinition : ScriptableObject
{
    public string Id => id;
    public float Cooldown => cooldown;
    public float CastTime => castTime;
    public InterruptType InterruptType => interruptType;
    public TargetResolver TargetResolver => targetResolver;
    //public IReadOnlyList<AbilityAction> Actions => actions;
    public List<AbilityAction> Actions => actions;

    [SerializeField] private string id;
    [SerializeField] private float cooldown;
    [SerializeField] private float castTime;
    [SerializeField] private InterruptType interruptType;
    [SerializeField] private TargetResolver targetResolver;
    [SerializeReference] private List<AbilityAction> actions;

    public bool CanBeInterruptedBy(InterruptType sourceInterruptType)
    {
        if (interruptType == InterruptType.None)
            return false;

        if (interruptType == InterruptType.Uninterruptible)
            return false;

        if (interruptType == InterruptType.Hard && sourceInterruptType == InterruptType.Soft)
            return false;

        return true;
    }
}

public enum InterruptType
{
    None,
    Soft,
    Hard,
    Uninterruptible
}