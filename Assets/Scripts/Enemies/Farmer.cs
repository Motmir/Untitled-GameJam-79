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

        for(int i = 0; i < 2; i++)
        {
            Vector3 offsetVector = directionVector;
            offsetVector.x += Random.Range(-0.15f, 0.15f);
            offsetVector.y += Random.Range(-0.15f, 0.15f);

            GameObject bulletTransform = Instantiate(bullet, bulletSpawn, Quaternion.identity);            
            bulletTransform.GetComponent<Farmerbullet>().Setup(offsetVector);
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
