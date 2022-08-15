using System;
using GoogleMobileAds.Api;

public class AppOpenAdLauncher : Singleton<AppOpenAdLauncher>
{
    public void Init()
    {
        MobileAds.Initialize(status => { AppOpenAdManager.Instance.LoadAd(); });
    }

    private void OnApplicationPause(bool pause)
    {
        if (!pause && AppOpenAdManager.ConfigResumeApp && !AppOpenAdManager.ResumeFromAds)
        {
            if (!AppOpenAdManager.Instance.ShowAdIfAvailable())
            {
                AppOpenAdManager.Instance.LoadAd();
            }
        }
    }

    public void TryGetAOA()
    {
        if (AppOpenAdManager.TryGetAOATime > 0)
            Invoke(nameof(GetAOA), AppOpenAdManager.TryGetAOATime);
    }

    void GetAOA()
    {
        AppOpenAdManager.Instance.LoadAd();
    }
}