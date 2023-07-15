using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance;
    public int cownter = 0;
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


}
