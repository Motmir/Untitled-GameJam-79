using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Spaceship_controls : MonoBehaviour
{
    //Attributes
    public Camera cam;
    public Rigidbody spaceshipRB;
    public Transform shipbody;
    public GameObject beamObj;
    public Transform gun;

    //control
    private Controls controls;
    private Vector2 moveInput, mousePos;

    
    //Variables
    public Vector2 acceleration = new Vector2(0.1f, 0.1f);
    public Vector2 maxSpeed = new Vector2(5, 5);
    public Vector2 drag = new Vector2(0.1f, 0.1f);

    private void Awake()
    {
        controls = new Controls();
        controls.Spaceship.Move.performed += Move;
        controls.Spaceship.Aim.performed += Aim;
        controls.Spaceship.Beam.performed += Beam;
        controls.Spaceship.Shoot.performed += Shoot;
    }
    void OnEnable()
    {
        controls.Enable();
        controls.Spaceship.Move.Enable();
        controls.Spaceship.Aim.Enable();
        controls.Spaceship.Beam.Enable();
        controls.Spaceship.Shoot.Enable();
    }
    void OnDisable()
    {
        controls.Disable();
        controls.Spaceship.Move.Disable();
        controls.Spaceship.Aim.Disable();
        controls.Spaceship.Beam.Disable();
        controls.Spaceship.Shoot.Disable();
    }


    void Move(InputAction.CallbackContext c)
    {
        Debug.Log(moveInput);
        moveInput = c.ReadValue<Vector2>();
        
    }

    void Beam(InputAction.CallbackContext c)
    {
        bool beamOn = c.ReadValue<float>() > 0.5f;
        beamObj.SetActive(beamOn);
    }

    void Aim(InputAction.CallbackContext c)
    {
        mousePos = c.ReadValue<Vector2>();
        Debug.Log(cam.ScreenToWorldPoint(mousePos));
        gun.up = (Vector2) cam.ScreenToWorldPoint(mousePos) - (Vector2) gun.position;
    }

    void Shoot(InputAction.CallbackContext c)
    {
        bool shootPressed = c.ReadValue<float>() > 0.5f;

    }
    

    void FixedUpdate()
    {
        //ship movement
        Vector2 currentVelocity = spaceshipRB.velocity;
        Vector2 currentSpeed = new Vector2(Mathf.Abs(currentVelocity.x), Mathf.Abs(currentVelocity.y));
        currentVelocity += moveInput * acceleration;
        currentVelocity -= drag * currentVelocity;
        if (currentSpeed.x > maxSpeed.x) { currentVelocity.x = maxSpeed.x * Mathf.Sign(currentVelocity.x); }
        if (currentSpeed.y > maxSpeed.y) { currentVelocity.y = maxSpeed.y * Mathf.Sign(currentVelocity.y); }
        spaceshipRB.velocity = currentVelocity;
        Debug.Log(currentVelocity);




    //private float tilt = 0;
    //public float maxTilt = 90f;
    //tilt = Mathf.Lerp(tilt, Mathf.Sign(currentVelocity.x) * (speed) / (maxSpeed.x), 0.5f);
    //shipbody.rotation = Quaternion.Euler(0, 0, -maxTilt * tilt);

    }
}

