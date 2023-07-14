using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
 
{

    // https://www.youtube.com/watch?v=zit45k6CUMk

    private float length, startPosX, startPosY;
    public GameObject cam;
    public float parallaxEffect;


        // Start is called before the first frame update
        void Start()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;
        length = 1 * GetComponent<SpriteRenderer>().bounds.size.x;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // how far we have moved from the start point 
        float distX = (cam.transform.position.x * parallaxEffect);
        float distY = (cam.transform.position.y * parallaxEffect);
        // move camera 
         transform.position = new Vector3(startPosX + distX, startPosY + distY, transform.position.z);
        // move camera - only X
      //  transform.position = new Vector3(startPosX + distX, transform.position.y, transform.position.z);

        // Looping the background
        // how far we have moved relative to the camera
        float temp = (cam.transform.position.x * (1 - parallaxEffect));

        if (temp > startPosX + length)
        {
            startPosX += length;
        } else if (temp < startPosX - length) {
            startPosX -= length;
        }


    }
}
