using UnityEngine;

public class EnemyTargetPlayer : Enemy1
{
    public float speed = 2f;

    private Transform player;
    private Vector3 screenBounds;
    Rigidbody2D rb;

    void RandomizeSpawnPoint()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        transform.position = new Vector3(Random.Range(-screenBounds.x, screenBounds.x), Random.Range(-screenBounds.y, screenBounds.y), 0);
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        RandomizeSpawnPoint();
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x);

            rb.rotation = angle;
            rb.velocity = speed * Time.deltaTime * direction;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
