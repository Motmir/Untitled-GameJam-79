using System;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance;
    public int cownter { private set; get; }

    public int spaceSceneGoalDist = 200, banked = 0, ammo = 3;
    private int level = 1;
    private float startPos, endPos;
    private bool levelTimerStarted = false;
    private float startLevelTime;
    private float remainingLevelTime;
    private GameObject spaceShip;

    private Transform sun;
    private Transform cam;
    private Light2D holyLight;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            UpdateCows();
            startPos = GameObject.Find("ProgressBar").GetComponent<RectTransform>().anchoredPosition.x + (Screen.width * 0.12f);
            endPos = Screen.width - (Screen.width * 0.12f);
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void UpdateCows()
    {
        GameObject.Find("Cownter").GetComponent<TextMeshProUGUI>().text = "Cownter: " + GameObject.Find("GameMaster").GetComponent<GameMaster>().cownter;
    }
    public void IncreaseCows(int cows)
    {
        cownter+=cows;
        UpdateCows();
    }

    public void DecreaseCows(int cows)
    {
        cownter-=cows;
        UpdateCows();
    }

    public void SwichScenes()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        levelTimerStarted = false;
        if (scene == 0)
        {
            banked = cownter;
            cownter = 0;
            SceneManager.LoadScene("Earth Scene");

        } else if (scene == 1)
        {
            SceneManager.LoadScene("Space");
        } else if (scene == 2)
        {
            cownter += banked;
            banked = 0;
            level++;
            SceneManager.LoadScene("Farm Scene");
        }
    }

    public void SpawnTheFuckingCows()
    {
        GameObject cow = (GameObject)Resources.Load("Cow_Mars");
        for (int i = 0; i < cownter; i++)
        {
            Instantiate(cow, new Vector3(UnityEngine.Random.Range(-4, 4), UnityEngine.Random.Range(-4, 4), 0), Quaternion.identity);
        }
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Earth Scene")
        {
            GameObject.Find("LevelManager").GetComponent<LevelController>().CallLevel(level);
        }
        else if (scene.name == "Space")
        {
            GameObject.Find("AsteroidController").GetComponent<AsteroidSpawner>().CallLevel(level);
        }
        else if (scene.name == "Farm Scene")
        {
            SpawnTheFuckingCows();
        }
    }


    public void FixedUpdate()
    {
        if (levelTimerStarted)
        {
            remainingLevelTime -= Time.deltaTime;
            if(remainingLevelTime <= 0)
            {
                SwichScenes();
            }
        }

        
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            float progress = (GameObject.Find("Spaceship").GetComponent<Transform>().position.x / spaceSceneGoalDist) * 100;
            if (progress >= 100)
            {
                progress = 100;
            }
            float currentPos = startPos + progress * ((endPos - startPos) / 100);
            GameObject.Find("Tracker").GetComponent<RectTransform>().position = new Vector3(currentPos, GameObject.Find("Tracker").GetComponent<RectTransform>().position.y, GameObject.Find("Tracker").GetComponent<RectTransform>().position.z);

            if (progress >= 100 && !levelTimerStarted)
            {
                startTimer(1);
            }
            
        }

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (!levelTimerStarted)
            {
                cam = GameObject.Find("PlayerCam").GetComponent<Transform>();
                sun = GameObject.Find("Sun").GetComponent<Transform>();
                holyLight = sun.GetComponent<Light2D>();
                startTimer(240);
            } else
            {
                float percent = remainingLevelTime / startLevelTime;
                float sunOffsetY = Mathf.Lerp(40,-5, 1 - percent);
                if(percent < 0.3f)
                {
                    holyLight.intensity = Mathf.Lerp(0.2f,1,percent/0.3f);
                }
                sun.transform.position = sun.transform.position = new Vector3(cam.position.x-5, cam.position.y-cam.position.y*0.2f+sunOffsetY, 5);
            }

        }
    }

    private void startTimer(int seconds)
    {
        startLevelTime = seconds;
        remainingLevelTime = seconds;
        levelTimerStarted = true;
    }

}
