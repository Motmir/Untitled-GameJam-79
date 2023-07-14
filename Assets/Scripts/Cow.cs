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
        if (canSee)
        {
            moveVector.x = distance.normalized.x * -1;
            moveVector.x *= Random.Range(1, 11) / 6;
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
            Move();
        }
    }

    public override void Collected()
    {
        GameObject.Find("GameMaster").GetComponent<GameMaster>().cownter++;
        Destroy(this.gameObject);
    }
}
