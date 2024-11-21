using UnityEngine;

public class EnemyHorizontalMovement : Enemy1
{
    [SerializeField] private float moveSpeed = 5f;

    private Vector2 dir;
    private Vector3 direction;
    private Vector3 spawnPoint;

    void Start()
    {
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



    }
}
