using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Vector2 maxSpeed;
    [SerializeField] Vector2 timeToFullSpeed;
    [SerializeField] Vector2 timeToStop;
    [SerializeField] Vector2 stopClamp;
    Vector2 moveDirection;
    Vector2 moveVelocity;
    Vector2 moveFriction;
    Vector2 stopFriction;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveVelocity = 2 * maxSpeed/timeToFullSpeed;
        moveFriction = -2 * maxSpeed/(timeToFullSpeed * timeToFullSpeed);
        stopFriction = -2 * maxSpeed/(timeToStop * timeToStop);
    }
    public void Move(){
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        moveDirection = new Vector2(inputX, inputY);

        Vector2 friction = GetFriction();

        float newVelocityX = rb.velocity.x + (moveDirection.x * moveVelocity.x + friction.x) * Time.deltaTime;
        float newVelocityY = rb.velocity.y + (moveDirection.y * moveVelocity.y + friction.y) * Time.deltaTime;

        newVelocityX = Mathf.Clamp(newVelocityX, -maxSpeed.x, maxSpeed.x);
        newVelocityX = Mathf.Clamp(newVelocityX, -stopClamp.x, stopClamp.x);

        newVelocityY = Mathf.Clamp(newVelocityY, -maxSpeed.y, maxSpeed.y);
        newVelocityY = Mathf.Clamp(newVelocityY, -stopClamp.y, stopClamp.y);

        rb.velocity = new Vector2(newVelocityX, newVelocityY);
    }
    public Vector2 GetFriction(){
        float frictionX = 0;
        float frictionY = 0;
        if(moveDirection.x != 0){
            frictionX = Mathf.Clamp(rb.velocity.x, moveFriction.x, -moveFriction.x);
        }
        if(moveDirection.y != 0){
            frictionY = Mathf.Clamp(rb.velocity.x, moveFriction.x, -moveFriction.x);
        }
        
        return new Vector2(frictionX, frictionY);
    }
    public void MoveBound(){

    }
    public bool IsMoving(){
        return rb.velocity.x != 0 || rb.velocity.y != 0;
    }
}
