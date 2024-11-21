using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Vector2 dir;
    private Vector3 direction;
    private Vector3 spawnPoint;
    public Weapon1 weapon;
    private AttackComponent attackComponent;
    private Bullet1 bullet;

    void Start()
    {
        attackComponent = GetComponent<AttackComponent>();
        Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        if (Random.value > 0.5f)
        {
            spawnPoint = new Vector3(-screenBounds.x + 1, Random.Range(-screenBounds.y + 1f, screenBounds.y), 0);
            direction = Vector3.left;
        }
        else
        {
            spawnPoint = new Vector3(screenBounds.x - 1, Random.Range(-screenBounds.y + 1f, screenBounds.y), 0);
            direction = Vector3.right;
        }
        transform.position = spawnPoint;
        weapon = GetComponent<Weapon1>();
    }

    void Update()
    {
        // Move the enemy
        transform.Translate(direction * moveSpeed * Time.deltaTime);

        // Check if the enemy is off the screen and reverse direction
        Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));


        if (transform.position.x > screenBounds.x)
        {
            direction = -direction;
        }
        else if (transform.position.x < -screenBounds.x)
        {
            direction = -direction;
        }
        StartFiring();
    }

    void StartFiring()
    {
        if (weapon != null)
        {
            weapon.StartFiring();
        }
    }
}
