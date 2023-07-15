using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour, ILevels
{
    private int cowsToSpawn, enemiesToSpawn;

    public void Awake()
    {
        Debug.Log("Starting level 1");
        Level1();
    }
    public void Level1()
    {
        cowsToSpawn = 5; enemiesToSpawn = 1;
        GameObject cow = (GameObject)Resources.Load("Cow");
        GameObject enemy = (GameObject)Resources.Load("Farmer");
        while (cowsToSpawn > 0) {
            GameObject.Instantiate(cow, new Vector2(Random.Range(-10, 10), 0), Quaternion.identity);
            cowsToSpawn--;
        }
        while (enemiesToSpawn > 0)
        {
            GameObject.Instantiate(enemy, new Vector2(Random.Range(-10, 10), 0), Quaternion.identity);
            enemiesToSpawn--;
        }
    }

    public void Level2()
    {
        throw new System.NotImplementedException();
    }

    public void Level3()
    {
        throw new System.NotImplementedException();
    }

    public void Level4()
    {
        throw new System.NotImplementedException();
    }

    public void Level5()
    {
        throw new System.NotImplementedException();
    }
}
