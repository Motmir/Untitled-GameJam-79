using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance;
    public int cownter = 0, spaceSceneGoalDist = 1000;
    private int scene = 0;
    private float startPos, endPos, barSize;
    private bool levelTimerStarted = false;
    private float remainingLevelTime;

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
            barSize = GameObject.Find("ProgressBar").GetComponent<RectTransform>().rect.width;
            startPos = GameObject.Find("ProgressBar").GetComponent<RectTransform>().anchoredPosition.x + 155;
            endPos = startPos + barSize - 75;
        } 
    }
    public void UpdateCows()
    {
        GameObject.Find("Cownter").GetComponent<TextMeshProUGUI>().text = "Cownter: " + GameObject.Find("GameMaster").GetComponent<GameMaster>().cownter;
    }
    public void IncreaseCows()
    {
        cownter++;
        UpdateCows();
    }

    public void DecreaseCows()
    {
        cownter--;
        UpdateCows();
    }

    public void SwichScenes()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;

        if (scene == 0)
        {
            SceneManager.LoadScene("Earth Scene");
        } else if (scene == 1)
        {
            SceneManager.LoadScene("Space");
        } else if (scene == 2)
        {
            SceneManager.LoadScene("Farm Scene");
        }
    }

    public void FixedUpdate()
    {
        if (levelTimerStarted)
        {
            remainingLevelTime -= Time.deltaTime;
            if(remainingLevelTime < 0)
            {
                SwichScenes();
            }
        }



        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            float progress = (GameObject.Find("Spaceship").GetComponent<Transform>().position.x / spaceSceneGoalDist) * 100;
            float currentPos = startPos + progress * ((endPos - startPos) / 100);
            GameObject.Find("Tracker").GetComponent<RectTransform>().position = new Vector3(currentPos, GameObject.Find("Tracker").GetComponent<RectTransform>().position.y, GameObject.Find("Tracker").GetComponent<RectTransform>().position.z);

            if (progress >= 100 && !levelTimerStarted)
            {
                levelTimerStarted = true;
                remainingLevelTime = 10;
            }
            
        }
    }


}
