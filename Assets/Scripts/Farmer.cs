using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class Farmer : EarthEntityParent
{
    private float reloadTimer = 0;
    public GameObject pfBullet;
    public override void FixedUpdate()
    {
        FindShip();
        CanSee();
        if (!grounded)
        {
            Vector3 rotationSpin = new Vector3(0, 0, 100 * Time.deltaTime);
            transform.Rotate(rotationSpin);
        }
        if (!beamed)
        {
            reloadTimer -= Time.deltaTime;
            Move();
        } else
        {
            TractorBeamed();
        }
    }

    public void Awake()
    {
        pfBullet = (GameObject)Resources.Load("Farmer_Bullet");

    }
    public void Shoot()
    {
        Vector2 bulletSpawn = (Vector2)transform.position + (directionVector / 2);


        GameObject bulletTransform = GameObject.Instantiate(pfBullet, bulletSpawn, Quaternion.identity);
        bulletTransform.GetComponent<Farmerbullet>().Setup(directionVector);
    }

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
                if (reloadTimer < 0)
                {
                    reloadTimer = 2;
                    Shoot();
                }
                if (distance.magnitude > (detectionRange / 1.2))
                {
                    moveVector = new Vector2(Mathf.Sign(directionVector.x) * 2, transform.position.y);
                }
                else
                {
                    moveVector = new Vector2(Mathf.Sign(directionVector.x) * -2, transform.position.y);
                }
            }
            else
            {
                if (dirTimer == 0)
                {
                    moveVector = ChooseDir();
                    dirTimer = Random.Range(0, 4) * 60;
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

    public override void Collected()
    {
        //idk?
    }
}
