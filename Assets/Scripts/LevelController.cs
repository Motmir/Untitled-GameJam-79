using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class LevelController : MonoBehaviour, ILevels
{
    private int cowsToSpawn, enemiesToSpawn, cowsToSpawnVal, enemiesToSpawnVal;
    private float cowTimer, enemyTimer, cowTimerVal, enemyTimerVal;
    GameObject cow, enemy;

    public void Awake()
    {
        Debug.Log("Starting level 1");
        Level1();
    }

    public float GetCowX()
    {
        float camPosX = GameObject.Find("PlayerCam").transform.position.x;
        float direction = Mathf.Pow(-1, Random.Range(1, 3));
        float posX = (direction * Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x) + (Random.Range(0, 30) * direction);
        return posX;
    }

    public float GetEnemyX()
    {
        GameObject cam = GameObject.Find("PlayerCam");
        float camPosX = cam.transform.position.x;
        float direction = Mathf.Pow(-1, Random.Range(1, 3));
        float posX = (direction * Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x)+ (Random.Range(0, 10) * direction);
        return posX;
    }

    public void FixedUpdate()
    {
        if (cowTimer < 0)
        {
            cowsToSpawn = cowsToSpawnVal;
            cowTimer = cowTimerVal;
            while (cowsToSpawn > 0)
            {
                Instantiate(cow, new Vector2(GetCowX(), -5), Quaternion.identity);
                cowsToSpawn--;
            }
        } else
        {
            cowTimer -= Time.deltaTime;
        }
        if (enemyTimer < 0)
        {
            enemiesToSpawn = enemiesToSpawnVal;
            enemyTimer = enemyTimerVal;
            while (enemiesToSpawn > 0)
            {
                Instantiate(enemy, new Vector2(GetEnemyX(), -5), Quaternion.identity);
                enemiesToSpawn--;
            }

        } else
        {
            enemyTimer -= Time.deltaTime;
        }

    }
    public void Level1()
    {
        cowsToSpawnVal = 5; enemiesToSpawnVal = 1; cowTimerVal = 10; enemyTimerVal = 60;
        cow = (GameObject)Resources.Load("Cow");
        enemy = (GameObject)Resources.Load("Farmer");
    }

    public void Level2()
    {
        cowsToSpawn = 15; enemiesToSpawn = 3; cowTimerVal = 10; enemyTimerVal = 60;
        cow = (GameObject)Resources.Load("Cow");
        enemy = (GameObject)Resources.Load("Farmer");
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
