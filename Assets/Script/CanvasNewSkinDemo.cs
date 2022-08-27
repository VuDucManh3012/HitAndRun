using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RocketTeam.Sdk.Services.Ads;

public class CanvasNewSkinDemo : MonoBehaviour
{
    public List<Sprite> ListImageWeapon;
    public List<Sprite> ListImageSkin;
    public Image ImageSkin;

    private int TypeShop;
    private int indexShop;

    public List<GameObject> WeaponDemo;
    public List<Texture> SkinDemo;

    [Header("RendererTexture")]
    public List<GameObject> ListModelSkin;
    public List<GameObject> ListModelWeapon;

    public GameObject VictoryScene;

    public void SetImage(int indexSkin, int TypeShop)
    {
        this.TypeShop = TypeShop;
        this.indexShop = indexSkin;
        if (TypeShop == 1)
        {
            ImageSkin.sprite = ListImageWeapon[indexSkin];
            ListModelWeapon[indexSkin].SetActive(true);
        }
        else if (TypeShop == 2)
        {
            ImageSkin.sprite = ListImageSkin[indexSkin];
            ListModelSkin[indexSkin].SetActive(true);
        }
    }
    private bool adsShowing;
    public void WatchAdsToGetSkinDemo()
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
            AdManager.Instance.ShowAdsReward(OnCompleteAdsGetSkinDemo, "GetSkinDemo");
        }
#if !PROTOTYPE
        else
        {
            adsShowing = true;
            AdManager.Instance.ShowAdsReward(OnCompleteAdsGetSkinDemo, "GetSkinDemo");
        }
#endif
    }
    private void OnCompleteAdsGetSkinDemo(int value)
    {
        AnalyticManager.LogWatchAds("GetSkinDemo", 1);
        adsShowing = false;
        if (this.TypeShop == 1)
        {
            //weapon
            PlayerPrefs.SetInt("weaponSpecial" + this.indexShop, 2);
            //deo luon
            PlayerPrefs.SetString("CurrentWeapon", WeaponDemo[this.indexShop].name);
        }
        else if (this.TypeShop == 2)
        {
            //skin
            PlayerPrefs.SetInt("skinSpecial" + this.indexShop, 2);
            //deo luon
            PlayerPrefs.SetString("CurrentSkin", SkinDemo[this.indexShop].name);
        }
        VictoryScene.SetActive(true);
    }
}
