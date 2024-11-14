using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy : MonoBehaviour
{
    public int Level;
    public Transform player;
    public float fireRate = 1f;

    [RequireComponent(typeof(HealthComponent))]
    [RequireComponent(typeof(HitboxComponent))]
    public class EnemyHorizontal : MonoBehaviour
    {
        private float speed = 5f;
        private Vector2 screenBounds;
        private AttackComponent attackComponent;

        void Start()
        {
            screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
            transform.position = new Vector3(
                Random.value > 0.5f ? screenBounds.x : -screenBounds.x,
                Random.Range(-screenBounds.y, screenBounds.y),
                0
            );
            speed = transform.position.x > 0 ? -Mathf.Abs(speed) : Mathf.Abs(speed);
            attackComponent = GetComponent<AttackComponent>();
        }

        void Update()
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);

            // Wrap around horizontally
            if (transform.position.x > screenBounds.x)
            {
                transform.position = new Vector3(-screenBounds.x, transform.position.y, 0);
            }
            else if (transform.position.x < -screenBounds.x)
            {
                transform.position = new Vector3(screenBounds.x, transform.position.y, 0);
            }
        }
    }

    [RequireComponent(typeof(HealthComponent))]
    [RequireComponent(typeof(HitboxComponent))]
    public class EnemyForward : MonoBehaviour
    {
        private float speed = 5f;
        private Vector2 screenBounds;
        private AttackComponent attackComponent;

        void Start()
        {
            screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
            transform.position = new Vector3(
                Random.Range(-screenBounds.x, screenBounds.x),
                screenBounds.y,
                0
            );
            attackComponent = GetComponent<AttackComponent>();
        }

        void Update()
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);

            // Wrap around vertically
            if (transform.position.y < -screenBounds.y)
            {
                transform.position = new Vector3(transform.position.x, screenBounds.y, 0);
            }
        }
    }

    [RequireComponent(typeof(HealthComponent))]
    [RequireComponent(typeof(HitboxComponent))]
    public class EnemyTargeting : MonoBehaviour
    {
        private float speed = 5f;
        private Vector2 screenBounds;
        public Transform player;
        void Start()
        {
            screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
            transform.position = new Vector3(
                Random.value > 0.5f ? screenBounds.x : -screenBounds.x,
                Random.Range(-screenBounds.y, screenBounds.y),
                0
            );
        }

        void Update()
        {
            if (player != null)
            {
                Vector3 direction = (player.position - transform.position).normalized;
                transform.Translate(direction * speed * Time.deltaTime);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // Die immediately on contact with the player
            if (other.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
        }
    }

    // Boss enemy that spawns left or right, moves horizontally, and fires bullets
    [RequireComponent(typeof(HealthComponent))]
    [RequireComponent(typeof(HitboxComponent))]
    public class EnemyBoss : MonoBehaviour
    {
        private float speed = 3f;
        private Vector2 screenBounds;
        private float nextFireTime = 0f;
        [SerializeField] private float shootIntervalInSeconds = 0.5f;
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private Transform bulletSpawnPoint;
        private IObjectPool<Bullet> objectPool;

        private readonly bool collectionCheck = false;
        private readonly int defaultCapacity = 30;
        private readonly int maxSize = 100;
        private float timer;
        public float fireRate = 1f;

        void Start()
        {
            screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
            transform.position = new Vector3(
                Random.value > 0.5f ? screenBounds.x : -screenBounds.x,
                Random.Range(-screenBounds.y, screenBounds.y),
                0
            );
            speed = transform.position.x > 0 ? -Mathf.Abs(speed) : Mathf.Abs(speed);

            // Initialize the bullet object pool if the bullet prefab is assigned
            if (bulletPrefab != null)
            {
                objectPool = new ObjectPool<Bullet>(
                    () => CreateBullet(bulletPrefab),  // Factory method for bullets
                    OnGetBullet,
                    OnReleaseBullet,
                    OnDestroyBullet,
                    collectionCheck,
                    defaultCapacity,
                    maxSize
                );
            }
            else
            {
                Debug.LogError("Bullet prefab is not set in EnemyBoss!");
            }
        }

        private Bullet CreateBullet(Bullet prefab)
        {
            Bullet newBullet = Instantiate(prefab);
            newBullet.SetObjectPool(objectPool);  // Provide pool to bullet for returning
            return newBullet;
        }

        private void OnGetBullet(Bullet bullet)
        {
            if (bulletSpawnPoint != null)
            {
                bullet.transform.position = bulletSpawnPoint.position;
                bullet.transform.rotation = bulletSpawnPoint.rotation;
                bullet.gameObject.SetActive(true);

                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.velocity = bullet.transform.up * bullet.bulletSpeed;
                }
            }
            else
            {
                Debug.LogError("Bullet spawn point is not set in EnemyBoss!");
            }
        }

        private void OnReleaseBullet(Bullet bullet)
        {
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
            }
            bullet.gameObject.SetActive(false);  // Hide bullet when returned to pool
        }

        private void OnDestroyBullet(Bullet bullet)
        {
            Destroy(bullet.gameObject);
        }

        void Update()
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);

            // Wrap around horizontally
            if (transform.position.x > screenBounds.x)
            {
                transform.position = new Vector3(-screenBounds.x, transform.position.y, 0);
            }
            else if (transform.position.x < -screenBounds.x)
            {
                transform.position = new Vector3(screenBounds.x, transform.position.y, 0);
            }

            // Fire bullets at intervals
            nextFireTime += Time.deltaTime;
            if(nextFireTime >= fireRate)
            {
                FireBullet();
                nextFireTime = 0f;
            }
        }

        private void FireBullet()
        {
            if (objectPool == null)
            {
                Debug.LogError("Bullet pool not initialized!");
                return;
            }
            objectPool.Get();  // Retrieve a bullet from the pool and shoot it
        }
    }
}
