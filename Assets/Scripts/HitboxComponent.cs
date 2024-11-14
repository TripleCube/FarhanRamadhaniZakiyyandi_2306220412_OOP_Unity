using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HitboxComponent : MonoBehaviour
{
    public HealthComponent healthComponent;
    private InvincibilityComponent invincibilityComponent;
    void Awake()
    {
        if (healthComponent == null)
        {
            healthComponent = GetComponent<HealthComponent>();
        }
        invincibilityComponent = GetComponent<InvincibilityComponent>();
    }
    public void Damage(int amount)
    {
        if (invincibilityComponent == null || !invincibilityComponent.isInvincible)
        {
            if (healthComponent != null)
            {
                healthComponent.Subtract(amount);
            }
        }
    }
}
