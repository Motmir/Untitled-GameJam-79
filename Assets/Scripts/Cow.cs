using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Cow : EarthEntityParent
{

    public void Moo()
    {   
        //Play audio clip
        throw new System.NotImplementedException();
    }

    public override void Move()
    {
        if (!grounded)
        {
            moveVector.y = -1.5f;
        } else
        {
            if (canSee)
            {
                moveVector.x = distance.normalized.x * -1;
                moveVector.x *= Random.Range(1, 11) / 6;
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
        if (!beamed)
        {
            Move();
        } else
        {
            TractorBeamed();
        }
    }

    public override void Collected()
    {
        GameObject.Find("GameMaster").GetComponent<GameMaster>().cownter++;
        Destroy(gameObject);
    }
}
