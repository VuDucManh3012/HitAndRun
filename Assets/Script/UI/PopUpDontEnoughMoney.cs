using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpDontEnoughMoney : MonoBehaviour
{
    public GameObject CanvasWeaponShop;
    public GameObject CanvasSkinShop;

    // Update is called once per frame
    void Update()
    {
        if (!CanvasWeaponShop.activeInHierarchy && !CanvasSkinShop.activeInHierarchy)
        {
            transform.gameObject.SetActive(false);
        }
    }
}
