using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ParentBullet : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected float speed = 3;
    public float duration = 5;
    public GameObject breakAnim;

    public abstract void Setup(Vector3 Dir);
    public abstract void FixedUpdate();

    public abstract void CustomDelAnim();

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Spaceship")
        {
            GameObject.Find("GameMaster").GetComponent<GameMaster>().DecreaseCows();
            CustomDelAnim();
            Destroy(gameObject);
        }
    }

    
}
