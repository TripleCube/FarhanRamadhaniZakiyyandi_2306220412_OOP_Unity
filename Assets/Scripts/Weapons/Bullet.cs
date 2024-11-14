using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Stats")]
    public float bulletSpeed = 20;
    public int Damage { get; set; }
    private Rigidbody2D rb;
    private IObjectPool<Bullet> objectPool;
    void Awake(){
        rb = GetComponent<Rigidbody2D>();
        if (rb == null){
            Debug.LogError("Rigidbody2D component is missing from the bullet!");
        }
    }

    void Start(){
        
    }
    public void SetObjectPool(IObjectPool<Bullet> pool)
    {
        objectPool = pool;
        if (objectPool == null)
        {
            Debug.LogError("Bullet pool is not set correctly!");
        }
    }

    void ReturnToPool()
    {
        if (objectPool != null)
        {
            objectPool.Release(this);  // Return bullet to pool
        }
        else
        {
            Debug.LogError("Bullet pool is null when trying to return to pool. Make sure SetObjectPool is called before using the bullet.");
        }
    }

    void Update(){
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        if(transform.position.y > max.y){
            ReturnToPool();
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Hit enemy with bullet damage: " + Damage);
            ReturnToPool();
        }
    }
}
