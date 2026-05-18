using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    //test

    public float health = 100;

    public void TakeDamage(float amount)
    {
        health -= amount;
    }
}
