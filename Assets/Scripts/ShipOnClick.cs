using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipOnClick : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnMouseDown()
    {
        GameMaster.Instance.SwichScenes();
    }
}
