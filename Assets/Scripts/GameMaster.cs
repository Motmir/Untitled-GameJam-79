using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance;
    public int cownter = 0, spaceSceneGoalDist = 1000;
    private int scene = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
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
            int progress = (int)GameObject.Find("Spaceship").GetComponent<Transform>().position.x * 100 / spaceSceneGoalDist;
            GameObject.Find("Precentage").GetComponent<TextMeshProUGUI>().text = progress + "%";
        }
    }


}
