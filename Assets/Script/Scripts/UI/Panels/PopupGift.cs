using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class PopupGift : UIPanel
{
    public override UI_PANEL GetID()
    {
        return UI_PANEL.PopupGift;
    }

    public static PopupGift Instance;

    [SerializeField]
    private Button CloseButton;

    [SerializeField]
    private TMP_Text giftTimerLb;

    [SerializeField]
    private TMP_Text rewardLb;

    [SerializeField]
    private GameObject btnClaim;

    public static void Show()
    {
        PopupGift newInstance = (PopupGift) GUIManager.Instance.NewPanel(UI_PANEL.PopupGift);
        Instance = newInstance;
        newInstance.OnAppear();
    }

    public void OnAppear()
    {
        if (isInited)
            return;

        base.OnAppear();

        // Init();
    }

    //
    // void UpdateGiftTimer()
    // {
    //     if (GM.NextTimeGetGift > DateTime.Now)
    //     {
    //         TimeSpan t = GM.NextTimeGetGift - DateTime.Now;
    //         giftTimerLb.text = string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
    //         btnClaim.SetActive(false);
    //     }
    //     else
    //     {
    //         giftTimerLb.text = string.Empty;
    //         btnClaim.SetActive(true);
    //     }
    // }
    //
    // void Init()
    // {
    //     rewardLb.text = Cfg.Game.OnlineReward.ToString();
    //     UpdateGiftTimer();
    // }
    //
    // public void Claim()
    // {
    //     AudioAssistant.Shot(TYPE_SOUND.GOAL);
    //     GM.Data.User.TotalPlaytime = 0;
    //     GM.Data.User.LastTimeLogin = DateTime.Now;
    //     GM.CalculateNextTimeGift();
    //     MainScreen.Instance.myGoldLb.ChangeText(GM.Data.User.Money, GM.Data.User.Money + Cfg.Game.OnlineReward, 1f);
    //     GM.Data.User.Money += Cfg.Game.OnlineReward;
    //     Database.SaveData();
    //     Close();
    // }
    //
    // protected override void RegisterEvent()
    // {
    //     base.RegisterEvent();
    //     CloseButton.onClick.AddListener(Close);
    //     Global.OnUpdatePerSec.AddListener(UpdateGiftTimer);
    // }
    //
    // protected override void UnregisterEvent()
    // {
    //     base.UnregisterEvent();
    //     CloseButton.onClick.RemoveListener(Close);
    //     if (Global)
    //         Global.OnUpdatePerSec.RemoveListener(UpdateGiftTimer);
    // }

    public override void OnDisappear()
    {
        base.OnDisappear();
        Instance = null;
    }
}