using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AsteroidSpawner : MonoBehaviour
{
    public GameObject sAsteroid, mAsteroid, bAsteroid;

    /*
    Difficulty multiplier for astroid spawner
    Affects speed, size and rate
    */

    public float difficultyMultiplier, spawnRate;

    float timer = 0; 

    public void Awake()
    {
        sAsteroid = (GameObject)Resources.Load("SmallAsteroid");
        mAsteroid = (GameObject)Resources.Load("MediumAsteroid");
        bAsteroid = (GameObject)Resources.Load("BigAsteroid");
    }
    

    void FixedUpdate()
    {
        timer += Time.deltaTime;

        // Modefies timer and size with diffcultyMultiplier
        float timerOffset = UnityEngine.Random.Range(0.0f, 0.01f*difficultyMultiplier);
        float asteroidSize = Mathf.Sqrt(1.2f * UnityEngine.Random.Range(0.0f, difficultyMultiplier));

        if(spawnRate < timer + timerOffset)
        {
            if(asteroidSize > 2)
            {
                SpawnAsteroid(bAsteroid);
            }else if(asteroidSize > 1) 
            {
                SpawnAsteroid(mAsteroid);
            }else
            {
                SpawnAsteroid(sAsteroid);
            }
            timer = 0;
        }
        
    }

    void SpawnAsteroid(GameObject Asteroid)
    {
        (Vector3 asteroidPos, Vector3 moveVector) = CreateAttackVector(Asteroid.name);
        GameObject asteroidTransform = GameObject.Instantiate(Asteroid, asteroidPos, Quaternion.identity);
        asteroidTransform.GetComponent<Asteroid>().Setup(moveVector);
    }

    // Returns start posision and moveVector for asteroid
    (Vector3, Vector3) CreateAttackVector(string asteroidType)
    {
        // Return values with default values
        Vector3 asteroidPos = new Vector3(12,0,0);
        Vector3 moveVector = new Vector3(-3,0,0);

        float moveDebuff = 1f;
        switch(asteroidType) 
        {
        case "BigAsteroid":
            moveDebuff = 0.6f;
            break;
        case "MediumAsteroid":
            moveDebuff = 0.8f;  
            break;
        }


        // Multiplies moveVector.x with difficulty multiplier
        moveVector.x *= UnityEngine.Random.Range(0.75f, difficultyMultiplier) * moveDebuff;

        // Creates random y coordinate for y
        float yPos = UnityEngine.Random.Range(0.0f, 5.0f);  
        if(yPos == 0) 
        {
            return (asteroidPos, moveVector); // if yPos is 0 no values have to change
        }    

        // Creates offset on Y for moveVector
        float yOffset = Mathf.Sqrt(yPos/12);
        float yMove = UnityEngine.Random.Range(0.0f, yOffset); 

        // Randomly makes astroid Y positive or negative
        float negativeMultiplier = Mathf.Pow(-1, UnityEngine.Random.Range(1,3));
        yPos *= negativeMultiplier;                
        yMove *= negativeMultiplier;

        asteroidPos.y = yPos;
        moveVector.y = -yMove;
        return (asteroidPos, moveVector);
    }
}
