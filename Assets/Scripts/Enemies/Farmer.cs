using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;

public class Farmer : EnemyParent
{
    public void Awake()
    {
        bullet = (GameObject)Resources.Load("Farmer_Bullet");

    }
    public override void Shoot()
    {
        Vector2 bulletSpawn = (Vector2)transform.position + (directionVector / 2);
        for(int i = -1; i < 2; i+=2)
        {

            GameObject bulletTransform = Instantiate(bullet, bulletSpawn, Quaternion.identity);
            bulletTransform.GetComponent<Farmerbullet>().Setup(directionVector);

            Quaternion randomRotation = Quaternion.Euler(0, Random.Range(-5f, 5f), 0);
        }
    }

    public override void CanShoot()
    {   
        if (canSee)
        {
            if (reloadTimer < 0)
            {
                reloadTimer = 2;
                Shoot();
            }
        }
    }

}
