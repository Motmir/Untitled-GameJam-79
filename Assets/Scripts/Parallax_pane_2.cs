using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax_pane_2 : MonoBehaviour

{

    // https://www.youtube.com/watch?v=zit45k6CUMk

    private float length, startPosX, startPosY;
    public GameObject cam;
    public float parallaxEffect;


    // Start is called before the first frame update
    void Start()
    {
        // of the background image
        startPosX = transform.position.x;
        startPosY = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.x;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // how far we have moved from the start point 
        float distX = (cam.transform.position.x * parallaxEffect);
        float distY = (cam.transform.position.y * parallaxEffect);

        // move camera - only X
        //  transform.position = new Vector3(startPosX + distX, transform.position.y, transform.position.z);

        // Looping the background
        // how far we have moved relative to the camera
        float temp = (cam.transform.position.x * (1 - parallaxEffect));

        if (temp > startPosX + length)
        {
            print("temp: " + temp);
            print("Aaaaaa");
            print("org startPosX: " + startPosX);
            print("length: " + length);
            startPosX += 3 * length;
            print("new startPosX: " + startPosX);
        }
        else if (temp < startPosX - length)
        {
            startPosX -= length;
        }

        // move background
        transform.position = new Vector3(startPosX + distX, startPosY + distY, transform.position.z);


    }
}
