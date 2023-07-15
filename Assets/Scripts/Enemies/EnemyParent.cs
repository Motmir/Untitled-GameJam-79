using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyParent : EarthEntityParent
{
    public float reloadTimer = 0;
    public GameObject bullet;
    public override void Collected()
    {
        //Cannot be collected, but write to log to show it works
        Debug.Log("Enemy cannot be collected");
    }

    public override void FixedUpdate()
    {
        FindShip();
        CanSee();
        if (!grounded)
        {
            Vector3 rotationSpin = new Vector3(0, 0, 100 * Time.deltaTime);
            transform.Rotate(rotationSpin);
        }
        else
        {
            WatchShip();
        }
        if (!beamed)
        {
            reloadTimer -= Time.deltaTime;
            Move();
        }
        else
        {
            TractorBeamed();
        }
    }
    public void WatchShip()
    {
        if (Mathf.Sign(directionVector.x) == -1)
        {
            //Spaceship is on the left side
            GameObject.Find("Upper").GetComponent<SpriteRenderer>().flipX = true;
            //GameObject.Find("Upper").GetComponent<Transform>().right = directionVector;
            Vector3 invertedDirectionVector = directionVector * new Vector3(-1, -1, 0);
            GameObject.Find("Upper").GetComponent<Transform>().right = invertedDirectionVector;
        }
        else
        {
            //Spaceship is on the right side
            GameObject.Find("Upper").GetComponent<SpriteRenderer>().flipX = false;
            GameObject.Find("Upper").GetComponent<Transform>().right = directionVector;
        }
    }

    public abstract void Shoot();

    public override void Move()
    {
        if (!grounded)
        {
            moveVector.y += -0.1f;
        }
        else
        {
            if (canSee)
            {
                if (distance.magnitude > (detectionRange / 2))
                {
                    moveVector += new Vector2(Mathf.Sign(directionVector.x) * 0.05f, transform.position.y);
                }
                else
                {
                    moveVector += new Vector2(Mathf.Sign(directionVector.x) * -0.05f, transform.position.y);
                }
                if (Mathf.Abs(moveVector.x) > 1) { moveVector.x = Mathf.Sign(moveVector.x) * 1; }
                if (reloadTimer < 1)
                {
                    if (reloadTimer < 0)
                    {
                        reloadTimer = 2;
                        Shoot();
                    }
                }
            }
            else
            {
                if (dirTimer == 0)
                {
                    moveVector = ChooseDir();
                    dirTimer = Random.Range(0, 4) * 60;
                }
                else
                {
                    dirTimer -= 1;
                }
            }
            moveVector.y = 0;
        }
        rb.velocity = moveVector;
    }
}
