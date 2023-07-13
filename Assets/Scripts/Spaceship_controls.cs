using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Spaceship_controls : MonoBehaviour
{

    //Attributes
    public Rigidbody2D spaceshipRB;
    public GameObject beamObj;
    public InputAction playerMove;
    public InputAction beam;

    //Variables
    public Vector2 acceleration = new Vector2(0.1f, 0.1f);
    public Vector2 maxSpeed = new Vector2(5, 5);
    public Vector2 drag = new Vector2(0.1f, 0.1f);
    void OnEnable()
    {
        playerMove.Enable();
        beam.Enable();
    }
    void OnDisable()
    {
        playerMove.Disable();
        beam.Disable();
    }
    void FixedUpdate()
    {
        //ship movement
        Vector2 moveInput = playerMove.ReadValue<Vector2>();
        Vector2 currentSpeed = spaceshipRB.velocity;
        currentSpeed += moveInput * acceleration;
        currentSpeed -= drag * currentSpeed;
        if (Mathf.Abs(currentSpeed.x) > maxSpeed.x) { currentSpeed.x = maxSpeed.x * Mathf.Sign(currentSpeed.x); }
        if (Mathf.Abs(currentSpeed.y) > maxSpeed.y) { currentSpeed.y = maxSpeed.y * Mathf.Sign(currentSpeed.y); }
        spaceshipRB.velocity = currentSpeed;
        Debug.Log(currentSpeed);

        //beam
        bool beamOn = beam.ReadValue<float>() > 0.5f;
        beamObj.SetActive(beamOn);
    }
}

