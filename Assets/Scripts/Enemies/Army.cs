using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;

public class Army : EnemyParent
{
    public void Awake()
    {
        bullet = (GameObject)Resources.Load("Army_Bullet");

    }

    public override void Shoot()
    {        
        Debug.Log("Pew Pew Pew");
        Vector2 bulletSpawn = (Vector2)transform.position + (directionVector / 2);

        GameObject bulletTransform = Instantiate(bullet, bulletSpawn, Quaternion.identity);

        directionVector *= 0.5f;

        bulletTransform.GetComponent<Farmerbullet>().Setup(directionVector);
    }

    public override void CanShoot()
    {   
        if (canSee)
        {
            if (reloadTimer < 0)
            {
                reloadTimer = 5;
                Shoot();
            }
        }
    }

}
