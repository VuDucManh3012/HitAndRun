﻿#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.IO;
// using Facebook.Unity.Settings;
using GoogleMobileAds.Editor;
using UnityEditor;
using UnityEditor.Build.Content;
using UnityEngine;

public class HCTools : Editor
{
    public static HCGameSetting GameSetting
    {
        get
        {
            if (gameSetting == null)
                gameSetting = GetGameSetting();

            return gameSetting;
        }
    }

    private static HCGameSetting gameSetting;

    public static HCGameSetting GetGameSetting()
    {
        var path = "Assets/HyperCatSDK/";
        var fileEntries = Directory.GetFiles(path);
        for (int i = 0; i < fileEntries.Length; i++)
        {
            if (fileEntries[i].EndsWith(".asset"))
            {
                var item =
                    AssetDatabase.LoadAssetAtPath<HCGameSetting>(fileEntries[i].Replace("\\", "/"));
                if (item != null)
                {
                    return item;
                }
            }
        }

        return null;
    }

    [MenuItem("HyperCat Toolkit/Edit Game Setting")]
    public static void EditGameSetting()
    {
        Selection.activeObject = GameSetting;
        ShowInspector();
    }

    [MenuItem("HyperCat Toolkit/Documents/SDK Manual")]
    public static void OpenSDKManual()
    {
        Application.OpenURL("https://docs.google.com/document/d/1QlDfryP0HSpNj1QRlHb8OLUKmXF6m7jyIOsYl564m-s");
    }

    [MenuItem("HyperCat Toolkit/Documents/Game Requirements")]
    public static void OpenGameRequirement()
    {
        Application.OpenURL("https://docs.google.com/document/d/1HBBpZSXmC4deb0iEN7TxXpe2z_kzwbeOcOUvZVprigE");
    }

    [MenuItem("HyperCat Toolkit/Documents/Applovin Adapters Status")]
    public static void OpenApplovinAdaptersStatus()
    {
        Application.OpenURL("https://hypercatstudio.notion.site/4d15ddac180048fcbe26df95d83c7079?v=5fb09f0cdc094b4a89da872a11f22f3f");
    }
    
    [MenuItem("HyperCat Toolkit/Documents/Submit Build Double Check Manual")]
    public static void SubmitBuildDoubleCheckManual()
    {
        Application.OpenURL("https://hypercatstudio.notion.site/Quy-tr-nh-double-check-build-aab-6291703a8a0c4648a05f47650da82428");
    }

    #region Build

    [MenuItem("HyperCat Toolkit/Build Android/Verify Player Setting")]
    public static void ValidatePlayerSetting()
    {
        PlayerSettings.companyName = "HyperCat";
        PlayerSettings.productName = GameSetting.GameName;
        if (Application.HasProLicense())
            PlayerSettings.SplashScreen.showUnityLogo = false;
        PlayerSettings.bundleVersion = string.Format("{0}.{1}.{2}", GameSetting.GameVersion, GameSetting.BundleVersion, GameSetting.BuildVersion);

#if UNITY_ANDROID || UNITY_IOS
        PlayerSettings.defaultInterfaceOrientation = UIOrientation.Portrait;
#endif

#if UNITY_ANDROID
        string defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);
        if (!defines.Contains("FIREBASE"))
            defines += ";FIREBASE";
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, defines);
        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, GameSetting.PackageName);
        PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);
        PlayerSettings.Android.bundleVersionCode = GameSetting.BundleVersion;
        PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel19;
        PlayerSettings.Android.targetSdkVersion = AndroidSdkVersions.AndroidApiLevel30;
        PlayerSettings.Android.targetArchitectures = AndroidArchitecture.All;
#elif UNITY_IOS
        string defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS);
        if (!defines.Contains("FIREBASE"))
            defines += ";FIREBASE";
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, defines);
#endif
    }

    [MenuItem("HyperCat Toolkit/Build Android/Build APK (Testing)")]
    public static void BuildAPK()
    {
        GameSetting.BuildVersion += 1;
        EditorUtility.SetDirty(GameSetting);
        AssetDatabase.SaveAssets();
        VerifyAdsIds();
        ValidatePlayerSetting();
        ForceResolver();

        PlayerSettings.Android.useCustomKeystore = false;

        EditorUserBuildSettings.development = false;
        EditorUserBuildSettings.allowDebugging = false;
        EditorUserBuildSettings.androidCreateSymbolsZip = false;
        EditorUserBuildSettings.buildAppBundle = false;

        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);

        var playerPath = GameSetting.BuildPath + string.Format("{0} {1}.apk", GameSetting.PackageName, PlayerSettings.bundleVersion);
        BuildPipeline.BuildPlayer(GetScenePaths(), playerPath, BuildTarget.Android, BuildOptions.None);
    }

    [MenuItem("HyperCat Toolkit/Build Android/Build AAB (Submit)")]
    public static void BuildAAB()
    {
        PlayerSettings.Android.useCustomKeystore = true;
        if (string.IsNullOrEmpty(PlayerSettings.keyaliasPass))
        {
            EditorUtility.DisplayDialog("HyperCat Warning", "Publishing Setting - Verify your keystore setting before performing a submit build!", "Yes, sir!");
            SettingsService.OpenProjectSettings("Project/Player");
            return;
        }

        bool hasGoogleServiceFile = false;
        var path = "Assets/StreamingAssets/";
        var fileEntries = Directory.GetFiles(path);
        for (var i = 0; i < fileEntries.Length; i++)
        {
            if (fileEntries[i].Contains("google-services"))
            {
                hasGoogleServiceFile = true;
                break;
            }
        }

        if (!hasGoogleServiceFile)
        {
            EditorUtility.DisplayDialog("HyperCat Warning", "google-services.json file not found. Please contact your manager to get the correct file!", "Yes, sir!");
            HCDebug.LogError("google-services.json file not found at Assets/StreamingAssets/");
            return;
        }

        GameSetting.BuildVersion = 1;
        GameSetting.BundleVersion += 1;
        EditorUtility.SetDirty(GameSetting);
        AssetDatabase.SaveAssets();
        VerifyAdsIds();
        ValidatePlayerSetting();
        ForceResolver();

        EditorUserBuildSettings.development = false;
        EditorUserBuildSettings.allowDebugging = false;
        EditorUserBuildSettings.androidCreateSymbolsZip = false;
        EditorUserBuildSettings.buildAppBundle = true;

        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);

        var playerPath = GameSetting.BuildPath + string.Format("{0} {1}.aab", GameSetting.PackageName, PlayerSettings.bundleVersion);
        BuildPipeline.BuildPlayer(GetScenePaths(), playerPath, BuildTarget.Android, BuildOptions.None);
    }

    private static string[] GetScenePaths()
    {
        var scenes = new string[EditorBuildSettings.scenes.Length];
        for (var i = 0; i < scenes.Length; i++)
        {
            scenes[i] = EditorBuildSettings.scenes[i].path;
        }

        return scenes;
    }

    #endregion

    #region Configs

    [MenuItem("HyperCat Toolkit/Configs/Game Config")]
    public static void ShowGameConfig()
    {
        var cfg = GetGameConfig();
        if (cfg == null)
        {
            cfg = ScriptableObject.CreateInstance<GameConfig>();
            UnityEditor.AssetDatabase.CreateAsset(cfg, "Assets/Configs/GameConfig.asset");
            UnityEditor.AssetDatabase.SaveAssets();
        }

        cfg = GetGameConfig();
        Selection.activeObject = cfg;
        ShowInspector();
    }

    public static GameConfig GetGameConfig()
    {
        var path = "Assets/Configs/";
        var fileEntries = Directory.GetFiles(path);
        for (int i = 0; i < fileEntries.Length; i++)
        {
            if (fileEntries[i].EndsWith(".asset"))
            {
                var item =
                    AssetDatabase.LoadAssetAtPath<GameConfig>(fileEntries[i].Replace("\\", "/"));
                if (item != null)
                {
                    return item;
                }
            }
        }

        return null;
    }

    [MenuItem("HyperCat Toolkit/Configs/Sound Config")]
    public static void ShowSoundConfig()
    {
        var cfg = GetSoundConfig();
        if (cfg == null)
        {
            cfg = ScriptableObject.CreateInstance<SoundConfig>();
            AssetDatabase.CreateAsset(cfg, "Assets/Configs/SoundConfig.asset");
            AssetDatabase.SaveAssets();
        }

        cfg = GetSoundConfig();
        Selection.activeObject = cfg;
        ShowInspector();
    }

    public static SoundConfig GetSoundConfig()
    {
        var path = "Assets/Configs/";
        var fileEntries = Directory.GetFiles(path);
        for (int i = 0; i < fileEntries.Length; i++)
        {
            if (fileEntries[i].EndsWith(".asset"))
            {
                var item =
                    AssetDatabase.LoadAssetAtPath<SoundConfig>(fileEntries[i].Replace("\\", "/"));
                if (item != null)
                {
                    return item;
                }
            }
        }

        return null;
    }

    #endregion

    #region Third-party SDK

    [MenuItem("HyperCat Toolkit/Third-party Sdk/Verify Ads Ids")]
    public static void VerifyAdsIds()
    {
        AppLovinSettings.Instance.AdMobAndroidAppId = GameSetting.AdmobAndroidID;
        GoogleMobileAdsSettings.Instance.GoogleMobileAdsAndroidAppId = GameSetting.AdmobAndroidID;

        EditorUtility.SetDirty(AppLovinSettings.Instance);
        AssetDatabase.SaveAssets();
    }

    #endregion

    public static void ShowInspector()
    {
        EditorApplication.ExecuteMenuItem("Window/General/Inspector");
    }

    public static void ForceResolver()
    {
        EditorApplication.ExecuteMenuItem("Assets/External Dependency Manager/Android Resolver/Force Resolver");
    }
}
#endif