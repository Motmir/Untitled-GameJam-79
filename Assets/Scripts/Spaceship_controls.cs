using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Spaceship_controls : MonoBehaviour
{
    //Attributes
    public Camera cam;
    public Rigidbody2D spaceshipRB;
    public Transform shipbody;
    public GameObject beamObj, spaceBullet;
    public Transform gun;

    //control
    private Controls controls;
    private Vector2 moveInput, mousePos;
    
    //Variables
    public Vector2 acceleration = new Vector2(0.1f, 0.1f);
    public Vector2 maxSpeed = new Vector2(5, 5);
    public Vector2 drag = new Vector2(0.1f, 0.1f);

    public Vector2 FloorRoofShip = new Vector2(-50,50);
    public Vector2 FloorRoofCam = new Vector2(-40, 40);

    public int someSpeedFactor, xOffset;
    public float reloadTimer, reloadTimerVal;
    public bool lockedGun;

    private void Awake()
    {
        controls = new Controls();
        controls.Spaceship.Move.performed += Move;
        controls.Spaceship.Aim.performed += Aim;
        controls.Spaceship.Beam.started += Beam;
        controls.Spaceship.Beam.canceled += Beam;
        controls.Spaceship.Shoot.performed += Shoot;
        controls.Spaceship.SwitchScene.performed += Swich;
    }
    void OnEnable()
    {
        controls.Enable();
        controls.Spaceship.Move.Enable();
        controls.Spaceship.Aim.Enable();
        controls.Spaceship.Beam.Enable();
        controls.Spaceship.Shoot.Enable();
        controls.Spaceship.SwitchScene.Enable();
    }
    void OnDisable()
    {
        controls.Disable();
        controls.Spaceship.Move.Disable();
        controls.Spaceship.Aim.Disable();
        controls.Spaceship.Beam.Disable();
        controls.Spaceship.Shoot.Disable();
        controls.Spaceship.SwitchScene.Disable();
    }


    void Move(InputAction.CallbackContext c)
    {
        moveInput = c.ReadValue<Vector2>();
        
    }

    void Beam(InputAction.CallbackContext c)
    {
        bool beamOn = c.ReadValue<float>() > 0.5f;
        beamObj.SetActive(beamOn);

        //Call the ICow Interface "TractorBeamed()" method to trigger cow.
    }

    void Aim(InputAction.CallbackContext c)
    {
        mousePos = c.ReadValue<Vector2>();
        gun.up = (Vector2) cam.ScreenToWorldPoint(mousePos) - (Vector2) gun.position;
    }

    void Shoot(InputAction.CallbackContext c)
    {
        if (reloadTimer < 0)
        {
            Vector2 directionVector;
            if (lockedGun)
            {
                directionVector = Vector2.right;
            } else
            {
                directionVector = gun.up;
            }
            
            Vector2 bulletSpawn = (Vector2)transform.position + (directionVector / 2);
            GameObject bulletTransform = Instantiate(spaceBullet, bulletSpawn, Quaternion.identity);
            bulletTransform.GetComponent<SpaceBullet>().Setup(directionVector);
            reloadTimer = reloadTimerVal;
        }
    }

    void Swich(InputAction.CallbackContext c)
    {
        GameMaster.Instance.SwichScenes();
    }


    private void LateUpdate()
    {

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
        float shipY = Mathf.Clamp(spaceshipRB.transform.position.y, FloorRoofShip.x, FloorRoofShip.y);
        spaceshipRB.transform.position = new Vector3(spaceshipRB.transform.position.x, shipY, 0);

        spaceshipRB.AddTorque( Vector3.Dot(Vector3.up, -spaceshipRB.transform.right));

        if (reloadTimer >= 0) 
        { 
            reloadTimer -= Time.deltaTime;
        }
        
        float camX = Mathf.Lerp(cam.transform.position.x, shipbody.position.x + xOffset, Time.deltaTime * someSpeedFactor);
        float camY = Mathf.Lerp(cam.transform.position.y, shipbody.position.y, Time.deltaTime * someSpeedFactor);

        camY = Mathf.Clamp(camY, FloorRoofCam.x, FloorRoofCam.y);

        cam.transform.position = new Vector3(camX, camY, -10);

        //private float tilt = 0;
        //public float maxTilt = 90f;
        //tilt = Mathf.Lerp(tilt, Mathf.Sign(currentVelocity.x) * (speed) / (maxSpeed.x), 0.5f);
        //shipbody.rotation = Quaternion.Euler(0, 0, -maxTilt * tilt);

    }
}

