using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject sAsteroid, mAsteroid, bAsteroid;

    // Timers for the spawn rate of different size asteroids
    public float smallAsteroidTimer, mediumAsteroidTimer, bigAsteroidTimer;

    public void Awake()
    {
        sAsteroid = (GameObject)Resources.Load("SmallAsteroid");
        mAsteroid = (GameObject)Resources.Load("MediumAsteroid");
        bAsteroid = (GameObject)Resources.Load("BigAsteroid");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        smallAsteroidTimer += Time.deltaTime;
        mediumAsteroidTimer += Time.deltaTime; 
        bigAsteroidTimer += Time.deltaTime;

        if(Mathf.Floor(smallAsteroidTimer) >= 5)
        {
            GameObject smallAsteroidTransform = GameObject.Instantiate(sAsteroid, new Vector2(12,0), Quaternion.identity);
            smallAsteroidTransform.GetComponent<Asteroid>().Setup(new Vector3(-3,0,0));
            smallAsteroidTimer = 0;
        }
        if(Mathf.Floor(mediumAsteroidTimer) >= 6)
        {
            GameObject mediumAsteroidTransform = GameObject.Instantiate(mAsteroid, new Vector2(12,0), Quaternion.identity);
            mediumAsteroidTransform.GetComponent<Asteroid>().Setup(new Vector3(-3,0,0));
            mediumAsteroidTimer = 0;
        }
        if(Mathf.Floor(bigAsteroidTimer) >= 7)
        {
            GameObject bigAsteroidTransform = GameObject.Instantiate(bAsteroid, new Vector2(12,0), Quaternion.identity);
            bigAsteroidTransform.GetComponent<Asteroid>().Setup(new Vector3(-3,0,0));
            bigAsteroidTimer = 0;
        }
    }
}
