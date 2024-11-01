using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    PlayerMovement playerMovement;
    Animator animator;

    void Awake(){
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }else{
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        if (playerMovement == null){
            Debug.LogWarning("PlayerMovement Missing");
        }
        GameObject engineEffect = GameObject.Find("EngineEffect");
        if (engineEffect == null){
            Debug.LogWarning("EngineEffect GameObject Missing");
        }else{
            animator = engineEffect.GetComponent<Animator>();
        }
        
    }
    void FixedUpdate()
    {
        if (playerMovement != null){
            playerMovement.Move();
        }
    }
    void LateUpdate(){
        if (animator != null && playerMovement != null){
            animator.SetBool("IsMoving", playerMovement.IsMoving());
        }
    }
}
