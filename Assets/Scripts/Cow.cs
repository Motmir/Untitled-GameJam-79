using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class Cow : EarthEntityParent
{

    public override void Move()
    {
        if (!grounded)
        {
            moveVector.y += -0.15f;
        } else
        {
            if (canSee)
            {
                moveVector.x = distance.normalized.x * -1;
                moveVector.x *= Random.Range(6, 13) / 6;
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
    public override void FixedUpdate()
    {
        FindShip();
        CanSee();
        if (!grounded)
        {
            Vector3 rotationSpin = new Vector3(0, 0, 100 * Time.deltaTime);
            transform.Rotate(rotationSpin);
        }
        if (!beamed)
        {
            Move();
        }
        else
        {
            TractorBeamed();
        }
    }



    public override void Collected()
    {
        GameObject.Find("GameMaster").GetComponent<GameMaster>().IncreaseCows();
        Destroy(gameObject);
    }
    /*
        Component[] components = GameObject.Find("Cownter").GetComponents(typeof(Component));
        foreach (Component component in components)
        {
            Debug.Log(component.ToString());
        }
    */
}
