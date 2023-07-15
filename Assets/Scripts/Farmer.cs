using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;

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
        WatchShip();
    }

    public void Awake()
    {
        pfBullet = (GameObject)Resources.Load("Farmer_Bullet");

    }
    public void WatchShip()
    {
        if (Mathf.Sign(directionVector.x) == -1)
        {
            GameObject.Find("Upper").GetComponent<SpriteRenderer>().flipY = true;
            GameObject.Find("Upper").GetComponent<SpriteRenderer>().flipX = false;
            GameObject.Find("Upper").GetComponent<Transform>().right = directionVector;
        } else
        {
            Vector3 invertedDirectionVector = directionVector * -1;
            GameObject.Find("Upper").GetComponent<SpriteRenderer>().flipY = false;
            GameObject.Find("Upper").GetComponent<SpriteRenderer>().flipX = true;
            GameObject.Find("Upper").GetComponent<Transform>().right = invertedDirectionVector;
        }
    }
    public void Shoot()
    {
        Vector2 bulletSpawn = (Vector2)transform.position + (directionVector / 2);


        GameObject bulletTransform = Instantiate(pfBullet, bulletSpawn, Quaternion.identity);
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
                if (distance.magnitude > (detectionRange / 2))
                {
                    moveVector += new Vector2(Mathf.Sign(directionVector.x) * 0.05f, transform.position.y);
                }
                else
                {
                    moveVector += new Vector2(Mathf.Sign(directionVector.x) * -0.05f, transform.position.y);
                }
                if (Mathf.Abs(moveVector.x) > 1) { moveVector.x = Mathf.Sign(moveVector.x) * 1; }
                if (reloadTimer < 1)
                {
                    if (reloadTimer < 0)
                    {
                        reloadTimer = 2;
                        Shoot();
                    }
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
