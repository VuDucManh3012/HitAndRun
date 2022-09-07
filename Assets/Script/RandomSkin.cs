using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using RocketTeam.Sdk.Services.Ads;
using UniRx;

public class RandomSkin : MonoBehaviour
{
    public GameObject[] ImageVery;
    public GameObject[] ImageDisable;
    public GameObject[] ImageSelect;
    public GameObject[] ImageTick;
    public GameObject[] YourSelect;
    public List<int> YourSelect2;
    public Animator ai;
    public Text diamond;
    int br;

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
        if (CanvasPopUpDontOpen.active && !offing)
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
            PlayerPrefs.SetString("diamond", 0.ToString());
        }
        else
        {
            diamond.text = PlayerPrefs.GetString("diamond");
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
                return;
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

        //da duoc nhung skin gi

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
            int e = int.Parse(PlayerPrefs.GetString("diamond"));
            if (e >= price)
            {
                e -= price;
                string m = e.ToString();
                diamond.text = m.ToString();
                PlayerPrefs.SetString("diamond", diamond.text);
                //
                int random = Random.RandomRange(1, YourSelect2.Count + 1);
                //
                br = YourSelect2.ToArray()[random - 1];
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
                //
                Debug.Log("Don`t enough money");
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
        ImageDisable[br].SetActive(false);
        ImageSelect[br].SetActive(true);
        ImageVery[br].SetActive(true);
        YourSelect[br].SetActive(true);

        yield return new WaitForSeconds(0.5f);
        ImageSelect[br].SetActive(false);
        ImageVery[br].SetActive(false);
        YourSelect[br].SetActive(true);
        PlayerPrefs.SetString("CurrentSkin", textSkin[br].name);

        PlayerPrefs.SetInt("skin " + br, 1);
        checkBall();
    }
    public void ChangeSkin()
    {
        ActiveFalse();
        if (EventSystem.current.currentSelectedGameObject.name == "Ball (1)")
        {
            if (!ImageDisable[0].active)
            {
                ImageTick[0].SetActive(true);
                PlayerPrefs.SetString("CurrentSkin", textSkin[0].name);
                skinIndex = 0;
                ChangeSkinUpdate();
            }
            else
            {
                checkBall();
            }
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "Ball (2)")
        {
            if (!ImageDisable[1].active)
            {
                ImageTick[1].SetActive(true);
                PlayerPrefs.SetString("CurrentSkin", textSkin[1].name);
                skinIndex = 1;
                ChangeSkinUpdate();
            }
            else
            {
                checkBall();
            }
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "Ball (3)")
        {
            if (!ImageDisable[2].active)
            {
                ImageTick[2].SetActive(true);
                PlayerPrefs.SetString("CurrentSkin", textSkin[2].name);
                skinIndex = 2;
                ChangeSkinUpdate();
            }
            else
            {
                checkBall();
            }
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "Ball (4)")
        {
            if (!ImageDisable[3].active)
            {
                ImageTick[3].SetActive(true);
                PlayerPrefs.SetString("CurrentSkin", textSkin[3].name);
                skinIndex = 3;
                ChangeSkinUpdate();
            }
            else
            {
                checkBall();
            }
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "Ball (5)")
        {
            if (!ImageDisable[4].active)
            {
                ImageTick[4].SetActive(true);
                PlayerPrefs.SetString("CurrentSkin", textSkin[4].name);
                skinIndex = 4;
                ChangeSkinUpdate();
            }
            else
            {
                checkBall();
            }
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "Ball (6)")
        {
            if (!ImageDisable[5].active)
            {
                ImageTick[5].SetActive(true);
                PlayerPrefs.SetString("CurrentSkin", textSkin[5].name);
                skinIndex = 5;
                ChangeSkinUpdate();
            }
            else
            {
                checkBall();
            }
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "Ball (7)")
        {
            if (!ImageDisableSpecial[0].active)
            {
                ImageTickSpecial[0].SetActive(true);
                PlayerPrefs.SetString("CurrentSkin", textSkin[6].name);
                skinIndex = 6;
                ChangeSkinUpdate();
            }
            else
            {
                ///Ads
                WatchAdsToAddSkin7();
            }
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "Ball (8)")
        {
            if (!ImageDisableSpecial[1].active)
            {
                ImageTickSpecial[1].SetActive(true);
                PlayerPrefs.SetString("CurrentSkin", textSkin[7].name);
                skinIndex = 7;
                ChangeSkinUpdate();
            }
            else
            {
                ///Ads
                WatchAdsToAddSkin8();
            }
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "Ball (9)")
        {
            if (!ImageDisableSpecial[2].active)
            {
                ImageTickSpecial[2].SetActive(true);
                PlayerPrefs.SetString("CurrentSkin", textSkin[8].name);
                skinIndex = 8;
                ChangeSkinUpdate();
            }
            else
            {
                ///Ads
                WatchAdsToAddSkin9();
            }
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "Ball (10)")
        {
            if (!ImageDisableSpecial[3].active)
            {
                ImageTickSpecial[3].SetActive(true);
                PlayerPrefs.SetString("CurrentSkin", textSkin[9].name);
                skinIndex = 9;
                ChangeSkinUpdate();
            }
            else
            {
                ///Ads
                WatchAdsToAddSkin10();
            }
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "Ball (11)")
        {
            if (!ImageDisableSpecial[4].active)
            {
                ImageTickSpecial[4].SetActive(true);
                PlayerPrefs.SetString("CurrentSkin", textSkin[10].name);
                skinIndex = 10;
                ChangeSkinUpdate();
            }
            else
            {
                ///Ads
                WatchAdsToAddSkin11();
            }
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "Ball (12)")
        {
            if (!ImageDisableSpecial[5].active)
            {
                ImageTickSpecial[5].SetActive(true);
                PlayerPrefs.SetString("CurrentSkin", textSkin[11].name);
                skinIndex = 11;
                ChangeSkinUpdate();
            }
            else
            {
                ///Ads
                WatchAdsToAddSkin12();
            }
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
        string diamondCurrent = PlayerPrefs.GetString("diamond");
        int diamondCurrentInt = int.Parse(diamondCurrent) + 1500;
        PlayerPrefs.SetString("diamond", diamondCurrentInt.ToString());
        DiamondText();
    }
    /// ///////////////////////////////////////////////////////////////////////////////////
    public void WatchAdsToAddSkin7()
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
            AdManager.Instance.ShowAdsReward(OnCompleteAdsToAddSkin7, "AddSkin7");
        }
#if !PROTOTYPE
        else
        {
            adsShowing = true;
            AdManager.Instance.ShowAdsReward(OnCompleteAdsToAddSkin7, "AddSkin7");
        }
#endif
    }
    private void OnCompleteAdsToAddSkin7(int value)
    {
        AnalyticManager.LogWatchAds("GetSkinSpecial7", 1);
        adsShowing = false;
        int adsPoint = PlayerPrefs.GetInt("skinSpecial" + 0) + 1;
        PlayerPrefs.SetInt("skinSpecial" + 0, adsPoint);
        if (PlayerPrefs.GetInt("skinSpecial" + 0) == 2)
        {
            PlayerPrefs.SetString("CurrentSkin", textSkin[6].name);
            checkBall();
            CheckItemChangeSkin();
        }
        checkBallSpecial();
    }
    /// ///////////////////////////////////////////////////////////////////////////////////

    public void WatchAdsToAddSkin8()
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
            AdManager.Instance.ShowAdsReward(OnCompleteAdsToAddSkin8, "AddSkin8");
        }
#if !PROTOTYPE
        else
        {
            adsShowing = true;
            AdManager.Instance.ShowAdsReward(OnCompleteAdsToAddSkin8, "AddSkin8");
        }
#endif
    }
    private void OnCompleteAdsToAddSkin8(int value)
    {
        AnalyticManager.LogWatchAds("GetSkinSpecial8", 1);
        adsShowing = false;
        int adsPoint = PlayerPrefs.GetInt("skinSpecial" + 1) + 1;
        PlayerPrefs.SetInt("skinSpecial" + 1, adsPoint);
        if (PlayerPrefs.GetInt("skinSpecial" + 1) == 2)
        {
            PlayerPrefs.SetString("CurrentSkin", textSkin[7].name);
            checkBall();
            CheckItemChangeSkin();
        }
        checkBallSpecial();
    }
    ///////////////////////////////////////////////////////////////////////////////////////
    ///
    public void WatchAdsToAddSkin9()
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
            AdManager.Instance.ShowAdsReward(OnCompleteAdsToAddSkin9, "AddSkin9");
        }
#if !PROTOTYPE
        else
        {
            adsShowing = true;
            AdManager.Instance.ShowAdsReward(OnCompleteAdsToAddSkin9, "AddSkin9");
        }
#endif
    }
    private void OnCompleteAdsToAddSkin9(int value)
    {
        AnalyticManager.LogWatchAds("GetSkinSpecial9", 1);
        adsShowing = false;
        int adsPoint = PlayerPrefs.GetInt("skinSpecial" + 2) + 1;
        PlayerPrefs.SetInt("skinSpecial" + 2, adsPoint);
        if (PlayerPrefs.GetInt("skinSpecial" + 2) == 2)
        {
            PlayerPrefs.SetString("CurrentSkin", textSkin[8].name);
            checkBall();
            CheckItemChangeSkin();
        }
        checkBallSpecial();
    }
    ///////////////////////////////////////////////////////////////////////////////////////
    ///
    public void WatchAdsToAddSkin10()
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
            AdManager.Instance.ShowAdsReward(OnCompleteAdsToAddSkin10, "AddSkin10");
        }
#if !PROTOTYPE
        else
        {
            adsShowing = true;
            AdManager.Instance.ShowAdsReward(OnCompleteAdsToAddSkin10, "AddSkin10");
        }
#endif
    }
    private void OnCompleteAdsToAddSkin10(int value)
    {
        AnalyticManager.LogWatchAds("GetSkinSpecial10", 1);
        adsShowing = false;
        int adsPoint = PlayerPrefs.GetInt("skinSpecial" + 3) + 1;
        PlayerPrefs.SetInt("skinSpecial" + 3, adsPoint);
        if (PlayerPrefs.GetInt("skinSpecial" + 3) == 2)
        {
            PlayerPrefs.SetString("CurrentSkin", textSkin[9].name);
            checkBall();
            CheckItemChangeSkin();
        }
        checkBallSpecial();
    }
    //////////////////////////////////////////////////////////////////////////////////////
    public void WatchAdsToAddSkin11()
    {
        if (!GameManager.NetworkAvailable)
        {
            Debug.Log("a1");
            PopupNoInternet.Show();
            return;
        }

        if (adsShowing)
        {
            Debug.Log("a2");
            return;
        }
        if (!GameManager.EnableAds)
        {
            Debug.Log("a3");
            adsShowing = true;
            AdManager.Instance.ShowAdsReward(OnCompleteAdsToAddSkin11, "AddSkin11");
        }
#if !PROTOTYPE
        else
        {
            Debug.Log("a4");
            adsShowing = true;
            AdManager.Instance.ShowAdsReward(OnCompleteAdsToAddSkin11, "AddSkin11");
        }
#endif
    }
    private void OnCompleteAdsToAddSkin11(int value)
    {
        AnalyticManager.LogWatchAds("GetSkinSpecial11", 1);
        adsShowing = false;
        int adsPoint = PlayerPrefs.GetInt("skinSpecial" + 4) + 1;
        PlayerPrefs.SetInt("skinSpecial" + 4, adsPoint);
        if (PlayerPrefs.GetInt("skinSpecial" + 4) == 2)
        {
            PlayerPrefs.SetString("CurrentSkin", textSkin[10].name);
            checkBall();
            CheckItemChangeSkin();
        }
        checkBallSpecial();
    }
    //////////////////////////////////////////////////////////////////////////////////////
    public void WatchAdsToAddSkin12()
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
            AdManager.Instance.ShowAdsReward(OnCompleteAdsToAddSkin12, "AddSkin12");
        }
#if !PROTOTYPE
        else
        {
            adsShowing = true;
            AdManager.Instance.ShowAdsReward(OnCompleteAdsToAddSkin12, "AddSkin12");
        }
#endif
    }
    private void OnCompleteAdsToAddSkin12(int value)
    {
        AnalyticManager.LogWatchAds("GetSkinSpecial12", 1);
        adsShowing = false;
        int adsPoint = PlayerPrefs.GetInt("skinSpecial" + 5) + 1;
        PlayerPrefs.SetInt("skinSpecial" + 5, adsPoint);
        if (PlayerPrefs.GetInt("skinSpecial" + 5) == 2)
        {
            PlayerPrefs.SetString("CurrentSkin", textSkin[11].name);
            checkBall();
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
