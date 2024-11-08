using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float rotateSpeed;

    Vector2 newPosition;
    GameObject player;
    void Start(){
        ChangePosition();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update(){
        transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
        
        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, newPosition) < 0.5f){
            ChangePosition();
        }

        if (player != null && player.GetComponentInChildren<Weapon>() != null){
            GetComponent<Collider2D>().enabled = true;
            GetComponent<SpriteRenderer>().enabled = true;
        }else{
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("Player")){
            Debug.Log("Objek Player Memasuki trigger");
            LevelManager levelManager = FindObjectOfType<LevelManager>();
            if(levelManager != null){
                levelManager.LoadScene("Main");
            }
        }else{
            Debug.Log("Bukan Objek Player yang memasuki Trigger");
        }
    }
    void ChangePosition(){
        float randomX = Random.Range(-8f, 8f); 
        float randomY = Random.Range(-4f, 4f); 
        newPosition = new Vector2(randomX, randomY);
    }
}
