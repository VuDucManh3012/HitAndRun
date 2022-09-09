using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpDontEnoughMoney : MonoBehaviour
{
    public GameObject CanvasWeaponShop;
    public GameObject CanvasSkinShop;
    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!CanvasWeaponShop.active && !CanvasSkinShop.active)
        {
            transform.gameObject.SetActive(false);
        }
    }
}
