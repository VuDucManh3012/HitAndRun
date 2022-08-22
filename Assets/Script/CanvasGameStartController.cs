using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasGameStartController : MonoBehaviour
{
    public int currentDiamond;

    public GameObject LevelUpdateEnoughDiamond;
    public GameObject LevelUpdateNoEnoughDiamond;
    public GameObject OfflineUpdateEnoughDiamond;
    public GameObject OfflineUpdateNoEnoughDiamond;
    public List<GameObject> ListObjectHidden;
    private bool Hiddened;
    [Header("RateUs")]
    public GameObject ButtonRateUs;
    [Header("Noads")]
    public GameObject ButtonNoAds;
    [Header("CanvasTouchPad")]
    public GameObject CanvasTouchPad;
    // Start is called before the first frame update
    void Start()
    {
        Hiddened = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (System.Int32.Parse(PlayerPrefs.GetString("stage")) <= 1 && !Hiddened)
        {
            foreach (GameObject item in ListObjectHidden)
            {
                item.SetActive(false);
            }
            Hiddened = true;
        }
        if (PlayerPrefs.HasKey("Rated"))
        {
            ButtonRateUs.SetActive(false);
        }
        else
        {
            ButtonRateUs.SetActive(true);
        }
        if (GameManager.Instance.Data.User.PurchasedNoAds)
        {
            ButtonNoAds.SetActive(false);
        }
        CanvasTouchPad.SetActive(true);
    }
    public void CheckSceneStart()
    {
        if (PlayerPrefs.HasKey("diamond"))
        {
            currentDiamond = System.Int32.Parse(PlayerPrefs.GetString("diamond"));
        }
        else
        {
            currentDiamond = 0;
        }
        if (currentDiamond >= PlayerPrefs.GetInt("UpdateLevel") * 50)
        {
            LevelUpdateEnoughDiamond.SetActive(true);
            LevelUpdateNoEnoughDiamond.SetActive(false);
        }
        else
        {
            LevelUpdateEnoughDiamond.SetActive(false);
            LevelUpdateNoEnoughDiamond.SetActive(true);
        }
        if (currentDiamond >= PlayerPrefs.GetInt("LevelOfflineReward") * 50)
        {
            OfflineUpdateEnoughDiamond.SetActive(true);
            OfflineUpdateNoEnoughDiamond.SetActive(false);
        }
        else
        {
            OfflineUpdateEnoughDiamond.SetActive(false);
            OfflineUpdateNoEnoughDiamond.SetActive(true);
        }
    }
}

