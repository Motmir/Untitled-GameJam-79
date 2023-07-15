using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ParentBullet : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected float speed = 3;
    public float duration = 5;

    public abstract void Setup(Vector3 Dir);

    public abstract void OnCollisionEnter2D(Collision2D collision);

    public void FixedUpdate()
    {
        duration -= Time.deltaTime;
        if (duration < 0)
        {
            Destroy(gameObject);
        }
    }
}
