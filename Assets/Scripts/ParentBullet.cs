using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ParentBullet : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected float speed = 3;
    protected int duration;

    public abstract void Setup(Vector3 Dir);

    public abstract void OnTriggerEnter2D(Collider2D collider);
}
