using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using RocketTeam.Sdk.Services.Ads;
using TMPro;

public class RandomShop2 : MonoBehaviour
{
    public GameObject[] ImageVery;
    public GameObject[] ImageDisable;
    public GameObject[] ImageSelect;
    public GameObject[] ImageTick;
    public GameObject[] YourSelect;
    public List<int> YourSelect2;
    public Animator ai;
    public TMP_Text diamond;
    int indexBall;

    public GameObject[] ImageVerySpecial;
    public GameObject[] ImageDisableSpecial;
    public GameObject[] ImageSelectSpecial;
    public GameObject[] ImageTickSpecial;
    public GameObject[] YourSelectSpecial;
    public List<int> YourSelect2Special;
    public GameObject[] ImageAds;

    public GameObject[] weaponlistRight;
    public GameObject[] weaponlistLeft;

    public GameObject ListWeaponNormal;
    public GameObject ListWeaponSpecial;
    [Header("Character&Camera")]
    public GameObject Character;
    public GameObject MainCamera;

    [Header("GameStartScene")]
    public GameObject GameStartScene;

    [Header("CanvasDiamond")]
    public GameObject CanvasDiamond;

    [Header("CanvasPopUpDontOpen")]
    public GameObject CanvasPopUpDontOpen;
    private bool offing;

    [Header("ButtonRandom")]
    public Text TextButtonRandom;

    [Header("CanvasManager")]
    public CanvasManager CanvasManager;
    // Start is called before the first frame update
    public void Start()
    {
        DiamondText();
        OnWeaponNormal();
        CheckBallSpecial();
        checkBall();
        SetTextButtonRandom();
    }
    private void SetTextButtonRandom()
    {
        //hienthiButtonRandom
        if (!PlayerPrefs.HasKey("SoLanRandomWeapon"))
        {
            PlayerPrefs.SetInt("SoLanRandomWeapon", 0);
        }
        TextButtonRandom.text = ((PlayerPrefs.GetInt("SoLanRandomWeapon") * 1000) + 1500).ToString();
    }
    public void Update()
    {
        if (CanvasPopUpDontOpen.activeInHierarchy && !offing)
        {
            StartCoroutine(OffCanvasPopupDontOpen());
            offing = true;
        }
    }
    IEnumerator OffCanvasPopupDontOpen()
    {
        yield return new WaitForSeconds(1.5f);
        CanvasPopUpDontOpen.SetActive(false);
        offing = false;
    }
    public void DiamondText()
    {
        if (!PlayerPrefs.HasKey("diamond"))
        {
            PlayerPrefs.SetInt("diamond", 0);
        }
        else
        {
            diamond.text = (PlayerPrefs.GetInt("diamond")).ToFormatString();
        }
    }

    public void checkBall()
    {
        ImageDisable[0].SetActive(false);
        YourSelect2.Clear();
        for (int i = 0; i <= ImageDisable.Length - 1; i++)
        {
            ImageVery[i].SetActive(false);
            ImageSelect[i].SetActive(false);
            if (PlayerPrefs.GetInt("ball " + i) == 1)
            {
                ImageDisable[i].SetActive(false);
            }
            else
            {
                if (i != 0)
                {
                    YourSelect2.Add(i);
                }
            }
        }
        for (int i = 0; i <= ImageDisable.Length - 1; i++)
        {
            if (PlayerPrefs.GetString("CurrentWeapon") == weaponlistRight[i].name)
            {
                ImageTick[i].SetActive(true);
                weaponlistRight[i].SetActive(true);
                weaponlistLeft[i].SetActive(true);
                CheckBallSpecial();
            }
            else
            {
                ImageTick[i].SetActive(false);
                weaponlistRight[i].SetActive(false);
                weaponlistLeft[i].SetActive(false);
            }
        }
    }
    public void CheckBallSpecial()
    {
        //da mua vu khi gi theo ads
        for (int i = 0; i <= ImageAds.Length - 1; i++)
        {
            if (PlayerPrefs.GetInt("weaponSpecial" + i) < 2)
            {
                ImageAds[i].transform.Find("TextAds").gameObject.GetComponent<Text>().text = PlayerPrefs.GetInt("weaponSpecial" + i) + "/2";
            }
            else
            {
                ImageAds[i].SetActive(false);
                ImageDisableSpecial[i].SetActive(false);
            }
        }
        //da mua vu khi gi
        YourSelect2Special.Clear();
        for (int i = 0; i <= ImageDisableSpecial.Length - 1; i++)
        {
            ImageVerySpecial[i].SetActive(false);
            ImageSelectSpecial[i].SetActive(false);
            if (PlayerPrefs.GetInt("ballSpecial " + i) == 1)
            {
                ImageDisableSpecial[i].SetActive(false);
            }
            else
            {
                YourSelect2Special.Add(i);
            }
        }
        //

        //Check dag cam vu khi gi
        for (int i = 0; i <= ImageDisableSpecial.Length - 1; i++)
        {
            if (PlayerPrefs.GetString("CurrentWeapon") == weaponlistRight[i + 6].name)
            {
                ImageTickSpecial[i].SetActive(true);
                weaponlistRight[i + 6].SetActive(true);
                weaponlistLeft[i + 6].SetActive(true);
                checkBall();
            }
            else
            {
                ImageTickSpecial[i].SetActive(false);
                weaponlistRight[i + 6].SetActive(false);
                weaponlistLeft[i + 6].SetActive(false);
            }
        }
    }
    public void RandomButton()
    {
        if (YourSelect2.Count != 0)
        {
            //
            int price;
            if (!PlayerPrefs.HasKey("SoLanRandomWeapon"))
            {
                price = 1500;
                PlayerPrefs.SetInt("SoLanRandomWeapon", 0);
            }
            else
            {
                price = (PlayerPrefs.GetInt("SoLanRandomWeapon") * 1000) + 1500;
            }
            //
            int diamondCurrent = PlayerPrefs.GetInt("diamond");
            if (diamondCurrent >= price)
            {
                diamondCurrent -= price;
                diamond.text = diamondCurrent.ToFormatString();
                PlayerPrefs.SetInt("diamond", diamondCurrent);
                int random = Random.Range(1, YourSelect2.Count + 1);
                //
                indexBall = YourSelect2.ToArray()[random - 1];
                //
                StartCoroutine(Wait());
                ai.enabled = true;
                //CongSolanRandom
                int SolanRandom = PlayerPrefs.GetInt("SoLanRandomWeapon");
                SolanRandom += 1;
                PlayerPrefs.SetInt("SoLanRandomWeapon", SolanRandom);
                SetTextButtonRandom();
            }
            else
            {
                //Don`t enough money
                CanvasPopUpDontOpen.SetActive(true);
                CanvasPopUpDontOpen.transform.Find("Text").GetComponent<Text>().text = "Don`t enough money";
                //
            }
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);
        ai.enabled = false;
        for (int i = 0; i <= ImageDisable.Length - 1; i++)
        {
            ImageSelect[i].SetActive(false);
            ImageVery[i].SetActive(false);
            YourSelect[i].SetActive(true);
        }
        ImageDisable[indexBall].SetActive(false);
        ImageSelect[indexBall].SetActive(true);
        ImageVery[indexBall].SetActive(true);
        YourSelect[indexBall].SetActive(true);

        yield return new WaitForSeconds(0.5f);
        ImageSelect[indexBall].SetActive(false);
        ImageVery[indexBall].SetActive(false);
        YourSelect[indexBall].SetActive(true);
        PlayerPrefs.SetString("CurrentWeapon", weaponlistRight[indexBall].name);

        PlayerPrefs.SetInt("ball " + indexBall, 1);
        checkBall();
    }
    public void OnWeaponNormal()
    {
        ListWeaponNormal.SetActive(true);
        ListWeaponSpecial.SetActive(false);
    }
    public void OnWeaponSpecial()
    {
        ListWeaponNormal.SetActive(false);
        ListWeaponSpecial.SetActive(true);
    }
    public void ChangeWeaponSpecial(int value)
    {
        if (!ImageDisableSpecial[value].activeInHierarchy)
        {
            ActiveFalse();
            ImageTickSpecial[value].SetActive(true);
            weaponlistRight[value + 6].SetActive(true);
            weaponlistLeft[value + 6].SetActive(true);
            PlayerPrefs.DeleteKey("CurrentWeapon");
            PlayerPrefs.SetString("CurrentWeapon", weaponlistRight[value + 6].name);
        }
        else
        {
            CanvasPopUpDontOpen.SetActive(true);
            CanvasPopUpDontOpen.transform.Find("Text").GetComponent<Text>().text = "Unlock In ChestRoom";
            CheckBallSpecial();
        }
    }
    public void ChangeWeaponNormal(int value)
    {
        if (!ImageDisable[value].activeInHierarchy)
        {
            ActiveFalse();
            ImageTick[value].SetActive(true);
            weaponlistRight[value].SetActive(true);
            weaponlistLeft[value].SetActive(true);
            PlayerPrefs.DeleteKey("CurrentWeapon");
            PlayerPrefs.SetString("CurrentWeapon", weaponlistRight[value].name);
        }
    }
    public void ActiveFalse()
    {
        for (int i = 0; i < weaponlistRight.Length / 2; i++)
        {
            ImageTick[i].SetActive(false);
            ImageTickSpecial[i].SetActive(false);
            weaponlistRight[i].SetActive(false);
            weaponlistLeft[i].SetActive(false);
        }
        for (int i = weaponlistRight.Length / 2; i < weaponlistRight.Length; i++)
        {
            weaponlistRight[i].SetActive(false);
            weaponlistLeft[i].SetActive(false);
        }
    }
    public void BackScene()
    {
        transform.parent.gameObject.SetActive(false);
        GameStartScene.SetActive(true);
        Character.GetComponent<AutoSpin>().enabled = false;
        CanvasDiamond.SetActive(true);
        Character.GetComponent<ControllerPlayer>().ChangeCam("CamStart");
        Character.GetComponent<ControllerPlayer>().Save.GetComponent<Save>().ReadText();
        Character.GetComponent<ControllerPlayer>().Start();
        Character.GetComponent<ControllerPlayer>().RotateCharacter(0, 180, 0);
    }

    private bool adsShowing = false;
    public void WatchAds()
    {
        if (!GameManager.NetworkAvailable)
        {
            PopupNoInternet.Show();
            return;
        }

        if (adsShowing)
            return;


        if (!GameManager.EnableAds)
        {
            adsShowing = true;
            AdManager.Instance.ShowAdsReward(AddDiamondAds, "AddDiamondShopWeapon");
        }
#if !PROTOTYPE
        else
        {
            adsShowing = true;
            AdManager.Instance.ShowAdsReward(AddDiamondAds, "AddDiamondShopWeapon");
        }
#endif
    }
    private void AddDiamondAds(int value)
    {
        AnalyticManager.LogWatchAds("RewardDiamondInShopWeapon", 1);
        adsShowing = false;
        CanvasManager.DiamondFlyAdsReward(1500);
    }
}
