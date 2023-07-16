using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    Vector3 mousePositionOffset;

    private Vector3 GetMouseWorldPosition()
    {
        // Capture mouse position and return WorldPoint
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }


    // when you click
    private void OnMouseDown()
    {
        // capture the mouse offset
        // offset = cows.pos - mouse.pos
        mousePositionOffset = gameObject.transform.position - GetMouseWorldPosition();
    }


    // when you hold and drag
    private void OnMouseDrag() 
    {
        transform.position = GetMouseWorldPosition() + mousePositionOffset; 
        
    }
}
