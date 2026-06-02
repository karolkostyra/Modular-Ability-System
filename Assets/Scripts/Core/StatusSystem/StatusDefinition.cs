using UnityEngine;

public abstract class StatusDefinition : ScriptableObject
{
    public float Duration => duration;

    [SerializeField] private float duration;
}