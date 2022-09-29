using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RocketTeam.Sdk.Services.Ads;
public class CanvasGameStartController : MonoBehaviour
{
    private int currentDiamond;
    [Header("ButtonUpgradeLevel")]
    public GameObject LevelUpdateEnoughDiamond;
    public GameObject LevelUpdateNoEnoughDiamond;

    [Header("ButtonUpgradeOfflineReward")]
    public GameObject OfflineUpdateEnoughDiamond;
    public GameObject OfflineUpdateNoEnoughDiamond;

    [Header("ButtonUpgradeCritBoss")]
    public Text InfoUpgradeCritBoss;
    public GameObject UpgradeCritBossEnoughDiamond;
    public Text PriceUpgradeCritBoss;
    public GameObject UpgradeCritBossNoEnoughDiamond;

    public List<GameObject> ListObjectHidden;
    private bool Hiddened;

    [Header("Noads")]
    public GameObject ButtonNoAds;

    [Header("CanvasTouchPad")]
    public GameObject CanvasTouchPad;

    [Header("CanvasPopupOfflineReward")]
    public GameObject CanvasPopupOfflineReward;

    [Header("CanvasBossRoom")]
    public GameObject ButtonBossRoom;
    public GameObject NoticeBossRoom;
    public GameObject CanvasBossRoom;
    public Text CanvasStage;
    public GameObject CanvasDiamondQuality;

    private bool adsShowing;

    // Start is called before the first frame update
    void Start()
    {
        Hiddened = false;
        if (!PlayerPrefs.HasKey("TicketBossRoom"))
        {
            PlayerPrefs.SetInt("TicketBossRoom", 1);
        }
        if (PlayerPrefs.GetInt("TicketBossRoom") > 0)
        {
            NoticeBossRoom.SetActive(true);
        }
        if (System.Int32.Parse(PlayerPrefs.GetString("stage")) < 3)
        {
            ButtonBossRoom.SetActive(false);
        }
        CheckSceneStart();
        SetInfoUpgradeCritBoss();
        SetPriceUpgradeCritBoss();
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
        if (GameManager.Instance.Data.User.PurchasedNoAds)
        {
            ButtonNoAds.SetActive(false);
        }
        CanvasTouchPad.SetActive(true);
        if (CanvasPopupOfflineReward.activeInHierarchy)
        {
            transform.gameObject.SetActive(false);
        }
    }
    public void CheckSceneStart()
    {
        if (PlayerPrefs.HasKey("diamond"))
        {
            currentDiamond = PlayerPrefs.GetInt("diamond");
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
        if (currentDiamond >= PlayerPrefs.GetInt("UpgradeCritBoss") * 50)
        {
            UpgradeCritBossEnoughDiamond.SetActive(true);
            UpgradeCritBossNoEnoughDiamond.SetActive(false);
        }
        else
        {
            UpgradeCritBossEnoughDiamond.SetActive(false);
            UpgradeCritBossNoEnoughDiamond.SetActive(true);
        }
    }
    public void ButtonOnBossRoom()
    {
        this.transform.gameObject.SetActive(false);
        CanvasBossRoom.SetActive(true);
        CanvasStage.text = "Boss";
        CanvasDiamondQuality.gameObject.SetActive(false);
        BossRoom.Instance.Start();
    }
    public void ButtonUpgradeCritBoss()
    {
        if (PlayerPrefs.HasKey("diamond"))
        {
            currentDiamond = PlayerPrefs.GetInt("diamond");
        }
        if (currentDiamond >= PlayerPrefs.GetInt("UpgradeCritBoss") * 50)
        {
            //subtractDiamond
            currentDiamond -= PlayerPrefs.GetInt("UpgradeCritBoss") * 50;
            PlayerPrefs.SetInt("diamond", currentDiamond);
            CanvasManager.Instance.Save.ReadText();
            //
            UgradeCritBoss();
        }
        else
        {
            WatchADSUpgradeCritBoss();
        }
    }
    void SetPriceUpgradeCritBoss()
    {
        PriceUpgradeCritBoss.text = (PlayerPrefs.GetInt("UpgradeCritBoss") * 50).ToFormatString();
    }
    void UgradeCritBoss()
    {
        int upgradeCritBossCurrent = PlayerPrefs.GetInt("UpgradeCritBoss");
        upgradeCritBossCurrent += 1;
        PlayerPrefs.SetInt("UpgradeCritBoss", upgradeCritBossCurrent);
        ControllerPlayer.Instance.OnParticle(0);
        CheckSceneStart();
        SetInfoUpgradeCritBoss();
        SetPriceUpgradeCritBoss();
    }
    void WatchADSUpgradeCritBoss()
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
            AdManager.Instance.ShowAdsReward(OnCompleteAdsUpgradeCritBoss, "ClaimX2Gift");
        }
#if !PROTOTYPE
        else
        {
            adsShowing = true;
            AdManager.Instance.ShowAdsReward(OnCompleteAdsUpgradeCritBoss, "ClaimX2Gift");
        }
#endif
    }
    void OnCompleteAdsUpgradeCritBoss(int value)
    {
        AnalyticManager.LogWatchAds("UpgradeCritBoss", 1);
        UgradeCritBoss();
    }
    void SetInfoUpgradeCritBoss()
    {
        if (!PlayerPrefs.HasKey("UpgradeCritBoss"))
        {
            PlayerPrefs.SetInt("UpgradeCritBoss", 1);
        }
        int critBoss = 10 * PlayerPrefs.GetInt("UpgradeCritBoss");
        InfoUpgradeCritBoss.text = "+" + critBoss + "%";
    }
}

