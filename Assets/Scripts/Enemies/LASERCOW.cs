using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEditor;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.Animations;

public class LASERCOW : EarthEntityParent
{    
    public float reloadTimer, shootTimer, aimTimer = 0;
    private float spinSpeed = 0;
    private bool shooting, aiming;
    public GameObject bullet, aimbullet, bulletObject;
    Vector3 rotationSpin;
    Vector2 LaserStart, LaserFinish;

    public override void Collected()
    {
        //Cannot be collected, but write to log to show it works
        Debug.Log("Enemy cannot be collected");
    }

    public override void FixedUpdate()
    {

        if(shooting)
        {
            shootTimer -= Time.deltaTime;
            if(shootTimer < 0)
            {
                reload();
            }else if(bulletObject == null)
            {
                reload();
            }else{
                return;
            }
        }
        FindShip();
        CanSee();
        WatchShip();
        CanShoot();
        
        if(aiming)
        {
            aimTimer -= Time.deltaTime;
            if(aimTimer < 0)
            {
                aiming = false;
                LaserFinish = directionVector;
                Aim(LaserFinish);
                
                Shoot();
                shooting = true;
                shootTimer = 1;
            } 
        }else if(!grounded)
        {
            if (spinSpeed > 0) { spinSpeed -= 5; } else { spinSpeed = 0; }
            rotationSpin = new Vector3(0, 0, spinSpeed * Time.deltaTime);
            transform.Rotate(rotationSpin);
        }
        else
        {
            Move();
            reloadTimer -= Time.deltaTime;
        }
    }


    public void Awake()
    {
        bullet = (GameObject)Resources.Load("LASER");
        aimbullet = (GameObject)Resources.Load("LASERAIM");
    }

    public void reload()
    {
        shooting = false;
    }

    public void chargeLaser()
    {
        LaserStart = directionVector;
        Aim(LaserStart);
        aimTimer = 1;
        aiming = true;
    }

    public void Aim(Vector2 aimVector)
    {
        Vector2 bulletSpawn = (Vector2)transform.position + (aimVector / 2) + new Vector2(0, -0.2f);

        GameObject bulletTransform = Instantiate(aimbullet, bulletSpawn, Quaternion.identity);
        bulletObject = bulletTransform;
        bulletTransform.GetComponent<LASERAIM>().Setup(aimVector);
    }

    public void Shoot()
    {   
        Vector2 bulletSpawn = (Vector2)transform.position + (LaserStart / 2) + new Vector2(0, -0.2f);

        GameObject bulletTransform = Instantiate(bullet, bulletSpawn, Quaternion.identity);
        bulletObject = bulletTransform;
        bulletTransform.GetComponent<LASER>().setFinishVector(LaserFinish);
        bulletTransform.GetComponent<LASER>().Setup(LaserStart);
        
    }

    public void CanShoot()
    {   
        if (canSee)
        {
            if (reloadTimer < 0)
            {
                reloadTimer = 3;
                chargeLaser();
            }
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
            gameObject.transform.GetChild(1).gameObject.GetComponent<Transform>().right = directionVector   ;
        }
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
}