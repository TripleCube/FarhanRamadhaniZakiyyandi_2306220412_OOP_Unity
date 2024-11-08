using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] Weapon weaponHolder;
    Weapon weapon;
    void Awake(){
        weapon = weaponHolder;
    }
    void Start()
    {
        if(weapon != null){
            TurnVisual(true);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player")){
            Debug.Log("Objek Player Memasuki trigger");
            weapon.transform.SetParent(other.transform);
            weapon.transform.localPosition = Vector3.zero;
            weapon.parentTransform = other.transform;
            TurnVisual(true);
            Destroy(gameObject);
        }else{
            Debug.Log("Bukan Objek Player yang memasuki Trigger");
        }
    }

    void TurnVisual(bool on)
    {
        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
        foreach(var sprite in sprites){
            sprite.enabled = on;
        }
    }

    void TurnVisual(bool on, Weapon weapon){
        TurnVisual(on);
        if(weapon != null){
            foreach(var sprite in weapon.GetComponentsInChildren<SpriteRenderer>()){
                sprite.color = on ? Color.white : new Color(1, 1, 1, 0); 
            }
        }
    }
}
