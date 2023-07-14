using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Rigidbody2D rb;

    void Start()
    {
        rb.velocity = new Vector2(-5, 0);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}

