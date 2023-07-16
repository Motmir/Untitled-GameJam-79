using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;

public class Sheriff : EnemyParent
{
    public float burstTimer = 0;
    public int bursts = 0;

    public void Awake()
    {
        bullet = (GameObject)Resources.Load("Bullet");

    }

    public override void BeamedAudio()
    {
        throw new System.NotImplementedException();
    }

    public override void CanShoot()
    {
        if (canSee)
        {
            if (reloadTimer < 0)
            {
                if(burstTimer < 0)
                {
                    Shoot();
                    burstTimer = 0.4f;
                    bursts++;
                    if(bursts > 2) 
                    {
                        reloadTimer = 2;
                        burstTimer = 0;
                        bursts = 0;
                    }
                }
                burstTimer -= Time.deltaTime;   
            }
        }
    }

    public override void Shoot()
    {        
        Vector2 bulletSpawn = (Vector2)transform.position + (directionVector / 2);

        GameObject bulletTransform = Instantiate(bullet, bulletSpawn, Quaternion.identity);
        
        directionVector *= 2;
        bulletTransform.GetComponent<Bullet>().Setup(directionVector);
    }
}
