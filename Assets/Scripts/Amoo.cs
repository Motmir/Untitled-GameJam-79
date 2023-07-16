using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Amoo : MonoBehaviour
{
    public bool full = true, canReload = true;
    public float fill = 1, reloadTimer = 0, reloadTimerVal = 3;
    
    public void Use()
    {
        full = false;
        reloadTimer = reloadTimerVal;
    }

    public void FixedUpdate()
    {
        if (canReload)
        {
            reloadTimer -= Time.deltaTime;
        }
        if (reloadTimer <= 0)
        {
            canReload = false;
            full = true;
            fill = 1;
        } else
        {
            fill = 1 - (reloadTimer / reloadTimerVal);
        }
        gameObject.transform.GetChild(0).GetComponentInChildren<Image>().fillAmount = fill;
    }
}
