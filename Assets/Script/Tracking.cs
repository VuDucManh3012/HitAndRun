using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class Tracking : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("SessionInit"))
        {
            PlayerPrefs.SetInt("SessionInit", 1);
            //Thoi gian bat dau choi
            if (!PlayerPrefs.HasKey("TimeStartPlay"))
            {
                PlayerPrefs.SetString("TimeStartPlay", System.DateTime.Now.ToString());
            }
            //
            //So Lan Mo Gift
            if (!PlayerPrefs.HasKey("NumberTimesOpenGift"))
            {
                PlayerPrefs.SetInt("NumberTimesOpenGift", 0);
            }
            //
            //So lan vao ShopSkin
            if (!PlayerPrefs.HasKey("NumberTimeOpenShopSkin"))
            {
                PlayerPrefs.SetInt("NumberTimeOpenShopSkin", 0);
            }
            //
            //So lan vao ShopWeapon
            if (!PlayerPrefs.HasKey("NumberTimeOpenShopWeapon"))
            {
                PlayerPrefs.SetInt("NumberTimeOpenShopWeapon", 0);
            }
            //
            //So lan chet
            if (!PlayerPrefs.HasKey("NumberTimeDie"))
            {
                PlayerPrefs.SetInt("NumberTimeDie", 0);
            }
            //
            //So lan kill boss
            if (!PlayerPrefs.HasKey("NumberTimesKillBoss"))
            {
                PlayerPrefs.SetInt("NumberTimesKillBoss", 0);
            }
        }
    }
    private void OnApplicationQuit()
    {
        TimeInSession();
        TrackingNumberTimesOpenGift();
        TrackingNumberTimeOpenShopSkin();
        TrackingNumberTimeOpenShopWeapon();
        TrackingStagePassed();
        TrackingUpgradedLevel();
        TrackingUpgradedOfflineReward();
        TrackingNumberTimesSpinLuckyWheel();
        TrackingNumberTimesDie();
        TrackingNumberKillBoss();
        PlayerPrefs.DeleteKey("SessionInit");
    }
    public void TimeInSession()
    {
        //Tinh thoi gian choi 1 session
        System.DateTime DateBefore = System.DateTime.Parse(PlayerPrefs.GetString("TimeStartPlay"));
        System.DateTime DateNow = System.DateTime.Now;
        System.TimeSpan t = DateNow - DateBefore;
        double Distance = Mathf.Abs(ToSingle(t.TotalMinutes));
        Distance = Distance - Distance % 1;
        ///Distance la so phut choi
        ///HamTracking
        AnalyticManager.LogEvent("TimeInSession", new Firebase.Analytics.Parameter("TimeInSession", Distance));
        //Xoa Key
        PlayerPrefs.DeleteKey("TimeStartPlay");
        //////////////////////////////////////////////////////////////////////////////////////////////
    }
    public void NumberTimesOpenGift()
    {
        int numberTimesOpenGift = PlayerPrefs.GetInt("NumberTimesOpenGift");
        numberTimesOpenGift += 1;
        PlayerPrefs.SetInt("NumberTimesOpenGift", numberTimesOpenGift);
    }
    private void TrackingNumberTimesOpenGift()
    {
        double numberTimesOpenGift = PlayerPrefs.GetInt("NumberTimesOpenGift");
        //HamTracking
        AnalyticManager.LogEvent("NumberTimesOpenGift", new Firebase.Analytics.Parameter("NumberTimesOpenGift", numberTimesOpenGift));
        //
        //XoaKey
        PlayerPrefs.DeleteKey("NumberTimesOpenGift");
        /////////////////////////////////////////////////////////////////////////////////////////////
    }
    public void NumberTimeOpenShopSkin()
    {
        int numberTimeOpenShopSkin = PlayerPrefs.GetInt("NumberTimeOpenShopSkin");
        numberTimeOpenShopSkin += 1;
        PlayerPrefs.SetInt("NumberTimeOpenShopSkin", numberTimeOpenShopSkin);
    }
    private void TrackingNumberTimeOpenShopSkin()
    {
        double numberTimeOpenShopSkin = PlayerPrefs.GetInt("NumberTimeOpenShopSkin");
        //HamTracking
        AnalyticManager.LogEvent("NumberTimeOpenShopSkin", new Firebase.Analytics.Parameter("NumberTimeOpenShopSkin", numberTimeOpenShopSkin));
        //
        //XoaKey
        PlayerPrefs.DeleteKey("NumberTimeOpenShopSkin");
        /////////////////////////////////////////////////////////////////////////////////////////////
    }
    public void NumberTimeOpenShopWeapon()
    {
        int numberTimeOpenShopWeapon = PlayerPrefs.GetInt("NumberTimeOpenShopWeapon");
        numberTimeOpenShopWeapon += 1;
        PlayerPrefs.SetInt("NumberTimeOpenShopWeapon", numberTimeOpenShopWeapon);
    }
    private void TrackingNumberTimeOpenShopWeapon()
    {
        double numberTimeOpenShopWeapon = PlayerPrefs.GetInt("NumberTimeOpenShopWeapon");
        //HamTracking
        AnalyticManager.LogEvent("NumberTimeOpenShopWeapon", new Firebase.Analytics.Parameter("NumberTimeOpenShopWeapon", numberTimeOpenShopWeapon));
        //
        //XoaKey
        PlayerPrefs.DeleteKey("NumberTimeOpenShopWeapon");
        /////////////////////////////////////////////////////////////////////////////////////////////
    }
    private void TrackingStagePassed()
    {
        double stage = double.Parse(PlayerPrefs.GetString("stage"));
        //HamTracking
        AnalyticManager.LogEvent("Stage", new Firebase.Analytics.Parameter("Stage", stage));
        //
    }
    private void TrackingUpgradedLevel()
    {
        double UpdateLevel = PlayerPrefs.GetInt("UpdateLevel");
        //HamTracking
        AnalyticManager.LogEvent("UpdateLevel", new Firebase.Analytics.Parameter("UpdateLevel", UpdateLevel));
        //
    }
    private void TrackingUpgradedOfflineReward()
    {
        double numberTimesUpgradedOfflineReward = (PlayerPrefs.GetInt("DistanceMax") - 30) / 5;
        //HamTracking
        AnalyticManager.LogEvent("UpdateOfflineReward", new Firebase.Analytics.Parameter("UpdateOfflineReward", numberTimesUpgradedOfflineReward));
        //
    }
    public void NumberTimesSpinLuckyWheel()
    {
        if (!PlayerPrefs.HasKey("NumberTimesSpinLuckyWheel"))
        {
            PlayerPrefs.SetInt("NumberTimesSpinLuckyWheel", 0);
        }
        int numberTimesSpinLuckyWheel = PlayerPrefs.GetInt("NumberTimesSpinLuckyWheel");
        numberTimesSpinLuckyWheel += 1;
        PlayerPrefs.SetInt("NumberTimesSpinLuckyWheel", numberTimesSpinLuckyWheel);
    }
    private void TrackingNumberTimesSpinLuckyWheel()
    {
        double numberTimesSpinLuckyWheel = PlayerPrefs.GetInt("NumberTimesSpinLuckyWheel");
        //HamTracking
        AnalyticManager.LogEvent("NumberTimesSpinLuckyWheel", new Firebase.Analytics.Parameter("NumberTimesSpinLuckyWheel", numberTimesSpinLuckyWheel));
        //
    }
    public void NumberTimesDie()
    {
        int numbertimesDie = PlayerPrefs.GetInt("NumberTimeDie");
        numbertimesDie += 1;
        PlayerPrefs.SetInt("NumberTimeDie", numbertimesDie);
    }
    private void TrackingNumberTimesDie()
    {
        double numberTimeDie = PlayerPrefs.GetInt("NumberTimeDie");
        //HamTracking
        AnalyticManager.LogEvent("NumberTimeDie", new Firebase.Analytics.Parameter("NumberTimeDie", numberTimeDie));
        //
        //XoaKey
        PlayerPrefs.DeleteKey("NumberTimeDie");
    }
    private void TrackingNumberKillBoss()
    {
        double numberTimesKillBoss = PlayerPrefs.GetInt("NumberTimesKillBoss");
        //HamTracking
        AnalyticManager.LogEvent("NumberTimesKillBoss", new Firebase.Analytics.Parameter("NumberTimesKillBoss", numberTimesKillBoss));
        //
        //XoaKey
        PlayerPrefs.DeleteKey("NumberTimesKillBoss");
    }
    private static float ToSingle(double value)
    {
        return (float)value;
    }
}
