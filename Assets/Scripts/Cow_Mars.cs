using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.XR;

public class Cow_Mars : MonoBehaviour
{
    /*
    internal Transform thisTransform;
    public Rigidbody2D rb; 

    // The movement speed of the object
    public float moveSpeed = 0.2f;

    // A minimum and maximum time delay for taking a decision, choosing a direction to move in
    public Vector2 decisionTime = new Vector2(1, 4);
    internal float decisionTimeCount = 0;

    // The possible directions that the object can move int, right, left, up, down, and zero for staying in place. I added zero twice to give a bigger chance if it happening than other directions
    internal Vector3[] moveDirections = new Vector3[] { Vector3.right, Vector3.left, Vector3.forward, Vector3.back, Vector3.zero, Vector3.zero };
    internal int currentMoveDirection;

    // Use this for initialization
    void Start()
    {
        // Cache the transform for quicker access
        //thisTransform = this.transform;
        rb = transform.GetComponent<Rigidbody2D>();
        thisTransform = rb.transform;

        // Set a random time delay for taking a decision ( changing direction, or standing in place for a while )
        decisionTimeCount = Random.Range(decisionTime.x, decisionTime.y);

        // Choose a movement direction, or stay in place
        ChooseMoveDirection();
    }

    // Update is called once per frame
    void Update()
    {
        // Move the object in the chosen direction at the set speed
        thisTransform.position += moveDirections[currentMoveDirection] * Time.deltaTime * moveSpeed;

        if (decisionTimeCount > 0) decisionTimeCount -= Time.deltaTime;
        else
        {
            // Choose a random time delay for taking a decision ( changing direction, or standing in place for a while )
            decisionTimeCount = Random.Range(decisionTime.x, decisionTime.y);

            // Choose a movement direction, or stay in place
            ChooseMoveDirection();
        }
    }

    void ChooseMoveDirection()
    {
        // Choose whether to move sideways or up/down
        currentMoveDirection = Mathf.FloorToInt(Random.Range(0, moveDirections.Length));
    }
    */
    
    public Rigidbody2D rb;

    public float speed = 5;
    public float directionChangeInterval = 1;
    public float maxHeadingChange = 30;
    Vector3 targetRotation;


    // A minimum and maximum time delay for taking a decision, choosing a direction to move in
    public Vector2 decisionTime = new Vector2(1, 4);
    internal float decisionTimeCount = 0;

    Vector3 moveDirection;


    float heading;

    public void Moo()
    {
        //Play audio clip
        throw new System.NotImplementedException();
    }

    void Awake()
    {
        // Set random initial rotation
        heading = UnityEngine.Random.Range(0, 360);
        transform.eulerAngles = new Vector3(0, heading, 0);

        // Set a random time delay for taking a decision ( changing direction, or standing in place for a while )
        decisionTimeCount = UnityEngine.Random.Range(decisionTime.x, decisionTime.y);
    }


    void NewHeadingRoutine()
    {
        var floor = transform.eulerAngles.y - maxHeadingChange;
        var ceil = transform.eulerAngles.y + maxHeadingChange;
        heading = UnityEngine.Random.Range(floor, ceil);
        targetRotation = new Vector3(0, heading, 0);
    }

    public void Move()
    {
        rb.velocity = moveDirection;
    }

    public void FixedUpdate()
    {

        if (decisionTimeCount > 0)
        {
            decisionTimeCount -= Time.deltaTime;
        }
        else
        {
            // Choose a random time delay for taking a decision ( changing direction, or standing in place for a while )
            decisionTimeCount = UnityEngine.Random.Range(decisionTime.x, decisionTime.y);

            // Choose a movement direction, or stay in place
            ChooseMoveDirection();
        }


        Move();
    }

    public void ChooseMoveDirection()
    {
        int randomNumber = UnityEngine.Random.Range(1, 5);

        switch (randomNumber)
        {
            case 1:
                moveDirection = Vector3.up;
                break;
            case 2:
                moveDirection = Vector3.down;                
                break;
            case 3:
                moveDirection = Vector3.down;
                break;
            default:
                moveDirection = Vector3.down;
                break;
        }
    }
    
}

