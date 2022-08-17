using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using RocketTeam.Sdk.Services.Ads;
public class CanvasManager : MonoBehaviour
{
    [Header("GameOver")]
    public GameObject GameOverScene;

    [Header("GameStart")]
    public GameObject GameStartScene;

    [Header("Save")]
    public Save Save;

    [Header("Gift10Minutes")]
    public int startingTime = 600;
    public Text countdownText;
    public GameObject NoticeGift10Minutes;

    [Header("CanvasOpenGift")]
    public GameObject ResultGift10minutes;
    public GameObject Gift10minutesButNotEnough;
    public Text currentTimeGift10miutes;
    private int diamondInChest;
    public int currentTime;
    private bool onGift10minutes;

    [Header("OfflineReward")]
    public Text DistanceMaxOfflineReward;
    public Text PriceUpdateOfflineReward;

    [Header("LuckyWheel")]
    public GameObject NoticeWheelLucky;
    public GameObject CanvasLuckyWheel;

    [Header("UpdateLevel")]
    public Text LevelUpdate;
    public Text PriceLevelUpdate;

    [Header("Victory Scene")]
    public GameObject VictoryScene;
    public Slider SliderVictory;
    public Text TextX;
    public Text DiamondFound;
    public Text DiamondBonusADSText;
    private double DiamondADS;
    private double bonusX;

    [Header("Character&Camera")]
    public GameObject Character;
    public ControllerPlayer characterController;
    public GameObject MainCamera;

    [Header("SkinCanvas")]
    public GameObject SkinShop;

    [Header("WeaponCanvas")]
    public GameObject WeaponShop;

    [Header("CanvasDiamond")]
    public GameObject CanvasDiamond;

    [Header("CanvasNewSkinChestRoom")]
    public GameObject CanvasNewSkinChestRoom;

    [Header("CanvasQualityKey")]
    public GameObject CanVasQualityKey;
    public List<GameObject> ListImageKeyDisable;

    [Header("CanvasNewSkinEnding")]
    public List<int> SkinNoBuy;
    public List<int> WeaponNoBuy;
    public int TypeShop;
    public int indexSkin;
    public int phanTramNewSkinEndingCurrent = 0;
    public int phanTramNewSkinEndingBefore = 0;
    public GameObject CanvasNewSkinEnding;
    public Image ImageSkin;
    public Image ImageMarkSkin;
    public Text TextButtonNewSkin;
    public Text TextPhanTram;
    public Sprite[] ListSpriteImage;
    public GameObject ButtonNoThank;
    private float AnimFillAmountSkinEnding;
    private float AnimFillAmountSkinEndingCurrent;
    private float TextFillAmountSkinEnding;
    private float TextFillAmountSkinEndingCurrent;
    public GameObject ButtonAdsNewSkinEnding;
    public GameObject ButtonNextLevel;
    public GameObject ButtonGetIt;

    [Header("CanVasPopUpRateUs")]
    private int StarRate = 5;
    public void FixedUpdate()
    {
        if (VictoryScene.active)
        {
            VictorySceneController();
        }
        else if (AnimFillAmountSkinEnding >= 0)
        {
            if (AnimFillAmountSkinEndingCurrent >= AnimFillAmountSkinEnding)
            {
                AnimFillAmountSkinEndingCurrent -= 0.1f * Time.deltaTime;
                ImageMarkSkin.fillAmount = AnimFillAmountSkinEndingCurrent;
            }
            if (TextFillAmountSkinEndingCurrent <= TextFillAmountSkinEnding)
            {
                TextFillAmountSkinEndingCurrent += 0.2f;
                TextPhanTram.text = (TextFillAmountSkinEndingCurrent / 1 - TextFillAmountSkinEndingCurrent % 1).ToString() + " %";
            }
        }
    }
    public void SetStarRate(int Star)
    {
        StarRate = Star;
    }
    [ContextMenu("CanvasNewSkinEndingController")]
    public void CanvasNewSkinEndingController()
    {
        CanvasNewSkinEnding.SetActive(true);

        if (PlayerPrefs.HasKey("phanTramNewSkinEndingCurrent") && PlayerPrefs.GetInt("phanTramNewSkinEndingCurrent") != 100)
        {
            ThemPhanTramSkinEndingCurrent();
            SetTextAndAnimSkinEnding();
        }
        else
        {
            RandomSkinEnding();
            SetTextAndAnimSkinEnding();
        }
    }

    public void RandomSkinEnding()
    {
        //checkball
        for (int i = 1; i < 6; i++)
        {
            if (PlayerPrefs.GetInt("ball " + i) != 1)
            {
                WeaponNoBuy.Add(i);
            }
            if (PlayerPrefs.GetInt("skin " + i) != 1)
            {
                SkinNoBuy.Add(i);
            }
        }
        //chon random
        TypeShop = Random.Range(0, 2);
        if (TypeShop == 0 && WeaponNoBuy.Count > 0)
        {
            indexSkin = Random.Range(0, WeaponNoBuy.Count);
        }
        else if (TypeShop == 1 && SkinNoBuy.Count > 0)
        {
            indexSkin = Random.Range(0, SkinNoBuy.Count);
        }
        //SetPhanTramCurrentFirst
        phanTramNewSkinEndingCurrent = 25;
        PlayerPrefs.SetInt("TypeShopEndingSkin", TypeShop);
        PlayerPrefs.SetInt("IndexSkinEndingShop", indexSkin);
        PlayerPrefs.SetInt("phanTramNewSkinEndingCurrent", phanTramNewSkinEndingCurrent);
    }
    public void ThemPhanTramSkinEndingCurrent()
    {
        phanTramNewSkinEndingCurrent = PlayerPrefs.GetInt("phanTramNewSkinEndingCurrent");
        phanTramNewSkinEndingBefore = phanTramNewSkinEndingCurrent;
        phanTramNewSkinEndingCurrent += 25;
        PlayerPrefs.SetInt("phanTramNewSkinEndingCurrent", phanTramNewSkinEndingCurrent);
    }
    public void SetAnimFillAmountSkinEnding(float fillAmount, float TextFillAmount)
    {
        AnimFillAmountSkinEnding = fillAmount;
        TextFillAmountSkinEnding = TextFillAmount;
    }
    public void SetTextAndAnimSkinEnding()
    {
        AnimFillAmountSkinEnding = 1;
        float CurrentProcess2 = phanTramNewSkinEndingBefore;
        CurrentProcess2 /= 100;
        AnimFillAmountSkinEndingCurrent = 1 - CurrentProcess2;

        TextFillAmountSkinEnding = 0;
        TextFillAmountSkinEndingCurrent = phanTramNewSkinEndingBefore;

        TypeShop = PlayerPrefs.GetInt("TypeShopEndingSkin");
        indexSkin = PlayerPrefs.GetInt("IndexSkinEndingShop");

        if (TypeShop == 0)
        {
            ImageSkin.sprite = ListSpriteImage[indexSkin];
        }
        else if (TypeShop == 1)
        {
            ImageSkin.sprite = ListSpriteImage[indexSkin + 6];
        }

        int CurrentProcess = PlayerPrefs.GetInt("phanTramNewSkinEndingCurrent");
        float fillAmount = CurrentProcess;
        fillAmount = 1 - (fillAmount / 100);
        SetAnimFillAmountSkinEnding(fillAmount, CurrentProcess);
        if (CurrentProcess == 100)
        {
            ButtonNoThank.SetActive(true);
            ButtonGetIt.SetActive(true);
            ButtonAdsNewSkinEnding.SetActive(false);
            ButtonNextLevel.SetActive(false);
        }
        else
        {
            ButtonNoThank.SetActive(false);
            ButtonGetIt.SetActive(false);
            ButtonAdsNewSkinEnding.SetActive(true);
            ButtonNextLevel.SetActive(true);
        }
    }
    [ContextMenu("ButtonCanvasNewSkinEnding")]
    public void ButtonCanvasNewSkinEnding()
    {
        if (TextFillAmountSkinEnding != 100)
        {
            //next level
            CanvasNewSkinEnding.SetActive(false);
            VictoryScene.SetActive(true);
        }
        else
        {
            //ads
            WatchAdsGetSkinEnding();
            //get it
        }
    }
    public void ButtonNoThanksNewSkinEnding()
    {
        if (PlayerPrefs.GetInt("phanTramNewSkinEndingCurrent") == 100)
        {
            PlayerPrefs.DeleteKey("phanTramNewSkinEndingCurrent");
        }
    }
    public void ButtonADSNewSkin()
    {
        WatchAdsAddPhanTramSkinEnding();
    }
    public void CanvasQualityKeyController()
    {
        //Check Key , set image disable
        int keyCurrent = System.Int32.Parse(PlayerPrefs.GetString("key"));
        if (keyCurrent >= 3)
        {
            PlayerPrefs.SetString("key", 3.ToString());
            keyCurrent = System.Int32.Parse(PlayerPrefs.GetString("key"));
        }

        if (keyCurrent != 0)
        {
            for (int i = 0; i < keyCurrent; i++)
            {
                ListImageKeyDisable[i].SetActive(false);
            }
        }
        CanVasQualityKey.SetActive(true);
    }
    public void VictorySceneController()
    {
        //Set Text X

        float valueSlider = SliderVictory.value;
        if (valueSlider >= 0 && valueSlider <= 1)
        {
            TextX.text = "x2";
            bonusX = 2;
        }
        else if (valueSlider >= 1 && valueSlider <= 2)
        {
            TextX.text = "x3";
            bonusX = 3;
        }
        else if (valueSlider >= 2 && valueSlider <= 3)
        {
            TextX.text = "x5";
            bonusX = 5;
        }
        else if (valueSlider >= 3 && valueSlider <= 4)
        {
            TextX.text = "x3";
            bonusX = 3;
        }
        else if (valueSlider >= 4 && valueSlider <= 5)
        {
            TextX.text = "x2";
            bonusX = 2;
        }
        DiamondADS = double.Parse(DiamondFound.text) * bonusX;
        DiamondBonusADSText.text = DiamondADS.ToString();
    }
    public void ADSBonusButton()
    {
        ///Ads
        WatchAdsADSBonusVictory();
    }
    public void Plus999Level()
    {
        //ads
        WatchAds999Level();
        //
    }
    public void GameOver()
    {
        Time.timeScale = 1;
        SetTimeCountDown();
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    public void Continues()
    {
        InterInVictory();
        //BossEnding
        int currentBoss = PlayerPrefs.GetInt("IntBossToSpawn") + 1;
        PlayerPrefs.SetInt("IntBossToSpawn", currentBoss);
        //
        Save.WriteText();
        SetTimeCountDown();
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    public void DiamondOfflineRewardUpdate()
    {
        int diamondCurrent = System.Int32.Parse(PlayerPrefs.GetString("diamond"));
        int PriceUpdate = System.Int32.Parse(PriceUpdateOfflineReward.text);

        if (diamondCurrent >= PriceUpdate)
        {
            int DistanceMaxNow = PlayerPrefs.GetInt("DistanceMax");
            PlayerPrefs.SetInt("DistanceMax", DistanceMaxNow += 5);
            SetTextOfflineRewardUpdate();
            diamondCurrent = diamondCurrent - PriceUpdate;
            PlayerPrefs.SetString("diamond", diamondCurrent.ToString());
            characterController.OnParticle(8);
        }
        else
        {

            ///Ads
            WatchAdsUpdateOfflineReward();
        }
    }
    public void ClaimOfflineReward()
    {
        int diamondCurrent = System.Int32.Parse(PlayerPrefs.GetString("diamond"));
        diamondCurrent += System.Int32.Parse(Save.DiamondBonusOffline.ToString());
        PlayerPrefs.SetString("diamond", diamondCurrent.ToString());
        Save.ReadText();
        Save.PopupOfflineReward.SetActive(false);
        Save.GameStartScene.SetActive(true);
        CanvasQualityKeyController();
        PlayerPrefs.SetString("DateBefore", System.DateTime.Now.ToString());
    }
    public void ClaimDoubleOfflineReward()
    {

        ///Ads
        WatchAdsClaimDoubleOfflineReward();
    }
    public void SetTextOfflineRewardUpdate()
    {
        if (PlayerPrefs.HasKey("DistanceMax"))
        {
            int DistanceMax = PlayerPrefs.GetInt("DistanceMax");
            int DistanceMaxIfUpdate = DistanceMax + 5;
            int DiamondMaxIfUpdatex = DistanceMaxIfUpdate * 10;
            DistanceMaxOfflineReward.text = DistanceMaxIfUpdate.ToString();
            PriceUpdateOfflineReward.text = DiamondMaxIfUpdatex.ToString();
        }
        else
        {
            PlayerPrefs.SetInt("DistanceMax", 30);
            SetTextOfflineRewardUpdate();
        }
    }
    private int currentUpdateLevel;
    public void Updatelevel()
    {
        currentUpdateLevel = PlayerPrefs.GetInt("UpdateLevel");
        int currentdiamond = System.Int32.Parse(PlayerPrefs.GetString("diamond"));
        if (currentdiamond >= (currentUpdateLevel + 1) * 50)
        {
            PlayerPrefs.SetInt("UpdateLevel", currentUpdateLevel + 1);
            SetTextUpdateLevel();
            characterController.myLevel += 1;

            //particle

            characterController.Particle[0].SetActive(false);
            characterController.Particle[0].SetActive(true);
            //
            //tru tien
            currentdiamond = currentdiamond - (currentUpdateLevel + 1) * 50;
            PlayerPrefs.SetString("diamond", currentdiamond.ToString());
            Save.ReadText();
            //
        }
        else
        {
            ///Ads
            WatchAdsUpdateLevel();
        }
    }
    public void SetTextUpdateLevel()
    {
        if (PlayerPrefs.HasKey("UpdateLevel"))
        {
            int currentUpdateLevel = PlayerPrefs.GetInt("UpdateLevel");
            currentUpdateLevel += 1;
            int priceUpdateLevel = currentUpdateLevel * 50;
            LevelUpdate.text = "Lv " + currentUpdateLevel.ToString();
            PriceLevelUpdate.text = priceUpdateLevel.ToString();
        }
        else
        {
            PlayerPrefs.SetInt("UpdateLevel", 0);
            SetTextUpdateLevel();
        }
    }
    public void TatADS()
    {
        ///tat ads
        GameManager.Instance.Data.User.PurchasedNoAds = true;
        ///
    }
    // Start is called before the first frame update
    void Start()
    {
        GameOverScene.SetActive(false);
        VictoryScene.SetActive(false);
        CanvasLuckyWheel.GetComponentInChildren<Spin>().Start();
        if (!PlayerPrefs.HasKey("currenttime"))
        {
            currentTime = startingTime;
        }
        else
        {
            currentTime = PlayerPrefs.GetInt("currenttime");
        }
        SetTextOfflineRewardUpdate();
        SetTextUpdateLevel();
        //check lan dau vao shop
        if (!PlayerPrefs.HasKey("FirstGoShopSkin"))
        {
            PlayerPrefs.SetInt("FirstGoShopSkin", 0);
        }
        if (!PlayerPrefs.HasKey("FirstGoShopWeapon"))
        {
            PlayerPrefs.SetInt("FirstGoShopWeapon", 0);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (currentTime == 0)
        {
            NoticeGift10Minutes.SetActive(true);
        }
        else if (currentTime != 0)
        {
            CountDown();
            NoticeGift10Minutes.SetActive(false);
        }
        CheckSlotSpin();
    }
    public void CheckSlotSpin()
    {
        if (!PlayerPrefs.HasKey("SpinSlot"))
        {
            PlayerPrefs.SetInt("SpinSlot", 1);
        }
        int spinslot = PlayerPrefs.GetInt("SpinSlot");
        if (spinslot >= 1)
        {
            NoticeWheelLucky.SetActive(true);
        }
        else
        {
            NoticeWheelLucky.SetActive(false);
        }
    }
    private void OnApplicationQuit()
    {
        SetTimeCountDown();
        PlayerPrefs.SetString("DateBefore", System.DateTime.Now.ToString());
        PlayerPrefs.SetInt("FirstGoShopSkin", 0);
        PlayerPrefs.SetInt("FirstGoShopWeapon", 0);
    }
    private float TimeTick;
    private int MaxTick = 1;
    private void CountDown()
    {
        if (currentTime <= 0)
        {
            currentTime = 0;
            onGift10minutes = true;
            countdownText.text = "00:00";
        }
        else
        {
            TimeTick += Time.deltaTime;
            if (TimeTick >= MaxTick)
            {
                TimeTick -= MaxTick;
                currentTime--;
            }

            double minutesCountDown = currentTime / 60;
            double secondsCountDown = currentTime % 60;
            string minutesDisplay, secondsDisplay;
            //minutes
            if (minutesCountDown >= 10)
            {
                minutesDisplay = minutesCountDown.ToString();
            }
            else
            {
                minutesDisplay = "0" + minutesCountDown;
            }
            if (secondsCountDown >= 10)
            {
                secondsDisplay = secondsCountDown.ToString();
            }
            else
            {
                secondsDisplay = "0" + secondsCountDown;
            }
            currentTimeGift10miutes.text = minutesDisplay + " : " + secondsDisplay;
            countdownText.text = minutesDisplay + " : " + secondsDisplay;
        }
    }
    public void ClosePopUpNewSkin()
    {
        CanvasNewSkinChestRoom.SetActive(false);
    }
    private void SetTimeCountDown()
    {
        PlayerPrefs.SetInt("currenttime", currentTime);
    }
    public void changeWeaponScene()
    {
        WeaponShop.SetActive(true);
        GameStartScene.SetActive(false);
        Character.GetComponent<ControllerPlayer>().ChangeCam("CamShop");
        Character.GetComponent<AutoSpin>().enabled = true;
        CanvasDiamond.SetActive(false);
        WeaponShop.transform.GetChild(0).GetComponent<RandomShop2>().Start();
    }
    public void changSkinScene()
    {
        SkinShop.SetActive(true);
        GameStartScene.SetActive(false);
        Character.GetComponent<ControllerPlayer>().ChangeCam("CamShop");
        Character.GetComponent<AutoSpin>().enabled = true;
        CanvasDiamond.SetActive(false);
        SkinShop.transform.GetChild(0).gameObject.GetComponent<RandomSkin>().Start();
    }
    public void Gift()
    {
        GameStartScene.SetActive(false);
        if (currentTime == 0)
        {
            ResultGift10minutes.SetActive(true);
            diamondInChest = Random.RandomRange(100, 500);
            ResultGift10minutes.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Earned :" + diamondInChest;
        }
        else
        {
            Gift10minutesButNotEnough.SetActive(true);
        }
    }
    public void CloseGift10minutesButNotEnough()
    {
        Gift10minutesButNotEnough.SetActive(false);
        GameStartScene.SetActive(true);
    }
    public void CloseGift()
    {
        ResultGift10minutes.SetActive(false);
        currentTime = startingTime;
        GameStartScene.SetActive(true);
    }
    public void ClaimGift()
    {
        ResultGift10minutes.SetActive(false);

        //diamond ++
        if (PlayerPrefs.GetString("diamond") != "")
        {
            int diamond = int.Parse(PlayerPrefs.GetString("diamond"));
            diamond += diamondInChest;
            PlayerPrefs.SetString("diamond", diamond.ToString());
            Save.ReadText();
        }
        //
        if (PlayerPrefs.GetInt("currenttime") == 0)
        {
            currentTime = 600;
            onGift10minutes = false;
        }
        currentTime = startingTime;
    }
    public void WatchADSClaimX2Gift()
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
            AdManager.Instance.ShowAdsReward(OnCompleteAdsClaimX2Gift, "ClaimX2Gift");
        }
#if !PROTOTYPE
        else
        {
            adsShowing = true;
            AdManager.Instance.ShowAdsReward(OnCompleteAdsClaimX2Gift, "ClaimX2Gift");
        }
#endif
    }
    public void OnCompleteAdsClaimX2Gift(int value)
    {
        AnalyticManager.LogWatchAds("ClaimX2Gift10Minutes", 1);
        ResultGift10minutes.SetActive(false);

        //diamond ++
        if (PlayerPrefs.GetString("diamond") != "")
        {
            int diamond = int.Parse(PlayerPrefs.GetString("diamond"));
            diamond += (diamondInChest*2);
            PlayerPrefs.SetString("diamond", diamond.ToString());
            Save.ReadText();
        }
        //
        if (PlayerPrefs.GetInt("currenttime") == 0)
        {
            currentTime = 600;
            onGift10minutes = false;
        }
        currentTime = startingTime;
    }
    public void LoseItGift()
    {
        ResultGift10minutes.SetActive(false);

        if (PlayerPrefs.GetInt("currenttime") == 0)
        {
            currentTime = 600;
            onGift10minutes = false;
        }
        currentTime = startingTime;
        GameStartScene.SetActive(true);
    }
    public void onLuckyWheel()
    {
        CanvasLuckyWheel.SetActive(true);
    }
    public void ChangeSceneTest()
    {
        SceneManager.LoadScene("AllObjectMap");
    }
    private bool adsShowing = false;
    public void ShowDebug()
    {
        MaxSdk.ShowMediationDebugger();
    }
    void InterInVictory()
    {
        if (PlayerPrefs.GetInt("FirstGoShopSkin") == 0)
        {
            if (PlayerPrefs.GetInt("FirstGoShopWeapon") == 0)
            {
                if (!GameManager.NetworkAvailable)
                {
                    PopupNoInternet.Show();
                    return;
                }

                if (adsShowing)
                    return;


                if (GameManager.EnableAds)
                {
                    AdManager.Instance.ShowInterstitial("Victory", 1);
                }
                PlayerPrefs.SetInt("FirstGoShopWeapon", 1);
            }
            PlayerPrefs.SetInt("FirstGoShopSkin", 1);
        }
    }
    public void InterInFirstBackShopSkin()
    {
        if (PlayerPrefs.GetInt("FirstGoShopSkin") == 0)
        {
            if (PlayerPrefs.GetInt("FirstGoShopWeapon") == 0)
            {
                if (!GameManager.NetworkAvailable)
                {
                    PopupNoInternet.Show();
                    return;
                }

                if (adsShowing)
                    return;


                if (GameManager.EnableAds)
                {
                    AdManager.Instance.ShowInterstitial("BackShopSkin", 1);
                }
                PlayerPrefs.SetInt("FirstGoShopWeapon", 1);
            }
            PlayerPrefs.SetInt("FirstGoShopSkin", 1);
        }
    }
    public void InterInFirstBackShopWeapon()
    {
        if (PlayerPrefs.GetInt("FirstGoShopWeapon") == 0)
        {
            if (!GameManager.NetworkAvailable)
            {
                PopupNoInternet.Show();
                return;
            }

            if (adsShowing)
                return;


            if (GameManager.EnableAds)
            {
                AdManager.Instance.ShowInterstitial("BackShopWeapon", 1);
            }
            PlayerPrefs.SetInt("FirstGoShopWeapon", 1);
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void WatchAdsADSBonusVictory()
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
            AdManager.Instance.ShowAdsReward(OnCompleteAdsADSBonusVictory, "ADSBonusVictory");
        }
#if !PROTOTYPE
        else
        {
            adsShowing = true;
            AdManager.Instance.ShowAdsReward(OnCompleteAdsADSBonusVictory, "ADSBonusVictory");
        }
#endif
    }
    private void OnCompleteAdsADSBonusVictory(int value)
    {
        AnalyticManager.LogWatchAds("AdsBonusVictory", 1);
        adsShowing = false;
        double diamondCurrent = double.Parse(Save.Diamond.text);
        diamondCurrent += DiamondADS;
        Save.Diamond.text = diamondCurrent.ToString();
        Continues();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void WatchAds999Level()
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
            AdManager.Instance.ShowAdsReward(OnCompleteAds999Level, "999Level");
        }
#if !PROTOTYPE
        else
        {
            adsShowing = true;
            AdManager.Instance.ShowAdsReward(OnCompleteAds999Level, "999Level");
        }
#endif
    }
    private void OnCompleteAds999Level(int value)
    {
        AnalyticManager.LogWatchAds("+999Level", 1);
        adsShowing = false;
        characterController.myLevel += 999;
        characterController.SetSkin();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void WatchAdsUpdateOfflineReward()
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
            AdManager.Instance.ShowAdsReward(OnCompleteAdsUpdateOfflineReward, "UpdateOfflineReward");
        }
#if !PROTOTYPE
        else
        {
            adsShowing = true;
            AdManager.Instance.ShowAdsReward(OnCompleteAdsUpdateOfflineReward, "UpdateOfflineReward");
        }
#endif
    }
    private void OnCompleteAdsUpdateOfflineReward(int value)
    {
        AnalyticManager.LogWatchAds("UpdateOfflineReward", 1);
        adsShowing = false;
        int DistanceMaxNow = PlayerPrefs.GetInt("DistanceMax");
        PlayerPrefs.SetInt("DistanceMax", DistanceMaxNow += 5);
        SetTextOfflineRewardUpdate();
        characterController.OnParticle(8);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void WatchAdsClaimDoubleOfflineReward()
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
            AdManager.Instance.ShowAdsReward(OnCompleteAdsClaimDoubleOfflineReward, "ClaimDoubleOfflineReward");
        }
#if !PROTOTYPE
        else
        {
            adsShowing = true;
            AdManager.Instance.ShowAdsReward(OnCompleteAdsClaimDoubleOfflineReward, "ClaimDoubleOfflineReward");
        }
#endif
    }
    private void OnCompleteAdsClaimDoubleOfflineReward(int value)
    {
        AnalyticManager.LogWatchAds("DoubleOfflineReward", 1);
        adsShowing = false;
        int diamondCurrent = System.Int32.Parse(PlayerPrefs.GetString("diamond"));
        diamondCurrent += System.Int32.Parse(Save.DiamondBonusOffline.ToString()) * 2;
        PlayerPrefs.SetString("diamond", diamondCurrent.ToString());
        Save.ReadText();
        Save.PopupOfflineReward.SetActive(false);
        Save.GameStartScene.SetActive(true);
        CanvasQualityKeyController();
        PlayerPrefs.SetString("DateBefore", System.DateTime.Now.ToString());
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void WatchAdsUpdateLevel()
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
            AdManager.Instance.ShowAdsReward(OnCompleteAdsUpdateLevel, "UpdateLevel");
        }
#if !PROTOTYPE
        else
        {
            adsShowing = true;
            AdManager.Instance.ShowAdsReward(OnCompleteAdsUpdateLevel, "UpdateLevel");
        }
#endif
    }
    private void OnCompleteAdsUpdateLevel(int value)
    {
        AnalyticManager.LogWatchAds("UpdateLevel", 1);
        adsShowing = false;
        PlayerPrefs.SetInt("UpdateLevel", currentUpdateLevel + 1);
        SetTextUpdateLevel();
        characterController.myLevel += 1;

        //particle

        characterController.Particle[0].SetActive(false);
        characterController.Particle[0].SetActive(true);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void WatchAdsGetSkinEnding()
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
            AdManager.Instance.ShowAdsReward(OnCompleteAdsGetSkinEnding, "GetSkinEnding");
        }
#if !PROTOTYPE
        else
        {
            adsShowing = true;
            AdManager.Instance.ShowAdsReward(OnCompleteAdsGetSkinEnding, "GetSkinEnding");
        }
#endif
    }
    private void OnCompleteAdsGetSkinEnding(int value)
    {
        AnalyticManager.LogWatchAds("GetSkinEnding", 1);
        adsShowing = false;
        if (TypeShop == 0)
        {
            PlayerPrefs.SetInt("ball " + (indexSkin + 1), 1);
        }
        else if (TypeShop == 1)
        {
            PlayerPrefs.SetInt("skin " + (indexSkin + 1), 1);
        }
        VictoryScene.SetActive(true);
        CanvasNewSkinEnding.SetActive(false);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void WatchAdsAddPhanTramSkinEnding()
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
            AdManager.Instance.ShowAdsReward(OnCompleteAdsAddPhanTramSkinEnding, "AddPhanTramSkinEnding");
        }
#if !PROTOTYPE
        else
        {
            adsShowing = true;
            AdManager.Instance.ShowAdsReward(OnCompleteAdsAddPhanTramSkinEnding, "AddPhanTramSkinEnding");
        }
#endif
    }
    private void OnCompleteAdsAddPhanTramSkinEnding(int value)
    {
        AnalyticManager.LogWatchAds("AddPercentSkinEnding", 1);
        adsShowing = false;
        CanvasNewSkinEndingController();
    }
}