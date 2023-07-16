using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

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

    private int ammoAmount;
    private Amoo[] amooray; 

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
        SpawnAmmo();
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

    private void SpawnAmmo()
    {
        ammoAmount = GameObject.Find("GameMaster").GetComponent<GameMaster>().ammo;
        amooray = new Amoo[ammoAmount];
        GameObject ammoObj = (GameObject)Resources.Load("Ammo");
        for (int i = 0; i < ammoAmount; i++)
        {
            GameObject Object = Instantiate(ammoObj, GameObject.Find("Canvas").transform);
            Object.transform.SetParent(GameObject.Find("Canvas").transform);
            if (SceneManager.GetActiveScene().name == "Earth Scene")
            {
                Object.transform.position = new Vector3(40 * i, 0, 0) + Object.transform.position;
            } else
            {
                Object.transform.position = new Vector3(40 * i, -390, 0) + Object.transform.position;
            }
            
            amooray[i] = Object.GetComponent<Amoo>();
        }
    }

    public void UpdateReload()
    {
        for (int i = 0; i < ammoAmount; i++)
        {
            amooray[i].canReload = false;
        }
        for (int i = 0; i < ammoAmount; i++)
        {
            
            if (!amooray[i].full)
            {
                amooray[i].canReload = true;
                return;
            }
        }
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
        for (int i = ammoAmount - 1; i >= 0; i--) 
        {
            if (amooray[i].full)
            {
                amooray[i].Use();
                Vector2 directionVector;
                if (lockedGun)
                {
                    directionVector = Vector2.right;
                }
                else
                {
                    directionVector = gun.up;
                }

                Vector2 bulletSpawn = (Vector2)transform.position + (directionVector / 2);
                GameObject bulletTransform = Instantiate(spaceBullet, bulletSpawn, Quaternion.identity);
                bulletTransform.GetComponent<SpaceBullet>().Setup(directionVector);
                reloadTimer = reloadTimerVal;
                return;
            }
        }
    }

    void Swich(InputAction.CallbackContext c)
    {
        GameMaster.Instance.SwichScenes();
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
        if(spaceshipRB.transform.position.y > FloorRoofShip.y)
        {
            spaceshipRB.velocity = new Vector2(currentVelocity.x, -0.5f);
        }
        else if (spaceshipRB.transform.position.y < FloorRoofShip.x)
        {
            spaceshipRB.velocity = new Vector2(currentVelocity.x, 0.5f);
        }
        spaceshipRB.AddTorque( Vector3.Dot(Vector3.up, -spaceshipRB.transform.right));

        UpdateReload();


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

