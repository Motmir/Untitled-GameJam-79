using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class Cow : EarthEntityParent
{
    public void Moo()
    {   
        //Play audio clip
        throw new System.NotImplementedException();
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
