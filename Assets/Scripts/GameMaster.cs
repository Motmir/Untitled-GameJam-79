using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance;
    public int cownter = 0, spaceSceneGoalDist = 1000;
    private int scene = 0;
    private float startPos, endPos, barSize;

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
            barSize = GameObject.Find("ProgressBar").GetComponent<RectTransform>().rect.width;
            startPos = GameObject.Find("ProgressBar").GetComponent<RectTransform>().anchoredPosition.x + 155;
            endPos = startPos + barSize - 75;
        } 
    }

    public void IncreaseCows()
    {
        cownter++;
        GameObject.Find("Cownter").GetComponent<TextMeshProUGUI>().text = "Cows: " + GameObject.Find("GameMaster").GetComponent<GameMaster>().cownter;
    }

    public void DecreaseCows()
    {
        cownter--;
        GameObject.Find("Cownter").GetComponent<TextMeshProUGUI>().text = "Cows: " + GameObject.Find("GameMaster").GetComponent<GameMaster>().cownter;
    }

    public void SwichScenes()
    {
        scene = (scene + 1) % SceneManager.sceneCountInBuildSettings;

        if(scene == 0)
        {
            SceneManager.LoadScene("Earth Scene");
        } else if (scene == 1)
        {
            SceneManager.LoadScene("Farm Scene");
        } else if (scene == 2)
        {
            SceneManager.LoadScene("Space");
        }
    }

    public void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            Debug.Log(GameObject.Find("Spaceship").GetComponent<Transform>().position.x + " : " + spaceSceneGoalDist);
            float progress = (GameObject.Find("Spaceship").GetComponent<Transform>().position.x / spaceSceneGoalDist) * 100;
            Debug.Log(progress);
            float currentPos = startPos + progress * ((endPos - startPos) / 100);
            GameObject.Find("Tracker").GetComponent<RectTransform>().position = new Vector3(currentPos, GameObject.Find("Tracker").GetComponent<RectTransform>().position.y, GameObject.Find("Tracker").GetComponent<RectTransform>().position.z);
        }
    }


}
