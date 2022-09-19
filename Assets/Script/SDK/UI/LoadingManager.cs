using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : Singleton<LoadingManager>
{
    private UILoading loading;
    private Slider slider;
    private Dictionary<SCENE_INDEX, Action> SceneLoadedSpecialAction = new Dictionary<SCENE_INDEX, Action>();

    void SetSceneAction(SCENE_INDEX scene, Action action)
    {
        if (SceneLoadedSpecialAction.ContainsKey(scene))
            SceneLoadedSpecialAction[scene] = action;
        else
            SceneLoadedSpecialAction.Add(scene, action);
    }

    public void LoadScene(SCENE_INDEX type,Slider sli, Action action = null, bool overrideNullAction = false)
    {
        if (loading == null)
            loading = GUIManager.Instance.LoadingUI;

        if (overrideNullAction || action != null)
            SetSceneAction(type, action);

        EventGlobalManager.Instance.OnStartLoadScene.Dispatch();
        StartCoroutine(LoadSceneAsync(type));
        slider = sli;
    }

    IEnumerator LoadSceneAsync(SCENE_INDEX index)
    {
        yield return Yielders.Get(0.1f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync((int) index);
        asyncLoad.allowSceneActivation = false;
        Application.backgroundLoadingPriority = ThreadPriority.Normal;
        while (!asyncLoad.isDone)
        {
            float progerss = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            slider.value = progerss;
            if (asyncLoad.progress >= 0.90f)
                asyncLoad.allowSceneActivation = true;
                
            yield return null;
        }

        System.GC.Collect(2, GCCollectionMode.Forced);
        Resources.UnloadUnusedAssets();

        EventGlobalManager.Instance.OnFinishLoadScene.Dispatch();
        if (SceneLoadedSpecialAction.ContainsKey(index) && SceneLoadedSpecialAction[index] != null)
        {
            SceneLoadedSpecialAction[index].Invoke();
        }

        yield return Yielders.Get(0.05f);
        loading.gameObject.SetActive(false);
    }
}