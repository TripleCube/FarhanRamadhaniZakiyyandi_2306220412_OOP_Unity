using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HitboxComponent1 : MonoBehaviour
{
    [SerializeField]
    HealthComponent1 health;

    Collider2D area;

    private InvincibilityComponent1 invincibilityComponent;


    void Start()
    {
        area = GetComponent<Collider2D>();
        invincibilityComponent = GetComponent<InvincibilityComponent1>();
    }

    public void Damage(Bullet1 bullet)
    {
        if (invincibilityComponent != null && invincibilityComponent.isInvincible){
            return;
        } 
        if (health != null){
            health.Subtract(bullet.damage);
        }
            
    }

    public void Damage(int damage)
    {
        if (invincibilityComponent != null && invincibilityComponent.isInvincible){
            return;
        } 
        if (health != null){
            health.Subtract(damage);
        }
            
    }
}
