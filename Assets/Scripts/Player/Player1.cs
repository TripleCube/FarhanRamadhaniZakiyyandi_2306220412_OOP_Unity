using UnityEngine;

public class Player1 : MonoBehaviour
{
    // This for getting the instace of Player Singleton
    public static Player1 Instance { get; private set; }

    // Getting the PlayerMovement methods
    PlayerMovement1 playerMovement;
    // Animator
    Animator animator;


    // Key for Singleton
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    // Getting Component
    void Start()
    {
        // Get PlayerMovement components
        playerMovement = GetComponent<PlayerMovement1>();

        // Get Animator components
        animator = GameObject.Find("EngineEffect").GetComponent<Animator>();
    }

    // Using FixedUpdate to Move because of physics
    void FixedUpdate()
    {
        playerMovement.Move();
    }

    // LateUpdate for animation related
    void LateUpdate()
    {
        playerMovement.MoveBound();
        animator.SetBool("IsMoving", playerMovement.IsMoving());
    }
}
