using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using RocketTeam.Sdk.Services.Ads;
using TMPro;

public class RandomSkin : MonoBehaviour
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
    public GameObject[] ImageAds;

    public GameObject[] WeaponSpecial;
    public Texture[] textSkin;

    private Renderer SkinRenderer;
    private Renderer SkinArmorRenderer;
    public GameObject ModelCharacter;
    public GameObject ModelArmor;

    public int skinIndex;
    public GameObject ListSkinNormal;
    public GameObject ListSkinSpecial;

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
        SkinRenderer = ModelCharacter.GetComponent<Renderer>();
        SkinArmorRenderer = ModelArmor.GetComponent<Renderer>();
        DiamondText();
        checkBall();
        checkBallSpecial();
        OnSkinNormal();
        ModelArmor.SetActive(true);
        adsShowing = false;
        SetTextButtonRandom();
    }
    private void SetTextButtonRandom()
    {
        //hienthiButtonRandom
        if (!PlayerPrefs.HasKey("SoLanRandomSkin"))
        {
            PlayerPrefs.SetInt("SoLanRandomSkin", 0);
        }
        TextButtonRandom.text = ((PlayerPrefs.GetInt("SoLanRandomSkin") * 1000) + 1500).ToString();
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
        //Set image Disable
        ImageDisable[0].SetActive(false);
        YourSelect2.Clear();
        for (int i = 0; i <= ImageDisable.Length - 1; i++)
        {
            ImageVery[i].SetActive(false);
            ImageSelect[i].SetActive(false);
            if (PlayerPrefs.GetInt("skin " + i) == 1)
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
        //Set skin
        for (int i = 0; i <= ImageDisable.Length - 1; i++)
        {
            if (PlayerPrefs.GetString("CurrentSkin") == textSkin[i].name)
            {
                ImageTick[i].SetActive(true);
                skinIndex = i;
                ChangeSkinUpdate();
            }
            else if (PlayerPrefs.GetString("CurrentSkin") == "")
            {
                ImageTick[0].SetActive(true);
                skinIndex = 0;
                ChangeSkinUpdate();
                return;
            }
            else
            {
                ImageTick[i].SetActive(false);
            }
        }
    }
    public void checkBallSpecial()
    {
        //da mua gi theo ads
        for (int i = 0; i <= ImageAds.Length - 1; i++)
        {
            if (PlayerPrefs.GetInt("skinSpecial" + i) < 2)
            {
                ImageAds[i].transform.Find("TextAds").gameObject.GetComponent<Text>().text = PlayerPrefs.GetInt("skinSpecial" + i) + "/2";
            }
            else
            {
                ImageAds[i].SetActive(false);
                ImageDisableSpecial[i].SetActive(false);
            }
        }

        //dag o skin nao
        for (int i = 0; i <= ImageDisableSpecial.Length - 1; i++)
        {
            if (PlayerPrefs.GetString("CurrentSkin") == textSkin[i + 6].name)
            {
                ImageTickSpecial[i].SetActive(true);
                skinIndex = i + 6;
                ChangeSkinUpdate();
            }
            else
            {
                ImageTickSpecial[i].SetActive(false);
            }
        }
    }
    public void RandomButton()
    {
        if (YourSelect2.Count != 0)
        {
            //
            int price;
            if (!PlayerPrefs.HasKey("SoLanRandomSkin"))
            {
                price = 1500;
                PlayerPrefs.SetInt("SoLanRandomSkin", 0);
            }
            else
            {
                price = (PlayerPrefs.GetInt("SoLanRandomSkin") * 1000) + 1500;
            }
            //
            int currentDiamond = PlayerPrefs.GetInt("diamond");
            if (currentDiamond >= price)
            {
                currentDiamond -= price;
                diamond.text = currentDiamond.ToFormatString();
                PlayerPrefs.SetInt("diamond", currentDiamond);
                //
                int random = Random.Range(1, YourSelect2.Count + 1);
                //
                indexBall = YourSelect2.ToArray()[random - 1];
                //
                ai.enabled = true;
                StartCoroutine(Wait());
                //CongSolanRandom
                int SolanRandom = PlayerPrefs.GetInt("SoLanRandomSkin");
                SolanRandom += 1;
                PlayerPrefs.SetInt("SoLanRandomSkin", SolanRandom);
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
        PlayerPrefs.SetString("CurrentSkin", textSkin[indexBall].name);

        PlayerPrefs.SetInt("skin " + indexBall, 1);
        checkBall();
    }
    public void ChangeSkinSpecial(int value)
    {
        ActiveFalse();
        if (!ImageDisableSpecial[value].activeInHierarchy)
        {
            ImageTickSpecial[value].SetActive(true);
            PlayerPrefs.SetString("CurrentSkin", textSkin[value + 6].name);
            skinIndex = value + 6;
            ChangeSkinUpdate();
        }
        else
        {
            ///Ads
            WatchAdsToAddSkin(value);
        }
        checkBallSpecial();
    }
    public void ChangeSkinNormal(int value)
    {
        ActiveFalse();
        if (!ImageDisable[value].activeInHierarchy)
        {
            ImageTick[value].SetActive(true);
            PlayerPrefs.SetString("CurrentSkin", textSkin[value].name);
            skinIndex = value;
            ChangeSkinUpdate();
        }
        else
        {
            checkBall();
        }
        checkBallSpecial();
    }
    public void ActiveFalse()
    {
        for (int i = 0; i <= ImageDisable.Length - 1; i++)
        {
            ImageTick[i].SetActive(false);
        }
    }
    public void BackScene()
    {
        ControllerPlayer controllerPlayer = Character.GetComponent<ControllerPlayer>();
        ModelArmor.SetActive(false);
        transform.parent.gameObject.SetActive(false);
        GameStartScene.SetActive(true);
        Character.GetComponent<AutoSpin>().enabled = false;
        CanvasDiamond.SetActive(true);
        controllerPlayer.ChangeCam("CamStart");
        controllerPlayer.Start();
        controllerPlayer.SetSkin2();
        controllerPlayer.Save.GetComponent<Save>().ReadText();
        controllerPlayer.RotateCharacter(0, 180, 0);
    }
    public void ChangeSkinUpdate()
    {
        SkinRenderer.material.mainTexture = textSkin[skinIndex];
        SkinArmorRenderer.material.mainTexture = textSkin[skinIndex];
        if (skinIndex >= 0)
        {
            foreach (GameObject item in WeaponSpecial)
            {
                item.SetActive(false);
            }
            WeaponSpecial[skinIndex].SetActive(true);
        }
        else
        {
            foreach (GameObject item in WeaponSpecial)
            {
                item.SetActive(false);
            }
        }
    }
    public void OnSkinNormal()
    {
        ListSkinNormal.SetActive(true);
        ListSkinSpecial.SetActive(false);
    }
    public void OnSkinSpecial()
    {
        ListSkinNormal.SetActive(false);
        ListSkinSpecial.SetActive(true);
    }
    public bool adsShowing;

    /// //////////////////////////////////////////////////////////////////////////////////////
    public void WatchAdsToAddDiamond()
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
            AdManager.Instance.ShowAdsReward(OnCompleteAdsAddDiamond, "AddDiamondInSkinShop");
        }
#if !PROTOTYPE
        else
        {
            adsShowing = true;
            AdManager.Instance.ShowAdsReward(OnCompleteAdsAddDiamond, "AddDiamondInSkinShop");
        }
#endif
    }
    private void OnCompleteAdsAddDiamond(int value)
    {
        AnalyticManager.LogWatchAds("RewardDiamondInShopSkin", 1);
        adsShowing = false;
        CanvasManager.DiamondFlyAdsReward(1500);
    }
    /// ///////////////////////////////////////////////////////////////////////////////////
    public void WatchAdsToAddSkin(int indexSkin)
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
            indexSkinSpecial = indexSkin;
            AdManager.Instance.ShowAdsReward(OnCompleteAdsToAddSkin, "AddSkin");
        }
#if !PROTOTYPE
        else
        {
            adsShowing = true;
            indexSkinSpecial = indexSkin;
            AdManager.Instance.ShowAdsReward(OnCompleteAdsToAddSkin, "AddSkin");
        }
#endif
    }
    private int indexSkinSpecial = 10;
    private void OnCompleteAdsToAddSkin(int value)
    {
        AnalyticManager.LogWatchAds("GetSkinSpecial " + indexSkinSpecial, 1);
        adsShowing = false;
        int adsPoint = PlayerPrefs.GetInt("skinSpecial" + indexSkinSpecial) + 1;
        PlayerPrefs.SetInt("skinSpecial" + indexSkinSpecial, adsPoint);
        if (PlayerPrefs.GetInt("skinSpecial" + indexSkinSpecial) == 2)
        {
            PlayerPrefs.SetString("CurrentSkin", textSkin[indexSkinSpecial + 6].name);
            checkBall();
            CheckItemChangeSkin();
        }
        checkBallSpecial();
    }
    
    [Header("ItemChangeSkin")]
    public GameObject SpawnMap;
    private GameObject ItemChangeSkin;
    void CheckItemChangeSkin()
    {
        if (System.Int32.Parse(PlayerPrefs.GetString("stage")) % 10 == 8)
        {
            ItemChangeSkin = SpawnMap.transform.Find("MapDemoSkin(Clone)").Find("ItemChangeSkin").gameObject;
            ItemChangeSkin.GetComponent<ModelChangeSkin>().Start();
        }
    }
}
