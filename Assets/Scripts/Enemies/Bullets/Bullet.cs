using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.VFX;

public class Bullet : ParentBullet
{
    public override void Setup(Vector3 Dir)
    {
        rb = transform.GetComponent<Rigidbody2D>();

        float rotation = Mathf.Rad2Deg * Mathf.Atan2(Dir.y, Dir.x);
        gameObject.transform.rotation = Quaternion.AngleAxis(rotation, Vector3.forward);
        rb.velocity = Dir * speed;
    }

    public override void FixedUpdate()
    {
        duration -= Time.deltaTime;
        if (duration < 0)
        {
            Destroy(gameObject);
        }
    }

    public override void CustomDelAnim()
    {
        GameObject effect = Instantiate(breakAnim, transform.position, Quaternion.identity);
    }
}
