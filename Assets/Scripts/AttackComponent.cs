using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AttackComponent : MonoBehaviour
{
    [SerializeField] public int damage; // Damage dealt by this attack

    void OnTriggerEnter2D(Collider2D other)
    {
        // Ignore collision if the other object has the same tag
        if (other.CompareTag(gameObject.tag))
        {
            return;
        }

        InvincibilityComponent invincibility = other.GetComponent<InvincibilityComponent>();
        if (invincibility != null)
        {
            invincibility.ActivateInvincibility();
        }

        // Check if the other object has a HitboxComponent and apply damage
        HitboxComponent hitbox = other.GetComponent<HitboxComponent>();
        if (hitbox != null)
        {
            hitbox.Damage(damage); // Apply damage to the other object's hitbox
        }
    }
}
