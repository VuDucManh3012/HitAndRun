using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasGift : MonoBehaviour
{
    public float currentTime;
    public Text TimeCountDown;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = PlayerPrefs.GetFloat("currenttime");
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        TimeCountDown.text = (currentTime/60).ToString();
        
    }
    public void closePopUp()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
