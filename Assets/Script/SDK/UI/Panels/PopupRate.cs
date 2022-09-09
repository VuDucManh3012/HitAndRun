using RocketTeam.Sdk.Services.Ads;
using System.Collections;
using System.Collections.Generic;
using HyperCatSdk;
using MoreMountains.NiceVibrations;
using UnityEngine;
using UnityEngine.UI;

public class PopupRate : UIPanel
{
    public override UI_PANEL GetID()
    {
        return UI_PANEL.PopupRate;
    }

    public static void Show()
    {
        PopupRate newInstance = (PopupRate) GUIManager.Instance.NewPanel(UI_PANEL.PopupRate);
        newInstance.OnAppear();
    }

    public void OnAppear()
    {
        if (isInited)
            return;

        base.OnAppear();

        starCount = 4;
        SetStar();
    }

    [SerializeField]
    Image[] imgStar;

    [SerializeField]
    Sprite[] sprStar;

    int starCount = 4;

    void SetStar()
    {
        for (int i = 0; i < 5; i++)
        {
            if (i <= starCount)
                imgStar[i].sprite = sprStar[0];
            else
                imgStar[i].sprite = sprStar[1];
        }
    }

    public void OnClickRate(int index)
    {
        HCVibrate.Haptic(HapticTypes.SoftImpact);
        starCount = index;
        SetStar();
    }

    public void OnClickConfirm()
    {
        Close();

        if (starCount < 4)
            PopupWarning.Show("Thanks for your feedback!");
        else
        {
#if UNITY_ANDROID
            Application.OpenURL(@"https://play.google.com/store/apps/details?id=" + GameManager.Instance.GameSetting.PackageName);
#elif UNITY_IOS
        if (!Device.RequestStoreReview())
        {
            Application.OpenURL(@"https://apps.apple.com/us/app/id" + GameManager.Instance.GameSetting.AppstoreID);
        }
#else
            Debug.Log("Rated in store!");
#endif
        }

        GM.Data.User.Rated = true;
        Database.SaveData();
        AnalyticManager.LogEvent("RateAction", "result", "Yes");
    }

    public void OnClickCancel()
    {
        AnalyticManager.LogEvent("RateAction", "result", "No");
        Close();
    }
}