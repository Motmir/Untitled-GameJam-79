using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.;

public class earth_controls : MonoBehaviour
{

    //Attributes
    public Rigidbody2D rigidbody2d;
    private Vector2 mvVector;


    //Variables
    public float speed = 2;
    public float maxSpeed = 10;
    public float drag = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpadet()
    {
        PlayerMove();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(InputAction.CallbackContext context)
    {
        inputMvVector = context.ReadValue<Vector2>();
    }
}
