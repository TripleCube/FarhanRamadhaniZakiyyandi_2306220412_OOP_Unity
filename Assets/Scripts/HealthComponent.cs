using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] public int maxHealth;
    private int health;
    void Start()
    {
        health = maxHealth;
    }

    public int Health => health;

    public void Subtract(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Destroy(gameObject); 
        }
    }
}
