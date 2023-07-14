using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

abstract public class EarthEntityParent : MonoBehaviour
{
    public GameObject ship;
    public Transform shipPos;
    public Rigidbody2D rb;
    public int dirTimer;
    public bool canSee, beamed;
    public float detectionRange;
    public Vector2 movement, distance, goal, directionVector, moveVector;

    public void CanSee()
    {
        int layerMask = 1 << 8;
        if (Physics2D.Raycast(transform.position, distance.normalized, distance.magnitude - 1, layerMask) == false
            && distance.magnitude < detectionRange)
        {
            canSee = true;
        }
        else
        {
            canSee = false;
        }
    }
    public void FindShip()
    {
        shipPos = ship.transform;
        directionVector = (shipPos.position - transform.position);
        distance = directionVector;
        directionVector.Normalize();
    }

    public Vector2 ChooseDir()
    {
        Vector2 dir = new Vector2(UnityEngine.Random.Range(-10, 10), 0);
        dir.x /= 7;
        return dir;
    }

    abstract public void FixedUpdate();

    abstract public void Move();

    // Start is called before the first frame update
    void Start()
    {
        ship = GameObject.Find("Spaceship");
        rb = transform.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}