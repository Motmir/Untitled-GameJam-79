using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceBullet : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected float speed = 3;
    public float duration = 5;
    public GameObject breakAnim;
    public void CustomDelAnim()
    {
        throw new System.NotImplementedException();
    }

    public void FixedUpdate()
    {
        duration -= Time.deltaTime;
        if (duration < 0)
        {
            Destroy(gameObject);
        }
    }

    public void Setup(Vector3 Dir)
    {
        rb = transform.GetComponent<Rigidbody2D>();

        float rotation = Mathf.Rad2Deg * Mathf.Atan2(Dir.y, Dir.x);
        gameObject.transform.rotation = Quaternion.AngleAxis(rotation, Vector3.forward);
        rb.velocity = Dir * 10;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.EndsWith("Asteroid(Clone)") == true)
        {
            //CustomDelAnim();
            
        }
        Destroy(gameObject);
    }
}
