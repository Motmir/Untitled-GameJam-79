using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IEarthEntities
{
    abstract public void TractorBeamed();
    abstract public void CanSee();
    abstract public void FindShip();
    abstract public Vector2 ChooseDir();
    abstract public void FixedUpdate();
    abstract public void Move();
}
