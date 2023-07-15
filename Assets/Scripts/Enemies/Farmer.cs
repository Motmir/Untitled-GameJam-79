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


        GameObject bulletTransform = Instantiate(bullet, bulletSpawn, Quaternion.identity);
        bulletTransform.GetComponent<Farmerbullet>().Setup(directionVector);
    }
}
