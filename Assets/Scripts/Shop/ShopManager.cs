using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    // https://www.youtube.com/watch?v=EEtOt0Jf7PQ&list=PL8K0QjCk8ZmhIocxRo6P90O55yDHGpPlb&index=3&t=628s

    private int cowins;
    public TMP_Text coinUI;
    public ShopItem[] shopItems;
    public ShopTemplate[] shopPanels;
    public GameObject[] shopPanelsGO;
    public Button[] myPurchaseBtns;
    public GameObject[] Images;
    public GameObject shopCanvas;
    

    void Start()
    {
        for (int i = 0; i < shopItems.Length; i++)
        {
            // Only the panles that has items are shown in runtime
            shopPanelsGO[i].SetActive(true);
        }
        Debug.Log(Mathf.FloorToInt(GameMaster.Instance.cownter / 10));
        coinUI.text = "COWins: " + Mathf.FloorToInt(GameMaster.Instance.cownter/10).ToString();
        LoadPanels(); 
        CheckPurchaseable();
    }

    // Generate the panles with correct information
    public void LoadPanels()
    {
        for (int i = 0; i < shopItems.Length; i++)
        {
            shopPanels[i].titleTxt.text = shopItems[i].title;
            shopPanels[i].descriptionTxt.text = shopItems[i].description;
            shopPanels[i].costTxt.text = "Coins: " + shopItems[i].baseCost.ToString();
            print(shopItems[i].artwork);
            Images[i].GetComponent<Image>().sprite = shopItems[i].artwork;
        }
    }

    public void CheckPurchaseable()
    {
        cowins = Mathf.FloorToInt(GameMaster.Instance.cownter / 10);
        for (int i = 0;i < shopItems.Length; i++)
        {
            
            if (cowins >= shopItems[i].baseCost) // if I have enough money
            {
                myPurchaseBtns[i].interactable = true;
            } else
            {
                myPurchaseBtns[i].interactable = false;
            }
        }
    }

    public void PurchaseItem(int btnNo)
    {
        if (cowins >= shopItems[btnNo].baseCost)
        {
            GameMaster.Instance.DecreaseCows(shopItems[btnNo].baseCost*10);
            coinUI.text = "Coins: " + cowins.ToString();
            CheckPurchaseable();
            // Unlock Item
            // TODO: Add this functionality
        }
    }

    public void ExitShop()
    {
        shopCanvas.SetActive(false);
        print("Buy shoppppp");
    }


}
