using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AttackComponent1 : MonoBehaviour
{
    public Bullet1 bullet;
    public int damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(gameObject.tag)) return;

        if (other.GetComponent<HitboxComponent1>() != null)
        {
            HitboxComponent1 hitbox = other.GetComponent<HitboxComponent1>();

            if (bullet != null)
            {
                hitbox.Damage(bullet.damage);
            }

            hitbox.Damage(damage);
        }

        if (other.GetComponent<InvincibilityComponent1>() != null)
        {
            other.GetComponent<InvincibilityComponent1>().TriggerInvincibility();
        }
    }
}
