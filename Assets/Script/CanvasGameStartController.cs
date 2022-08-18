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
    public GameObject NoAds;
    public List<GameObject> ListObjectHidden;
    private bool Hiddened;
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

