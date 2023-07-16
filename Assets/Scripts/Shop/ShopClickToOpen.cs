using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopClickToOpen : MonoBehaviour
{

    public GameObject shopCanvas;

    // when you click
    private void OnMouseDown()
    {
        // capture the mouse offset
        // offset = cows.pos - mouse.pos
        // mousePositionOffset = gameObject.transform.position - GetMouseWorldPosition();
        shopCanvas.SetActive(true);
    }

}
