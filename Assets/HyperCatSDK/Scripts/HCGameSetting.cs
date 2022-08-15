﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HCGameSetting", menuName = "HyperCat/Game Setting")]
public class HCGameSetting : ScriptableObject
{
    [Header("Game, Version and Build setting")]
    public string GameName = "Prototype";

    public string GameVersion = "1.0";
    public int BuildVersion = 0;
    public int BundleVersion = 1;
    public string PackageName = "com.hypercat.prototype";
    public string BuildPath = "D:/HyperCat Build/";

    [Space, Header("Appstore ID (iOS Only)")]
    public string AppstoreID;

    [Space, Header("Ads IDs")]
    public string AdmobAndroidID;

    public string AdmobiOSID;

    public string InterAd;
    public string RewardedAd;
    public string BannerAd;
    public List<string> AOAListIds;
}