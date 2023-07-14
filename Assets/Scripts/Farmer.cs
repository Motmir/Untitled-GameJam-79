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
        Move();
        reloadTimer -= Time.deltaTime;
    }

    public void Init()
    {
        pfBullet = (GameObject)Resources.Load("Farmer_bullet");

    }
    public void Shoot()
    {
        Vector2 bulletSpawn = (Vector2)transform.position + (directionVector / 2);


        GameObject bulletTransform = GameObject.Instantiate(pfBullet, bulletSpawn, Quaternion.identity);
        bulletTransform.GetComponent<Farmerbullet>().Setup(directionVector);
    }

    public override void Move()
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
            } else
            {
                moveVector = new Vector2(Mathf.Sign(directionVector.x) * -2, transform.position.y);
            }
        } else
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
        rb.velocity = moveVector;
    }
}
