using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Cow : EarthEntityParent, ICow 
{

    private int dirTimer = 0;
    private bool beamed = false;

    public void Moo()
    {   
        //Play audio clip
        throw new System.NotImplementedException();
    }

    public void TractorBeamed()
    {
        //If this method is called it means the cow has been hit by the tractor beam.

        moveVector = directionVector;
        rb.velocity = moveVector;
        //throw new System.NotImplementedException();
    }

    public void Walk()
    {
        if (canSee)
        {
            moveVector.x = distance.normalized.x * -1;
            moveVector.x *= Random.Range(1, 11) / 6;
        } else
        {

        }
        if (dirTimer == 0)
        {
            moveVector = ChooseDir();
            dirTimer = Random.Range(0, 4) * 60;
        } else
        {
            dirTimer -= 1;
        }
        rb.velocity = moveVector;
        //throw new System.NotImplementedException();
    }

    private Vector2 ChooseDir()
    {
        Vector2 dir = new Vector2(Random.Range(-10, 10), 0);
        dir.x /= 7;
        return dir;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void FixedUpdate()
    {
        FindShip();
        CanSee();
        if (!beamed)
        {
            Walk();
        }
    }
}
