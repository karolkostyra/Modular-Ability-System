using System;
using UnityEngine;

[Serializable]
public class UnitBaseStats
{
    [SerializeField] private float baseDamage;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float armor;

    public float Get(StatType stat)
    {
        return stat switch
        {
            StatType.Damage => baseDamage,
            StatType.MoveSpeed => moveSpeed,
            StatType.Armor => armor,
            _ => 0f
        };
    }
}