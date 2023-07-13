using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class earth_controls : MonoBehaviour
{

    //Attributes
    public Rigidbody2D rb;
    private Vector2 moveInput, moveDir;
    public InputAction playerMove;


    //Variables
    public float speed = 2;
    public float maxSpeed = 5;
    public Vector2 drag = new Vector2((float)0.1, (float)0.1);
    void OnEnable()
    {
        playerMove.Enable();
    }
    void OnDisable()
    {
       playerMove.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = playerMove.ReadValue<Vector2>();
    }
    void FixedUpdate()
    {
        moveDir = rb.velocity;
        //Accelerate the ship
        moveDir.x += moveInput.x * speed;
        if (Mathf.Abs(moveDir.x) > maxSpeed) { moveDir.x = maxSpeed * Mathf.Sign(moveDir.x); }
        moveDir.y += moveInput.y * speed;
        if (Mathf.Abs(moveDir.y) > maxSpeed) { moveDir.y = maxSpeed * Mathf.Sign(moveDir.y); }


        print("Before drag: " + moveDir.x);
        //Apply drag to the ship
        if (Mathf.Abs(moveDir.x) >= 0 && Mathf.Abs(moveDir.x) <= 1)
        {
            moveDir.x = 0;
        }
        else
        {
            moveDir.x -= drag.x * Mathf.Sign(moveDir.x);
        }
        print("After drag: " + moveDir.x);

        if (Mathf.Abs(moveDir.y) >= 0 && Mathf.Abs(moveDir.y) <= 1)
        {
            moveDir.y = 0;
        }
        else
        {
            moveDir.y -= drag.y * Mathf.Sign(moveDir.y);
        }

        //Apply the new velocity
        rb.velocity = new Vector2((moveDir.x), (moveDir.y));
    }


}
