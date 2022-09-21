using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using RocketTeam.Sdk.Services.Ads;
using HyperCatSdk;
using MoreMountains.NiceVibrations;
public class CanvasManager : MonoBehaviour
{
    [Header("GameOver")]
    public GameObject GameOverScene;

    [Header("GameStart")]
    public GameObject GameStartScene;
    public CanvasGameStartController CanvasGameStartController;

    [Header("Save")]
    public Save Save;

    [Header("Gift10Minutes")]
    public GameObject PopupGift;
    private int startingTime = 180;
    public Text countdownText;
    public GameObject NoticeGift10Minutes;
    public Text RewardGift10Minutes;

    [Header("CanvasOpenGift")]
    public GameObject Gift10minutesButNotEnough;
    public Text currentTimeGift10miutes;
    private int diamondInChest;
    public int currentTime;

    [Header("OfflineReward")]
    public Text TextPriceUpdateOfflineLevel;
    public Text TextInfoUpdateOfflineLevel;
    private int DistanceMax = 40;
    private int DistanceMin = 10;
    public GameObject PopupRewardOffline;
    public Text TextPopupRewardOffline;

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
    public Text TextXInLv1;
    public Text DiamondFound;
    public GameObject BossBonus;
    public Text DiamondBonusADSText;
    public Text DiamondBonusADSTextInLv1;
    public GameObject ADSDiamondFly;
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
    public List<int> WeaponNoBuy;
    public List<GameObject> WeaponBonusEnding;
    public List<int> SkinNoBuy;
    public List<Texture> SkinBonusEnding;
    public int TypeShop;
    public int indexSkin;
    public int phanTramNewSkinEndingCurrent = 0;
    public int phanTramNewSkinEndingBefore = 0;
    public GameObject CanvasNewSkinEnding;
    public DemoSkinEnding DemoSkinEnding;
    public Image ImageSkin;
    public Image ImageMarkSkin;
    public Text TextButtonNewSkin;
    public Text TextPhanTram;
    public Sprite[] ListSpriteImage;
    public GameObject ButtonNoThank;
    public GameObject ButtonAdsNewSkinEnding;
    public GameObject ButtonNextLevel;
    public GameObject ButtonGetIt;
    private float AnimFillAmountSkinEnding;
    private float AnimFillAmountSkinEndingCurrent;
    private float TextFillAmountSkinEnding;
    private float TextFillAmountSkinEndingCurrent;


    [Header("CanVasPopUpRateUs")]
    public GameObject CanvasPopupRate;
    public GameObject ButtonRateUs;
    public GameObject PopupThankForRate;
    private int StarRate = 5;

    [Header("BackGroundNenToi")]
    public GameObject BackGroundNenToi;

    [Header("Haptic")]
    public GameObject ButtonOnHaptic;
    public GameObject ButtonOffHaptic;

    [Header("Music&Sound")]
    public Slider MusicVolumn;
    public Slider SoundVolumn;

    [Header("CanvasTouchPad")]
    public GameObject CanvasTouchPad;

    [Header("CanvasDemoSkin")]
    public GameObject CanvasDemoSkin;

    [Header("CanvasDiamondFly")]
    public MoneyClaimFx MoneyClaimFx;
    public Transform SpawnPointDefault;
    public GameObject QuantityDiamondDisplay;
    private Transform SpawnPoint;

    [Header("CanvasBossRoom")]
    public GameObject CanvasBossRoom;

    // Start is called before the first frame update
    void Start()
    {
        Loadscene = false;
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
        if (PlayerPrefs.HasKey("DateBefore"))
        {
            if (!PlayerPrefs.HasKey("ClaimedOfflineReward"))
            {
                PlayerPrefs.SetInt("ClaimedOfflineReward", 0);
            }
            else
            {
                CheckOfflineReward();
                PlayerPrefs.SetString("DateBefore", System.DateTime.Now.ToString());
            }

        }
        SetTextOfflineRewardUpdate();
        //haptic
        checkHaptic();
        ////audio
        //
        AudioAssistant.Instance.PlayMusic("Start");
        AudioAssistant.Instance.PlayMusic("Start");
        checkvolumn();
        //
        SetValueSliderSetting();
        if (PlayerPrefs.GetInt("InterVictory") == 1)
        {
            InterInVictory();
        }
        //checkRated
        bool Rated = PlayerPrefs.HasKey("Rated");
        int stage;
        try
        {
            stage = System.Int32.Parse(PlayerPrefs.GetString("stage"));
        }
        catch
        {
            stage = 1;
        }

        if (!Rated && (stage == 3 || stage % 15 == 0))
        {
            CanvasPopupRate.SetActive(true);
            GameStartScene.SetActive(false);
            CanVasQualityKey.SetActive(false);
        }
        //
    }
    public void DiamondFly(int diamondBonus, Transform spawnTransform)
    {
        if (spawnTransform != null)
        {
            MoneyClaimFx.ClaimMoney(diamondBonus, spawnTransform);
        }
        else
        {
            MoneyClaimFx.ClaimMoney(diamondBonus, SpawnPointDefault);
        }
    }
    public void DiamondFlyAdsReward(int diamondBonus)
    {
        DiamondFly(diamondBonus, SpawnPoint);
    }
    public void SetSpawnPoint(Transform transform)
    {
        SpawnPoint = transform;
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
        if (ScreenVictoryActive)
        {
            if (characterController.DemoingSkin)
            {
                CanvasDemoSkin.GetComponent<CanvasNewSkinDemo>().SetImage(characterController.indexSkinDemo, characterController.typeShopSkinDemo);
                CanvasDemoSkin.SetActive(true);
            }
            else
            {
                VictoryScene.SetActive(true);
            }
        }
    }
    public void FixedUpdate()
    {
        if (VictoryScene.activeInHierarchy)
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
        OnLoadScene();
    }
    public void SetRewardGift10Minutes(int value)
    {
        RewardGift10Minutes.text = "Earned : " + value.ToString();
    }
    public void SetStarRate(int Star)
    {
        StarRate = Star;
    }
    [ContextMenu("CanvasNewSkinEndingController")]
    public void CanvasNewSkinEndingController()
    {
        CanvasNewSkinEnding.SetActive(true);
        //
        WeaponNoBuy.Clear();
        SkinNoBuy.Clear();
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
        //
        if (WeaponNoBuy.Count > 0 || SkinNoBuy.Count > 0)
        {
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
        else
        {
            ButtonCanvasNewSkinEnding();
        }

    }

    public void RandomSkinEnding()
    {
        //checkball
        WeaponNoBuy.Clear();
        SkinNoBuy.Clear();
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
        RandomTypeShopAndIndexSkin();
        //SetPhanTramCurrentFirst
        phanTramNewSkinEndingCurrent = 25;
        PlayerPrefs.SetInt("TypeShopEndingSkin", TypeShop);
        PlayerPrefs.SetInt("IndexSkinEndingShop", indexSkin);
        PlayerPrefs.SetInt("phanTramNewSkinEndingCurrent", phanTramNewSkinEndingCurrent);
    }
    void RandomTypeShopAndIndexSkin()
    {
        //chon random
        //Type 1 = Weapon, Type 2 = Skin
        TypeShop = Random.Range(1, 3);
        if (TypeShop == 1 && WeaponNoBuy.Count > 0)
        {
            indexSkin = Random.Range(0, WeaponNoBuy.Count);
        }
        else if (TypeShop == 1 && WeaponNoBuy.Count == 0 && SkinNoBuy.Count > 0)
        {
            indexSkin = Random.Range(0, SkinNoBuy.Count);
        }
        else if (TypeShop == 2 && SkinNoBuy.Count > 0)
        {
            indexSkin = Random.Range(0, SkinNoBuy.Count);
        }
        else if (TypeShop == 2 && SkinNoBuy.Count == 0 && WeaponNoBuy.Count > 0)
        {
            indexSkin = Random.Range(0, WeaponNoBuy.Count);
        }
        if (TypeShop == 1)
        {
            if (TypeShop == PlayerPrefs.GetInt("TypeShopEndingSkin") && indexSkin == PlayerPrefs.GetInt("IndexSkinEndingShop") && SkinNoBuy.Count > 0)
            {
                indexSkin = Random.Range(0, SkinNoBuy.Count);
            }
        }
        else if (TypeShop == 2)
        {
            if (TypeShop == PlayerPrefs.GetInt("TypeShopEndingSkin") && indexSkin == PlayerPrefs.GetInt("IndexSkinEndingShop") && WeaponNoBuy.Count > 0)
            {
                indexSkin = Random.Range(0, WeaponNoBuy.Count);
            }
        }
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

        WeaponNoBuy.Clear();
        SkinNoBuy.Clear();
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
        TypeShop = PlayerPrefs.GetInt("TypeShopEndingSkin");
        indexSkin = PlayerPrefs.GetInt("IndexSkinEndingShop");
        if (TypeShop == 1 && WeaponNoBuy.Count == 0)
        {
            RandomSkinEnding();
        }
        else if (TypeShop == 2 && SkinNoBuy.Count == 0)
        {
            RandomSkinEnding();
        }
        else if (TypeShop == 1)
        {
            DemoSkinEnding.OnModel(TypeShop, indexSkin);
            //ImageSkin.sprite = ListSpriteImage[WeaponNoBuy[indexSkin]];
        }
        else if (TypeShop == 2)
        {
            DemoSkinEnding.OnModel(TypeShop, indexSkin);
            //ImageSkin.sprite = ListSpriteImage[SkinNoBuy[indexSkin] + 6];
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
            if (characterController.DemoingSkin)
            {
                CanvasDemoSkin.GetComponent<CanvasNewSkinDemo>().SetImage(characterController.indexSkinDemo, characterController.typeShopSkinDemo);
                CanvasDemoSkin.SetActive(true);
            }
            else
            {
                VictoryScene.SetActive(true);
            }
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
        if (characterController.DemoingSkin)
        {
            CanvasDemoSkin.GetComponent<CanvasNewSkinDemo>().SetImage(characterController.indexSkinDemo, characterController.typeShopSkinDemo);
            CanvasDemoSkin.SetActive(true);
        }
        else
        {
            VictoryScene.SetActive(true);
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
            CanVasQualityKey.SetActive(true);
        }

    }
    public void VictorySceneController()
    {
        //Set Text X

        float valueSlider = SliderVictory.value;
        if (valueSlider >= 0 && valueSlider <= 1)
        {
            TextX.text = "x2";
            TextXInLv1.text = "x2";
            bonusX = 2;
        }
        else if (valueSlider >= 1 && valueSlider <= 2)
        {
            TextX.text = "x3";
            TextXInLv1.text = "x3";
            bonusX = 3;
        }
        else if (valueSlider >= 2 && valueSlider <= 3)
        {
            TextX.text = "x5";
            TextXInLv1.text = "x5";
            bonusX = 5;
        }
        else if (valueSlider >= 3 && valueSlider <= 4)
        {
            TextX.text = "x3";
            TextXInLv1.text = "x3";
            bonusX = 3;
        }
        else if (valueSlider >= 4 && valueSlider <= 5)
        {
            TextX.text = "x2";
            TextXInLv1.text = "x2";
            bonusX = 2;
        }
        if (BossBonus.activeInHierarchy)
        {
            DiamondADS = (double.Parse(DiamondFound.text) + 300) * bonusX;
        }
        else
        {
            DiamondADS = double.Parse(DiamondFound.text) * bonusX;
        }
        DiamondBonusADSText.text = DiamondADS.ToString();
        DiamondBonusADSTextInLv1.text = DiamondADS.ToString();
    }
    public void ADSBonusButton()
    {
        ///Ads
        WatchAdsADSBonusVictory();
    }
    public void ADSBonusButtonInLv1()
    {
        adsShowing = false;
        DiamondFlyAdsReward((int)DiamondADS);
        PlayerPrefs.SetInt("InterVictory", 0);
        Save.WriteText();
        SetTimeCountDown();
    }
    public void SetLoadScene()
    {
        Loadscene = true;
    }
    public void Plus999Level()
    {
        if (PlayerPrefs.HasKey("First999Level"))
        {
        //ads
        WatchAds999Level();
        //
        }
        else
        {
            OnCompleteAds999Level(1);
        }

    }
    public void GameOver()
    {
        PlayerPrefs.SetInt("InterVictory", 1);
        Time.timeScale = 1;
        SetTimeCountDown();
        Loadscene = true;
    }
    public void Continues()
    {
        Save.WriteText();
        PlayerPrefs.SetString("key", PlayerPrefs.GetString("key"));

        SetTimeCountDown();
        PlayerPrefs.SetInt("InterVictory", 1);
        Loadscene = true;
    }
    public bool Loadscene = false;
    public bool ScreenVictoryActive;
    private void OnLoadScene()
    {
        if (Loadscene)
        {
            SceneManager.LoadScene(1);
        }
    }
    public void DiamondOfflineRewardUpdate()
    {
        int currentDiamond = PlayerPrefs.GetInt("diamond");
        int currentPriceUpdateOfllineLevel = System.Int32.Parse(TextPriceUpdateOfflineLevel.text);
        if (currentDiamond >= currentPriceUpdateOfllineLevel)
        {
            //Cong them update
            int currentUpdateLevel = PlayerPrefs.GetInt("LevelOfflineReward");
            currentUpdateLevel += 1;
            PlayerPrefs.SetInt("LevelOfflineReward", currentUpdateLevel);
            SetTextOfflineRewardUpdate();
            //tru diamond
            currentDiamond -= currentPriceUpdateOfllineLevel;
            PlayerPrefs.SetInt("diamond", currentDiamond);
            Save.ReadText();
            //Finish
            characterController.Particle[8].SetActive(false);
            characterController.Particle[8].SetActive(true);
            CanvasGameStartController.CheckSceneStart();
            AudioSpendMoney();
        }
        else
        {
            WatchAdsUpdateOfflineReward();
        }
    }
    public void ClaimOfflineReward()
    {
        //Cong Diamond
        DiamondFlyAdsReward(TotalDiamondRewardOffline);

        //Dong Popup
        Save.GameStartScene.SetActive(true);
        CanvasQualityKeyController();
        PlayerPrefs.SetString("DateBefore", System.DateTime.Now.ToString());
        PlayerPrefs.SetInt("ClaimedOfflineReward", 1);
    }
    public void ClaimDoubleOfflineReward()
    {
        ///Ads
        WatchAdsClaimDoubleOfflineReward();
    }
    public void SetTextOfflineRewardUpdate()
    {
        if (PlayerPrefs.HasKey("DateBefore"))
        {
            if (!PlayerPrefs.HasKey("LevelOfflineReward"))
            {
                PlayerPrefs.SetInt("LevelOfflineReward", 1);
            }
            int LevelOfflineReward = PlayerPrefs.GetInt("LevelOfflineReward");
            int PriceUpdateOfflineLevel = LevelOfflineReward * 50;
            int InfoUpdateOfflineLevel = LevelOfflineReward * 5 + 5;
            TextPriceUpdateOfflineLevel.text = PriceUpdateOfflineLevel.ToString();
            TextInfoUpdateOfflineLevel.text = InfoUpdateOfflineLevel.ToString();
        }
        else
        {
            PlayerPrefs.SetString("DateBefore", System.DateTime.Now.ToString());
            PlayerPrefs.SetInt("LevelOfflineReward", 1);
            SetTextOfflineRewardUpdate();
        }
    }
    private int TotalDiamondRewardOffline;
    public void CheckOfflineReward()
    {
        if (PlayerPrefs.GetInt("ClaimedOfflineReward") == 0)
        {
            //Tinh tg offline
            System.DateTime DateBefore = System.DateTime.Parse(PlayerPrefs.GetString("DateBefore"));
            System.DateTime DateNow = System.DateTime.Now;
            System.TimeSpan t = DateNow - DateBefore;
            double Distance = Mathf.Abs(ToSingle(t.TotalMinutes));
            Distance = Distance - Distance % 1;
            //
            if (Distance >= DistanceMin)
            {
                //hien popup
                PopupRewardOffline.SetActive(true);
                GameStartScene.SetActive(false);
                CanVasQualityKey.SetActive(false);
                BackGroundNenToi.SetActive(true);
                //
                if (Distance >= DistanceMax)
                {
                    Distance = DistanceMax;
                }
                TotalDiamondRewardOffline = (int)Distance * (PlayerPrefs.GetInt("LevelOfflineReward") * 5);
                TextPopupRewardOffline.text = "Earned : " + TotalDiamondRewardOffline.ToString();
            }
            PlayerPrefs.SetInt("ClaimedOfflineReward", 1);
        }

    }
    private int currentUpdateLevel;
    public void Updatelevel()
    {
        currentUpdateLevel = PlayerPrefs.GetInt("UpdateLevel");
        int currentdiamond = PlayerPrefs.GetInt("diamond");
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
            int currentDiamond = PlayerPrefs.GetInt("diamond");
            currentDiamond -= (currentUpdateLevel + 1) * 50;
            PlayerPrefs.SetInt("diamond", currentDiamond);
            Save.ReadText();
            //
            CanvasGameStartController.CheckSceneStart();
            AudioSpendMoney();
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
            PlayerPrefs.SetInt("UpdateLevel", 1);
            SetTextUpdateLevel();
        }
    }
    public void TatADS()
    {
        ///tat ads
        GameManager.Instance.Data.User.PurchasedNoAds = true;
        ///
    }

    public void checkvolumn()
    {
        MusicVolumn.value = PlayerPrefs.GetFloat("MusicVolumn");
        SoundVolumn.value = PlayerPrefs.GetFloat("SfxVolumn");
        AudioAssistant.Instance.UpdateSoundSetting2(SoundVolumn.value, MusicVolumn.value);
    }
    public void RateUs()
    {
        PopupThankForRate.SetActive(true);
        CanvasPopupRate.SetActive(false);
        GameStartScene.SetActive(true);
        PlayerPrefs.SetInt("Rated", 1);
        if (StarRate >= 4)
        {
            Application.OpenURL(@"https://play.google.com/store/apps/details?id=" + GameManager.Instance.NoAdsId);
        }
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
    private float TimeTick;
    private int MaxTick = 1;
    private void CountDown()
    {
        if (currentTime <= 0)
        {
            currentTime = 0;
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
            diamondInChest = Random.Range(100, 500);
            RewardGift10Minutes.text = diamondInChest.ToString();
            PopupGift.SetActive(true);
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
        currentTime = startingTime;
        GameStartScene.SetActive(true);
    }
    public void ClaimGift()
    {
        PopupGift.SetActive(false);
        //diamond ++
        if (PlayerPrefs.HasKey("diamond"))
        {
            DiamondFlyAdsReward(diamondInChest);
            Save.ReadText();
        }
        //
        if (PlayerPrefs.GetInt("currenttime") == 0)
        {
            currentTime = 600;
        }
        currentTime = startingTime;
        GameStartScene.SetActive(true);
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
        PopupGift.SetActive(false);
        AnalyticManager.LogWatchAds("ClaimX2Gift10Minutes", 1);

        //diamond ++
        if (PlayerPrefs.HasKey("diamond"))
        {
            DiamondFlyAdsReward(diamondInChest * 2);
            Save.ReadText();
        }
        //
        if (PlayerPrefs.GetInt("currenttime") == 0)
        {
            currentTime = 600;
        }
        currentTime = startingTime;
        GameStartScene.SetActive(true);

    }
    public void LoseItGift()
    {
        if (PlayerPrefs.GetInt("currenttime") == 0)
        {
            currentTime = 600;
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
    public static float ToSingle(double value)
    {
        return (float)value;
    }
    private bool adsShowing = false;
    public void ShowDebug()
    {
        MaxSdk.ShowMediationDebugger();
    }
    void InterInVictory()
    {
        if (adsShowing)
            return;


        if (GameManager.EnableAds)
        {
            AdManager.Instance.ShowInterstitial("Victory", 1);
        }
    }
    public void InterInFirstBackShopSkin()
    {
        if (PlayerPrefs.GetInt("FirstGoShopSkin") == 0)
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
        DiamondFlyAdsReward((int)DiamondADS);
        PlayerPrefs.SetInt("InterVictory", 0);
        Save.WriteText();
        SetTimeCountDown();
        ADSDiamondFly.SetActive(true);
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
        if (!PlayerPrefs.HasKey("First999Level"))
        {
            PlayerPrefs.SetInt("First999Level", 1);
        }
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
        //Cong them update
        int currentUpdateLevel = PlayerPrefs.GetInt("UpdateLevel");
        currentUpdateLevel += 1;
        PlayerPrefs.SetInt("UpdateLevel", currentUpdateLevel);
        SetTextOfflineRewardUpdate();
        //Finish
        characterController.Particle[8].SetActive(false);
        characterController.Particle[8].SetActive(true);
        CanvasGameStartController.CheckSceneStart();
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
        DiamondFlyAdsReward(TotalDiamondRewardOffline * 2);
        Save.ReadText();
        Save.GameStartScene.SetActive(true);
        CanvasQualityKeyController();
        PlayerPrefs.SetString("DateBefore", System.DateTime.Now.ToString());
        PlayerPrefs.SetInt("ClaimedOfflineReward", 1);
        PopupRewardOffline.SetActive(false);
        BackGroundNenToi.SetActive(false);
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
        CanvasGameStartController.CheckSceneStart();
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
        if (TypeShop == 1)
        {
            PlayerPrefs.SetInt("ball " + WeaponNoBuy[indexSkin], 1);
            PlayerPrefs.SetString("CurrentWeapon", WeaponBonusEnding[WeaponNoBuy[indexSkin]].name);
        }
        else if (TypeShop == 2)
        {
            PlayerPrefs.SetInt("skin " + SkinNoBuy[indexSkin], 1);
            PlayerPrefs.SetString("CurrentSkin", SkinBonusEnding[SkinNoBuy[indexSkin]].name);
        }
        PlayerPrefs.DeleteKey("phanTramNewSkinEndingCurrent");
        if (characterController.DemoingSkin)
        {
            CanvasDemoSkin.GetComponent<CanvasNewSkinDemo>().SetImage(characterController.indexSkinDemo, characterController.typeShopSkinDemo);
            CanvasDemoSkin.SetActive(true);
        }
        else
        {
            VictoryScene.SetActive(true);
        }
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
    public void AudioSpendMoney()
    {
        AudioAssistant.Shot(TYPE_SOUND.SpendMoney);
        HCVibrate.Haptic(HapticTypes.SoftImpact);
    }
    public void AudioButton()
    {
        AudioAssistant.Shot(TYPE_SOUND.BUTTON);
        HCVibrate.Haptic(HapticTypes.SoftImpact);
    }
    public void OnHaptic()
    {
        GameManager.Instance.Data.Setting.Haptic = 1;
        PlayerPrefs.SetInt("HapticStatus", 1);
        checkHaptic();
    }
    public void OffHaptic()
    {
        GameManager.Instance.Data.Setting.Haptic = 0;
        PlayerPrefs.SetInt("HapticStatus", 0);
        checkHaptic();
    }
    public void checkHaptic()
    {
        if (!PlayerPrefs.HasKey("HapticStatus"))
        {
            PlayerPrefs.SetInt("HapticStatus", 1);
        }
        if (PlayerPrefs.GetInt("HapticStatus") == 0)
        {
            //DangTat
            ButtonOnHaptic.SetActive(true);
            ButtonOffHaptic.SetActive(false);
            GameManager.Instance.Data.Setting.Haptic = 0;
        }
        else
        {
            ButtonOnHaptic.SetActive(false);
            ButtonOffHaptic.SetActive(true);
            GameManager.Instance.Data.Setting.Haptic = 1;
        }
    }
    public void ChangeVolumnMusic()
    {
        AudioAssistant.Instance.UpdateSoundSetting2(SoundVolumn.value, MusicVolumn.value);
        if (PlayerPrefs.HasKey("MusicVolumn"))
        {
            PlayerPrefs.SetFloat("MusicVolumn", MusicVolumn.value);
        }
        else
        {
            PlayerPrefs.SetFloat("MusicVolumn", 0.4f);
        }

    }
    public void ChangeVolumnSfx()
    {
        AudioAssistant.Instance.UpdateSoundSetting2(SoundVolumn.value, MusicVolumn.value);
        if (PlayerPrefs.HasKey("SfxVolumn"))
        {
            PlayerPrefs.SetFloat("SfxVolumn", SoundVolumn.value);
        }
        else
        {
            PlayerPrefs.SetFloat("SfxVolumn", 0.8f);
        }
    }
    public void PLayMusicStart()
    {
        AudioAssistant.Instance.PlayMusic("Start");
    }
    public void SetValueSliderSetting()
    {
        if (!PlayerPrefs.HasKey("SfxVolumn"))
        {
            PlayerPrefs.SetFloat("SfxVolumn", 0.8f);
        }
        if (!PlayerPrefs.HasKey("MusicVolumn"))
        {
            PlayerPrefs.SetFloat("MusicVolumn", 0.4f);
        }

        SoundVolumn.value = PlayerPrefs.GetFloat("SfxVolumn");
        MusicVolumn.value = PlayerPrefs.GetFloat("MusicVolumn");
        AudioAssistant.Instance.UpdateSoundSetting2(SoundVolumn.value, MusicVolumn.value);
    }
    public void ButtonOffCanvasBossRoom()
    {
        CanvasBossRoom.SetActive(false);
        GameStartScene.SetActive(true);
        QuantityDiamondDisplay.SetActive(true);
    }

    public void ButtonOnCanvasBossRoom()
    {
        CanvasBossRoom.SetActive(true);
        GameStartScene.SetActive(false);
        QuantityDiamondDisplay.SetActive(false);
    }
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            if (!AddedData)
            {
                SetTimeCountDown();
                PlayerPrefs.SetString("DateBefore", System.DateTime.Now.ToString());
                PlayerPrefs.SetInt("FirstGoShopSkin", 0);
                PlayerPrefs.SetInt("FirstGoShopWeapon", 0);
                PlayerPrefs.SetInt("ClaimedOfflineReward", 0);
                PlayerPrefs.SetInt("InterVictory", 0);
            }
        }
        else
        {
            AddedData = false;
        }
    }
    public bool AddedData = false;
    void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            if (!AddedData)
            {
                SetTimeCountDown();
                PlayerPrefs.SetString("DateBefore", System.DateTime.Now.ToString());
                PlayerPrefs.SetInt("FirstGoShopSkin", 0);
                PlayerPrefs.SetInt("FirstGoShopWeapon", 0);
                PlayerPrefs.SetInt("ClaimedOfflineReward", 0);
                PlayerPrefs.SetInt("InterVictory", 0);
            }
        }
        else
        {
            AddedData = false;
        }
    }
    private void OnApplicationQuit()
    {
        SetTimeCountDown();
        PlayerPrefs.SetString("DateBefore", System.DateTime.Now.ToString());
        PlayerPrefs.SetInt("FirstGoShopSkin", 0);
        PlayerPrefs.SetInt("FirstGoShopWeapon", 0);
        PlayerPrefs.SetInt("ClaimedOfflineReward", 0);
        PlayerPrefs.SetInt("InterVictory", 0);
    }
}