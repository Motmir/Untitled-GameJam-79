using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmer : EarthEntityParent
{
    public override void FixedUpdate()
    {
        FindShip();
        CanSee();
        Move();
    }

    public override void Move()
    {
        if (canSee)
        {
            
        } else
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
        rb.velocity = moveVector;
    }
}
