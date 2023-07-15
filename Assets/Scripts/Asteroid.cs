using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : ParentBullet
{
    public override void Setup(Vector3 Dir)
    {
        rb = transform.GetComponent<Rigidbody2D>();
        float rotation = Mathf.Rad2Deg * Mathf.Atan2(Dir.y, Dir.x);
        gameObject.transform.rotation = Quaternion.AngleAxis(rotation, Vector3.forward);
        rb.velocity = Dir * speed;
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        //Still needs to implement damaging the ship
        if (collision.gameObject.name == "Spaceship")
        {
            Destroy(gameObject);
        }
    }

}

