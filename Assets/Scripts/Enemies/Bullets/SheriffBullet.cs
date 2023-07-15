using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class SheriffBullet : ParentBullet
{

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        //Needs to implement damaging the ship
        if (collision.gameObject.name == "Spaceship")
        {
            GameObject.Find("GameMaster").GetComponent<GameMaster>().DecreaseCows();
            Destroy(gameObject);
        } else if (collision.gameObject.name.StartsWith("Cow"))
        {
            //Push the cow
            collision.gameObject.GetComponent<Cow>().moveVector = new Vector2(rb.velocity.x, collision.rigidbody.velocity.y);
            collision.gameObject.GetComponent<Cow>().dirTimer = 10000;
        }
    }

    public override void Setup(Vector3 Dir)
    {
        rb = transform.GetComponent<Rigidbody2D>();

        float rotation = Mathf.Rad2Deg * Mathf.Atan2(Dir.y, Dir.x);
        gameObject.transform.rotation = Quaternion.AngleAxis(rotation, Vector3.forward);
        rb.velocity = Dir * speed;
    }
}
