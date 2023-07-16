using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopClickToOpen : MonoBehaviour
{

    public GameObject shopCanvas;
    public AudioSource shopOpenSound;

    // when you click
    private void OnMouseDown()
    {
        shopOpenSound.Play();
        shopCanvas.SetActive(true);
    }

}
