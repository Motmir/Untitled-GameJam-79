using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : ParentBullet
{
    GameObject Spaceship; 
    public float adjustRate, timer = 0;

    public override void Setup(Vector3 Dir)
    {
        Spaceship = GameObject.Find("Spaceship");
        rb = transform.GetComponent<Rigidbody2D>();

        float rotation = Mathf.Rad2Deg * Mathf.Atan2(Dir.y, Dir.x);
        gameObject.transform.rotation = Quaternion.AngleAxis(rotation, Vector3.forward);
        rb.velocity = Dir * speed;
    }

    public void adjustDirection()
    {
        Transform bulletPos = gameObject.transform;
        Transform shipPos = Spaceship.transform;
        Vector2 Dir = (shipPos.position - bulletPos.position);
        Dir.Normalize();

        rb = transform.GetComponent<Rigidbody2D>();

        float rotation = Mathf.Rad2Deg * Mathf.Atan2(Dir.y, Dir.x);
        gameObject.transform.rotation = Quaternion.AngleAxis(rotation, Vector3.forward);
        rb.velocity = Dir * speed;
    }

    public override void FixedUpdate()
    {
        timer += Time.deltaTime;
        duration -= Time.deltaTime;

        if (duration < 0)
        {
            Destroy(gameObject);
        }else if( adjustRate < timer )
        {
            timer = 0;
            adjustDirection();
        }

    }

    public override void CustomDelAnim()
    {
        throw new System.NotImplementedException();
    }
}
