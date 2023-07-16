using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AsteroidSpawner : MonoBehaviour, ILevels
{
    public GameObject sAsteroid, mAsteroid, bAsteroid;
    public Transform spaceship;


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

        // Modefies timer and size with difficultyMultiplier
        int numberOfRocks = Mathf.FloorToInt(2 * difficultyMultiplier);

        if(1/spawnRate < timer)
        {
            for(int i = 0; i < numberOfRocks; i++)
            {
                float asteroidSize = Mathf.Sqrt(1.2f * Random.Range(0.0f, difficultyMultiplier));
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
            }
            timer = 0;
        }
        
    }

    // Spawns Asteroid
    void SpawnAsteroid(GameObject Asteroid)
    {
        (Vector3 asteroidPos, Vector3 moveVector) = CreateAttackVector(Asteroid.name);
        GameObject asteroidTransform = Instantiate(Asteroid, asteroidPos, Quaternion.identity);
        asteroidTransform.GetComponent<Asteroid>().Setup(moveVector);
    }

    // Returns start posision and moveVector for asteroid
    (Vector3, Vector3) CreateAttackVector(string asteroidType)
    {
        // Return values with default values
        Vector3 asteroidPos = spaceship.position + new Vector3(60f,0f,0f);
        Vector3 moveVector = new Vector3(-2*difficultyMultiplier,0,0);

        float moveDebuff = 1f;
        switch(asteroidType) 
        {
        case "BigAsteroid":
            moveDebuff = 0.2f;
            break;
        case "MediumAsteroid":
            moveDebuff = 0.4f;  
            break;
        }


        // Multiplies moveVector.x with difficulty multiplier
        asteroidPos.y += UnityEngine.Random.Range(-10.0f, 10); ;
        moveVector.x *= UnityEngine.Random.Range(0.75f, difficultyMultiplier) * moveDebuff;

        // Creates random y coordinate for y
        float yPos = UnityEngine.Random.Range(0.0f, 20.0f);  
        if(yPos == 0) 
        {
            return (asteroidPos, moveVector); // if yPos is 0 no values have to change
        }    

        // Creates offset on Y for moveVector
        float yOffset = Mathf.Pow(yPos/12, 2);
        float yMove = UnityEngine.Random.Range(0.0f, yOffset); 

        // Randomly makes astroid Y positive or negative
        float negativeMultiplier = Mathf.Pow(-1, UnityEngine.Random.Range(1,3));
        yPos *= negativeMultiplier;                
        yMove *= negativeMultiplier;

        asteroidPos.y += yPos;
        moveVector.y += yMove;
        return (asteroidPos, moveVector);
    }

    public void Level1()
    {
        difficultyMultiplier = 0.5f;
        spawnRate = 1;
    }

    public void Level2()
    {
        difficultyMultiplier = 1f;
        spawnRate = 2;
    }

    public void Level3()
    {
        difficultyMultiplier = 1.3f;
        spawnRate = 3;
    }

    public void Level4()
    {
        difficultyMultiplier = 1.7f;
        spawnRate = 4;
    }

    public void Level5()
    {
        difficultyMultiplier = 2f;
        spawnRate = 5;
    }

    public void CallLevel(int lv)
    {
        switch (lv)
        {
            case 1:
                Level1();
                break;
            case 2:
                Level2();
                break;
            case 3:
                Level3();
                break;
            case 4:
                Level4();
                break;
            case 5:
                Level5();
                break;
        }
    }
}
