using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmerbullet : MonoBehaviour
{
    public int damage = 1, speed = 3;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    public void Setup(Vector2 Dir)
    {
        rb = transform.GetComponent<Rigidbody2D>();

        float rotation = Mathf.Rad2Deg * Mathf.Atan2(Dir.y, Dir.x);
        gameObject.transform.rotation = Quaternion.AngleAxis(rotation, Vector3.forward);
        Debug.Log(Dir);
        rb.velocity = Dir * speed;
    }
}
