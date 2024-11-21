using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Pool;

public class Weapon1 : MonoBehaviour
{
    [Header("Weapon Stats")]
    [SerializeField] private float shootIntervalInSeconds = 0.5f;


    [Header("Bullets")]
    public Bullet1 bullet;
    [SerializeField] private Transform bulletSpawnPoint;


    [Header("Bullet Pool")]
    private IObjectPool<Bullet1> objectPool;

    private readonly bool collectionCheck = false;
    private readonly int defaultCapacity = 30;
    private readonly int maxSize = 100;

    private bool canFire = false;
    private float timer;


    public Transform parentTransform;


    private void Awake()
    {
        Assert.IsNotNull(bulletSpawnPoint);

        objectPool = new ObjectPool<Bullet1>(CreateBullet, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject, collectionCheck, defaultCapacity, maxSize);
    }


    private void Shoot()
    {
        Bullet1 bulletObj = objectPool.Get();

        bulletObj.transform.SetPositionAndRotation(bulletSpawnPoint.position, bulletSpawnPoint.rotation);
    }


    private void FixedUpdate()
    {
        if(canFire == true && objectPool != null){
        timer += Time.deltaTime;

        if (timer >= shootIntervalInSeconds)
        {
            timer = 0f;
            Shoot();
        }
        }
    }

    private Bullet1 CreateBullet()
    {
        Bullet1 instance = Instantiate(bullet);
        instance.objectPool = objectPool;
        instance.transform.parent = transform;

        return instance;
    }

    private void OnGetFromPool(Bullet1 obj)
    {
        obj.gameObject.SetActive(true);
    }

    private void OnReleaseToPool(Bullet1 obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void OnDestroyPooledObject(Bullet1 obj)
    {
        Destroy(obj.gameObject);
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
