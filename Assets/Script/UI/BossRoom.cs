using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RocketTeam.Sdk.Services.Ads;
public class BossRoom : MonoBehaviour
{
    public TMP_Text TextQualityTicket;

    public int indexBoss;

    private bool adsShowing;
    public GameObject EnterBossRoom;
    private CanvasManager CanvasManager;

    public Text textStage;

    [Header("ListBoss")]
    public List<Boss> ListInfoBoss;

    [Header("InfoBoss")]
    private int numberId;
    private string name;
    private int healthCurrent;
    private Sprite image;
    private Sprite imageShadow;
    private int healthMax;
    private bool fighted;
    private int levelToFight;
    private bool dead;

    [Header("DisplayInfo")]
    public TMP_Text TextNameBoss;
    public Image ImageBoss;
    public GameObject ImageCleaned;
    public TMP_Text TextHealthBoss;
    public Slider SliderHealthBoss;
    public GameObject HealthBoss;
    public GameObject HealthBarCantFight;
    public TMP_Text TextCantFight;

    [Header("ButtonFight")]
    public GameObject ButtonChallengeNormal;
    public GameObject ButtonLocked;
    public GameObject ButtonAdsChallenge;
    public static BossRoom Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        indexBoss = 0;
        CanvasManager = transform.parent.GetComponent<CanvasManager>();
        SetTextQualityTicket();
        DisplayInfoBoss();
    }
    public int GetQualityTicketBossRoom()
    {
        return PlayerPrefs.GetInt("TicketBossRoom");
    }
    public Boss GetDataBoss()
    {
        return ListInfoBoss[indexBoss];
    }
    void SetTextQualityTicket()
    {
        TextQualityTicket.text = GetQualityTicketBossRoom().ToFormatString();
    }
    void GetInfo()
    {
        Boss currentBoss = ListInfoBoss[indexBoss];
        this.numberId = currentBoss.numberId;
        this.name = currentBoss.name;
        this.image = currentBoss.image;
        this.imageShadow = currentBoss.imageShadow;

        if (currentBoss.healthCurrent <= 0)
        {
            currentBoss.healthCurrent = currentBoss.healthMax;
        }

        this.healthCurrent = currentBoss.healthCurrent;
        this.healthMax = currentBoss.healthMax;
        this.fighted = currentBoss.fighted;
        this.levelToFight = currentBoss.levelToFight;
        this.dead = currentBoss.dead;
    }
    public Sprite GetImageBoss()
    {
        return this.image;
    }
    public void DisplayInfoBoss()
    {
        GetInfo();
        CheckCanFight();
        if (!CheckConditionCanFight.CanFight)
        {
            //Ko The Danh
            SetTextCantFight();
        }
        else
        {
            //Co the danh boss
            SetTextCanFight();
        }
        OnOffHealthBarCanFight();
        OnOffImageCleaned();
    }
    void OnOffImageCleaned()
    {
        if (dead)
        {
            ImageCleaned.SetActive(true);
        }
        else
        {
            ImageCleaned.SetActive(false);
        }
    }
    class CheckConditionCanFight
    {
        public static bool CanFight;
        public static int TypeFight;
    }
    void CheckCanFight()
    {
        if (indexBoss > 0 && !ListInfoBoss[indexBoss - 1].fighted)
        {
            //chua danh boss truoc
            CheckConditionCanFight.CanFight = false;
            CheckConditionCanFight.TypeFight = 2;
        }
        else if (levelToFight > CanvasManager.characterController.myLevel)
        {
            //Khong du level
            CheckConditionCanFight.CanFight = false;
            CheckConditionCanFight.TypeFight = 1;
        }
        else if (GetQualityTicketBossRoom() == 0)
        {
            //ko co ticket, du lv , da danh boss trc
            CheckConditionCanFight.CanFight = true;
            CheckConditionCanFight.TypeFight = 2;
        }
        else
        {
            //Du ticket, du lv , da danh boss trc
            CheckConditionCanFight.CanFight = true;
            CheckConditionCanFight.TypeFight = 1;
        }
    }
    void OnOffHealthBarCanFight()
    {
        if (CheckConditionCanFight.CanFight)
        {
            //Cothedanh
            this.HealthBoss.SetActive(true);
            this.HealthBarCantFight.SetActive(false);
        }
        else if (!CheckConditionCanFight.CanFight)
        {
            //Khongthedanh
            this.HealthBoss.SetActive(false);
            this.HealthBarCantFight.SetActive(true);
        }
    }
    void OnOffButtonChallenge(GameObject ButtonOn)
    {
        this.ButtonChallengeNormal.SetActive(false);
        this.ButtonLocked.SetActive(false);
        this.ButtonAdsChallenge.SetActive(false);
        ButtonOn.SetActive(true);
    }
    void SetTextCanFight()
    {
        TextNameBoss.text = numberId.ToString() + ". " + name;
        ImageBoss.sprite = image;
        TextHealthBoss.text = healthCurrent + "/" + healthMax;
        SliderHealthBoss.maxValue = healthMax;
        SliderHealthBoss.value = healthCurrent;

        if (CheckConditionCanFight.TypeFight == 1)
        {
            //Du ticket, du lv , da danh boss trc
            OnOffButtonChallenge(ButtonChallengeNormal);
        }
        else
        {
            //Ko co ticket, du lv , da danh boss trc
            OnOffButtonChallenge(ButtonAdsChallenge);
        }
    }

    void SetTextCantFight()
    {
        TextNameBoss.text = "????";
        ImageBoss.sprite = imageShadow;

        if (CheckConditionCanFight.TypeFight == 1)
        {
            //Khong du lv
            TextCantFight.text = "You Need <color=red> Level " + levelToFight;
        }
        else
        {
            //chua danh boss truoc
            TextCantFight.text = "You Need <color=red> Kill PrevBoss";
        }
        OnOffButtonChallenge(ButtonLocked);
    }
    public void DisplayInfoPreviousBoss()
    {
        if (indexBoss > 0)
        {
            indexBoss -= 1;
            DisplayInfoBoss();
        }
    }
    public void DisplayInfoNextBoss()
    {
        if (indexBoss < ListInfoBoss.Count - 1)
        {
            indexBoss += 1;
            DisplayInfoBoss();
        }
    }
    public void ButtonEnterBossRoomNormal()
    {
        if (PlayerPrefs.GetInt("TicketBossRoom") >= 1)
        {
            textStage.text = "Boss";
            int quantityTicket = PlayerPrefs.GetInt("TicketBossRoom");
            quantityTicket -= 1;
            PlayerPrefs.SetInt("TicketBossRoom", quantityTicket);
            EnterBossRoom.SetActive(true);
            SetTextQualityTicket();
            this.gameObject.SetActive(false);
            
        }
    }
    public void ButtonAdsBossRoom()
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
            AdManager.Instance.ShowAdsReward(OnCompleteAdsEnterBossRoom, "AdsEnterBossRoom");
        }
#if !PROTOTYPE
        else
        {
            adsShowing = true;
            AdManager.Instance.ShowAdsReward(OnCompleteAdsEnterBossRoom, "AdsEnterBossRoom");
        }
#endif
    }
    private void OnCompleteAdsEnterBossRoom(int value)
    {
        AnalyticManager.LogWatchAds("AdsEnterBossRoom", 1);
        adsShowing = false;
        EnterBossRoom.SetActive(true);
        textStage.text = "Boss";
        this.gameObject.SetActive(false);
    }
}
