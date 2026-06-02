using UnityEngine;

public class Player : MonoBehaviour, IAbilityTarget
{
    //test

    public float health = 100;
    
    public void TakeDamage(float amount)
    {
        health -= amount;
    }

    public void ApplyStatus(StatusDefinition status)
    {
        //TO_DO: delegate this action to StatusSystem
    }
}
