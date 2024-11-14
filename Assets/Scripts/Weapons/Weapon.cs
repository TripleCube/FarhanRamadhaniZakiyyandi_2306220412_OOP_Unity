using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Weapon : MonoBehaviour
{
    public Transform parentTransform;         
    [Header("Weapon Stats")]
    [SerializeField] private float shootIntervalInSeconds = 0.5f;
    [Header("Bullets")]
    public Bullet bullet;
    [SerializeField] private Transform bulletSpawnPoint;

    [Header("Bullet Pool")]
    private IObjectPool<Bullet> objectPool;
    private readonly bool collectionCheck = false;
    private readonly int defaultCapacity = 30;
    private readonly int maxSize = 100;
    private float timer;
    private bool canFire = false;
    void Awake()
    {
        // Initialize the object pool only if the bullet prefab is set
        if (bullet != null)
        {
            objectPool = new ObjectPool<Bullet>(
                () => CreateBullet(bullet),  // Factory method for bullets
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
            Debug.LogError("Bullet prefab is not set in Weapon!");
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
        bullet.transform.position = bulletSpawnPoint.position;
        bullet.transform.rotation = bulletSpawnPoint.rotation;
        bullet.gameObject.SetActive(true);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = bullet.transform.up * bullet.bulletSpeed;
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

    private void FixedUpdate()
    {
        if(canFire == true && objectPool != null){
            timer += Time.deltaTime;
            if (timer >= shootIntervalInSeconds){
                FireBullet();
                timer = 0f;
            }
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

    public void StartFiring()
    {
        canFire = true;  // Enable firing
    }

    public void StopFiring()
    {
        canFire = false;  // Disable firing
    }
}