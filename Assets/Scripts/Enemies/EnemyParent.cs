using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public abstract class EnemyParent : EarthEntityParent
{
    public float reloadTimer = 0;
    private float spinSpeed = 0;
    public GameObject bullet;
    public int hitPoints = 1;
    Vector3 rotationSpin;
    public override void Collected()
    {
        //Cannot be collected, but write to log to show it works
        Debug.Log("Enemy cannot be collected");
    }

    public override void FixedUpdate()
    {
        FindShip();
        CanSee();
        if (beamed)
        {
            TractorBeamed();
            spinSpeed += 2;
            rotationSpin = new Vector3(0, 0, spinSpeed * Time.deltaTime);
            transform.Rotate(rotationSpin);
        } else if (!grounded)
        {
            if (spinSpeed > 0) { spinSpeed -= 5; } else { spinSpeed = 0; }
            rotationSpin = new Vector3(0, 0, spinSpeed * Time.deltaTime);
            transform.Rotate(rotationSpin);
            WatchShip();
            CanShoot();
        }
        else
        {
            WatchShip();
            CanShoot();
            Move();
            reloadTimer -= Time.deltaTime;
        }
    }
    public void WatchShip()
    {
        if (Mathf.Sign(directionVector.x) == -1)
        {
            //Spaceship is on the left side
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            //Vector3 invertedDirectionVector = directionVector * new Vector3(-1, -1, -1);
            //gameObject.transform.GetChild(1).gameObject.GetComponent<Transform>().right = invertedDirectionVector;
            Transform arm = gameObject.transform.GetChild(1).gameObject.GetComponent<Transform>();
            arm.right = directionVector;
            Vector3 eu = arm.rotation.eulerAngles;
            arm.rotation = Quaternion.Euler(new Vector3(eu.x+180,eu.y,-eu.z));
        }
        else
        {
            //Spaceship is on the right side
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            gameObject.transform.GetChild(1).gameObject.GetComponent<Transform>().right = directionVector;
        }
    }

    public abstract void CanShoot();

    public abstract void Shoot();

    public override void Move()
    {
        if (!grounded)
        {
            moveVector.y += -0.1f;
        }
        else
        {
            if (canSee)
            {
                if (distance.magnitude > (detectionRange / 2))
                {
                    moveVector += new Vector2(Mathf.Sign(directionVector.x) * 0.05f, transform.position.y);
                }
                else
                {
                    moveVector += new Vector2(Mathf.Sign(directionVector.x) * -0.05f, transform.position.y);
                }
                if (Mathf.Abs(moveVector.x) > 1) { moveVector.x = Mathf.Sign(moveVector.x) * 1; }
            }
            else
            {
                if (dirTimer == 0)
                {
                    moveVector = ChooseDir();
                    dirTimer = UnityEngine.Random.Range(0, 4) * 60;
                }
                else
                {
                    dirTimer -= 1;
                }
            }
            moveVector.y = 0;
        }
        rb.velocity = moveVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Earth ground")
        {
            grounded = true;
            dirTimer = 0;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (collision.gameObject.name == "ShipBullet(Clone)")
        {
            hitPoints -= 1;
            if (hitPoints <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
