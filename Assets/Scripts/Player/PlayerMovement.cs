using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Vector2 maxSpeed = new(10f, 10f);
    [SerializeField] Vector2 timeToFullSpeed  = new(2f, 2f);
    [SerializeField] Vector2 timeToStop = new(2.5f, 2.5f);
    [SerializeField] Vector2 stopClamp = new(2.5f, 2.5f);
    Vector2 moveDirection;
    Vector2 moveVelocity;
    Vector2 moveFriction;
    Vector2 stopFriction;
    Rigidbody2D rb;
    BoxCollider2D bc;
    Vector2 cameraBounds;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        moveVelocity = 2 * maxSpeed/timeToFullSpeed;
        moveFriction = -2 * maxSpeed/(timeToFullSpeed * timeToFullSpeed);
        stopFriction = -2 * maxSpeed/(timeToStop * timeToStop);
        cameraBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }
    public void Move(){
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        Vector3 pos = transform.position;

        moveDirection = new Vector2(inputX, inputY);

        Vector2 friction = GetFriction();

        float newVelocityX = rb.velocity.x + (moveDirection.x * moveVelocity.x + friction.x) * Time.deltaTime;
        float newVelocityY = rb.velocity.y + (moveDirection.y * moveVelocity.y + friction.y) * Time.deltaTime;

        newVelocityX = Mathf.Clamp(newVelocityX, -maxSpeed.x, maxSpeed.x);
        newVelocityX = Mathf.Clamp(newVelocityX, -stopClamp.x, stopClamp.x);

        newVelocityY = Mathf.Clamp(newVelocityY, -maxSpeed.y, maxSpeed.y);
        newVelocityY = Mathf.Clamp(newVelocityY, -stopClamp.y, stopClamp.y);

        rb.velocity = new Vector2(newVelocityX, newVelocityY);

        CamClamp();
    }
    public Vector2 GetFriction(){
        float frictionX = 0;
        float frictionY = 0;

        if(moveDirection.x == 0){
            frictionX = Mathf.Clamp(rb.velocity.x * stopFriction.x, -Mathf.Abs(stopFriction.x), Mathf.Abs(stopFriction.x));
        }
        else{
            frictionX = Mathf.Clamp(rb.velocity.x, -Mathf.Abs(moveFriction.x), Mathf.Abs(moveFriction.x));
        }
        if(moveDirection.y == 0){
            frictionY = Mathf.Clamp(rb.velocity.y * stopFriction.y, -Mathf.Abs(stopFriction.y), Mathf.Abs(stopFriction.y));
        }
        else{
            frictionY = Mathf.Clamp(rb.velocity.y, -Mathf.Abs(moveFriction.y), Mathf.Abs(moveFriction.y));
        }
        
        return new Vector2(frictionX, frictionY);
    }
    public void MoveBound(){
        
    }
    private void CamClamp()
    {
        Vector2 playerSize = bc.size / 2f;

        float minX = -cameraBounds.x;
        float maxX = cameraBounds.x;
        float minY = -cameraBounds.y;
        float maxY = cameraBounds.y - playerSize.y;

        Vector2 clampedPosition = new Vector2(
            Mathf.Clamp(transform.position.x, minX, maxX),
            Mathf.Clamp(transform.position.y, minY, maxY)
        );

        transform.position = clampedPosition;
    }
    public bool IsMoving(){
        return rb.velocity.x != 0 || rb.velocity.y != 0;
    }
}
