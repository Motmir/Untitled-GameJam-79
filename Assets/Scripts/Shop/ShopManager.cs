using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    // https://www.youtube.com/watch?v=EEtOt0Jf7PQ&list=PL8K0QjCk8ZmhIocxRo6P90O55yDHGpPlb&index=3&t=628s


    public int coins;
    public TMP_Text coinUI;
    public ShopItem[] shopItems;
    public ShopTemplate[] shopPanels;
    public GameObject[] shopPanelsGO;
    public Button[] myPurchaseBtns;
    public GameObject shopCanvas;
    

    void Start()
    {
        for (int i = 0; i < shopItems.Length; i++)
        {
            // Only the panles that has items are shown in runtime
            shopPanelsGO[i].SetActive(true);
        }

        coinUI.text = "Coins: " + coins.ToString();
        LoadPanels(); 
        CheckPurchaseable();
    }

    void Update()
    {
        
    }

    // Press a button and get coins
    public void AddCoins()
    {
        coins++;
        coinUI.text = "Coins: " + coins.ToString();
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
        }
    }

    public void CheckPurchaseable()
    {
        for(int i = 0;i < shopItems.Length; i++)
        {
            if (coins >= shopItems[i].baseCost) // if I have enough money
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
        if (coins >= shopItems[btnNo].baseCost)
        {
            coins = coins - shopItems[btnNo].baseCost;
            coinUI.text = "Coins: " + coins.ToString();
            CheckPurchaseable();
            // Unlock Item
            // TODO: Add this functionality
        }
    }

    public void ExitShop()
    {
        shopCanvas.SetActive(false);
    }


}
