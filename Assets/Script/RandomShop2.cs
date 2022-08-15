using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using RocketTeam.Sdk.Services.Ads;

public class RandomShop2 : MonoBehaviour
{
    public GameObject[] ImageVery;
    public GameObject[] ImageDisable;
    public GameObject[] ImageSelect;
    public GameObject[] ImageTick;
    public GameObject[] YourSelect;
    public List<int> YourSelect2;
    public Animator ai;
    public Text diamond;
    int br;

    public GameObject[] ImageVerySpecial;
    public GameObject[] ImageDisableSpecial;
    public GameObject[] ImageSelectSpecial;
    public GameObject[] ImageTickSpecial;
    public GameObject[] YourSelectSpecial;
    public List<int> YourSelect2Special;
    public GameObject[] ImageAds;
    public Animator aiSpecial;
    int brSpecial;

    public GameObject[] weaponlistRight;
    public GameObject[] weaponlistLeft;

    public GameObject ListWeaponNormal;
    public GameObject ListWeaponSpecial;
    [Header("Character&Camera")]
    public GameObject Character;
    public GameObject MainCamera;
    [Header("GameStartScene")]
    public GameObject GameStartScene;
    [Header("CanvasDiamond")]
    public GameObject CanvasDiamond;
    [Header("CanvasPopUpDontOpen")]
    public GameObject CanvasPopUpDontOpen;
    private bool offing;
    // Start is called before the first frame update
    public void Start()
    {
        DiamondText();
        OnWeaponNormal();
        CheckBallSpecial();
        checkBall();
    }
    public void Update()
    {
        if (CanvasPopUpDontOpen.active && !offing)
        {
            StartCoroutine(OffCanvasPopupDontOpen());
            offing = true;
        }
    }
    IEnumerator OffCanvasPopupDontOpen()
    {
        yield return new WaitForSeconds(1.5f);
        CanvasPopUpDontOpen.SetActive(false);
        offing = false;
    }
    public void DiamondText()
    {
        if (!PlayerPrefs.HasKey("diamond"))
        {
            PlayerPrefs.SetString("diamond", 0.ToString());
        }
        else
        {
            diamond.text = PlayerPrefs.GetString("diamond");
        }
    }

    public void checkBall()
    {
        ImageDisable[0].SetActive(false);
        YourSelect2.Clear();
        for (int i = 0; i <= ImageDisable.Length - 1; i++)
        {
            ImageVery[i].SetActive(false);
            ImageSelect[i].SetActive(false);
            if (PlayerPrefs.GetInt("ball " + i) == 1)
            {
                ImageDisable[i].SetActive(false);
            }
            else
            {
                if (i != 0)
                {
                    YourSelect2.Add(i);
                }
            }
        }
        for (int i = 0; i <= ImageDisable.Length - 1; i++)
        {
            if (PlayerPrefs.GetString("CurrentWeapon") == weaponlistRight[i].name)
            {
                ImageTick[i].SetActive(true);
                weaponlistRight[i].SetActive(true);
                weaponlistLeft[i].SetActive(true);
                CheckBallSpecial();
            }
            else
            {
                ImageTick[i].SetActive(false);
                weaponlistRight[i].SetActive(false);
                weaponlistLeft[i].SetActive(false);
            }
        }
    }
    public void CheckBallSpecial()
    {
        //da mua vu khi gi theo ads
        for (int i = 0; i <= ImageAds.Length - 1; i++)
        {
            if (PlayerPrefs.GetInt("weaponSpecial" + i) < 2)
            {
                ImageAds[i].transform.Find("TextAds").gameObject.GetComponent<Text>().text = PlayerPrefs.GetInt("weaponSpecial" + i) + "/2";
            }
            else
            {
                ImageAds[i].SetActive(false);
                ImageDisableSpecial[i].SetActive(false);
            }
        }
        //da mua vu khi gi
        YourSelect2Special.Clear();
        for (int i = 0; i <= ImageDisableSpecial.Length - 1; i++)
        {
            ImageVerySpecial[i].SetActive(false);
            ImageSelectSpecial[i].SetActive(false);
            if (PlayerPrefs.GetInt("ballSpecial " + i) == 1)
            {
                ImageDisableSpecial[i].SetActive(false);
            }
            else
            {
                YourSelect2Special.Add(i);
            }
        }
        //

        //Check dag cam vu khi gi
        for (int i = 0; i <= ImageDisableSpecial.Length - 1; i++)
        {
            if (PlayerPrefs.GetString("CurrentWeapon") == weaponlistRight[i + 6].name)
            {
                ImageTickSpecial[i].SetActive(true);
                weaponlistRight[i + 6].SetActive(true);
                weaponlistLeft[i + 6].SetActive(true);
                checkBall();
            }
            else
            {
                ImageTickSpecial[i].SetActive(false);
                weaponlistRight[i + 6].SetActive(false);
                weaponlistLeft[i + 6].SetActive(false);
            }
        }
    }
    public void RandomButton()
    {
        if (YourSelect2.Count != 0)
        {
            int e = int.Parse(PlayerPrefs.GetString("diamond"));
            if (e >= 1500)
            {
                e -= 1500;
                string m = e.ToString();
                diamond.text = m.ToString();
                PlayerPrefs.SetString("diamond", diamond.text);
                int random = Random.RandomRange(1, YourSelect2.Count + 1);
                //
                br = YourSelect2.ToArray()[random - 1];
                //
                StartCoroutine(Wait());
                ai.enabled = true;
            }
            else
            {
                //
                Debug.Log("Don`t enough money");
                CanvasPopUpDontOpen.SetActive(true);
                CanvasPopUpDontOpen.transform.Find("Text").GetComponent<Text>().text = "Don`t enough money";
                //
            }
        }
    }
    public void RandomButtonSpecial()
    {
        if (YourSelect2Special.Count != 0)
        {
            int e = int.Parse(PlayerPrefs.GetString("diamond"));
            if (e >= 1500)
            {
                e -= 1500;
                string m = e.ToString();
                diamond.text = m.ToString();
                PlayerPrefs.SetString("diamond", diamond.text);
                int random = Random.RandomRange(1, YourSelect2Special.Count + 1);
                //
                brSpecial = YourSelect2Special.ToArray()[random - 1];
                //
                StartCoroutine(WaitSpecial());
            }
            else
            {
                //
                Debug.Log("Don`t enough money");
                CanvasPopUpDontOpen.SetActive(true);
                CanvasPopUpDontOpen.transform.Find("Text").GetComponent<Text>().text = "Don`t enough money";
                //
            }
        }
    }
    IEnumerator Wait()
    {

        yield return new WaitForSeconds(2f);
        ai.enabled = false;
        for (int i = 0; i <= ImageDisable.Length - 1; i++)
        {
            ImageSelect[i].SetActive(false);
            ImageVery[i].SetActive(false);
            YourSelect[i].SetActive(true);
        }
        ImageDisable[br].SetActive(false);
        ImageSelect[br].SetActive(true);
        ImageVery[br].SetActive(true);
        YourSelect[br].SetActive(true);

        yield return new WaitForSeconds(0.5f);
        ImageSelect[br].SetActive(false);
        ImageVery[br].SetActive(false);
        YourSelect[br].SetActive(true);
        PlayerPrefs.SetString("CurrentWeapon", weaponlistRight[br].name);

        PlayerPrefs.SetInt("ball " + br, 1);
        checkBall();
    }
    IEnumerator WaitSpecial()
    {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i <= ImageDisableSpecial.Length - 1; i++)
        {
            ImageSelectSpecial[i].SetActive(false);
            ImageVerySpecial[i].SetActive(false);
            YourSelectSpecial[i].SetActive(true);
        }
        ImageDisableSpecial[brSpecial].SetActive(false);
        ImageSelectSpecial[brSpecial].SetActive(true);
        ImageVerySpecial[brSpecial].SetActive(true);
        YourSelectSpecial[brSpecial].SetActive(true);

        yield return new WaitForSeconds(0.5f);
        ImageSelectSpecial[brSpecial].SetActive(false);
        ImageVerySpecial[brSpecial].SetActive(false);
        YourSelectSpecial[brSpecial].SetActive(true);

        PlayerPrefs.SetInt("ballSpecial " + brSpecial, 1);
        PlayerPrefs.SetString("CurrentWeapon", weaponlistRight[brSpecial + 6].name);

        CheckBallSpecial();
    }
    [ContextMenu("DeleteAll")]
    public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }
    [ContextMenu("Test")]
    public void Test()
    {
        Debug.Log(PlayerPrefs.GetString("diamond"));
    }
    public void OnWeaponNormal()
    {
        ListWeaponNormal.SetActive(true);
        ListWeaponSpecial.SetActive(false);
    }
    public void OnWeaponSpecial()
    {
        ListWeaponNormal.SetActive(false);
        ListWeaponSpecial.SetActive(true);
    }
    public void ChangeWeapon()
    {

        if (EventSystem.current.currentSelectedGameObject.name == "Ball (1)")
        {
            if (!ImageDisable[0].active)
            {
                ActiveFalse();
                ImageTick[0].SetActive(true);
                weaponlistRight[0].SetActive(true);
                weaponlistLeft[0].SetActive(true);
                PlayerPrefs.DeleteKey("CurrentWeapon");
                PlayerPrefs.SetString("CurrentWeapon", weaponlistRight[0].name);
            }
            else
            {

            }
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "Ball (2)")
        {
            if (!ImageDisable[1].active)
            {
                ActiveFalse();
                ImageTick[1].SetActive(true);
                weaponlistRight[1].SetActive(true);
                weaponlistLeft[1].SetActive(true);
                PlayerPrefs.DeleteKey("CurrentWeapon");
                PlayerPrefs.SetString("CurrentWeapon", weaponlistRight[1].name);
            }
            else
            {

            }
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "Ball (3)")
        {
            if (!ImageDisable[2].active)
            {
                ActiveFalse();
                ImageTick[2].SetActive(true);
                weaponlistRight[2].SetActive(true);
                weaponlistLeft[2].SetActive(true);
                PlayerPrefs.DeleteKey("CurrentWeapon");
                PlayerPrefs.SetString("CurrentWeapon", weaponlistRight[2].name);
            }
            else
            {

            }
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "Ball (4)")
        {
            if (!ImageDisable[3].active)
            {
                ActiveFalse();
                ImageTick[3].SetActive(true);
                weaponlistRight[3].SetActive(true);
                weaponlistLeft[3].SetActive(true);
                PlayerPrefs.DeleteKey("CurrentWeapon");
                PlayerPrefs.SetString("CurrentWeapon", weaponlistRight[3].name);
            }
            else
            {

            }
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "Ball (5)")
        {
            if (!ImageDisable[4].active)
            {
                ActiveFalse();
                ImageTick[4].SetActive(true);
                weaponlistRight[4].SetActive(true);
                weaponlistLeft[4].SetActive(true);
                PlayerPrefs.DeleteKey("CurrentWeapon");
                PlayerPrefs.SetString("CurrentWeapon", weaponlistRight[4].name);
            }
            else
            {

            }
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "Ball (6)")
        {
            if (!ImageDisable[5].active)
            {
                ActiveFalse();
                ImageTick[5].SetActive(true);
                weaponlistRight[5].SetActive(true);
                weaponlistLeft[5].SetActive(true);
                PlayerPrefs.DeleteKey("CurrentWeapon");
                PlayerPrefs.SetString("CurrentWeapon", weaponlistRight[5].name);
            }
            else
            {

            }
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "Ball (7)")
        {
            if (!ImageDisableSpecial[0].active)
            {
                ActiveFalse();
                ImageTickSpecial[0].SetActive(true);
                weaponlistRight[6].SetActive(true);
                weaponlistLeft[6].SetActive(true);
                PlayerPrefs.DeleteKey("CurrentWeapon");
                PlayerPrefs.SetString("CurrentWeapon", weaponlistRight[6].name);
            }
            else
            {
                CanvasPopUpDontOpen.SetActive(true);
                CanvasPopUpDontOpen.transform.Find("Text").GetComponent<Text>().text = "Unlock In ChestRoom";
                CheckBallSpecial();
            }
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "Ball (8)")
        {
            if (!ImageDisableSpecial[1].active)
            {
                ActiveFalse();
                ImageTickSpecial[1].SetActive(true);
                weaponlistRight[7].SetActive(true);
                weaponlistLeft[7].SetActive(true);
                PlayerPrefs.DeleteKey("CurrentWeapon");
                PlayerPrefs.SetString("CurrentWeapon", weaponlistRight[7].name);
            }
            else
            {
                CanvasPopUpDontOpen.SetActive(true);
                CanvasPopUpDontOpen.transform.Find("Text").GetComponent<Text>().text = "Unlock In ChestRoom";
                CheckBallSpecial();
            }
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "Ball (9)")
        {
            if (!ImageDisableSpecial[2].active)
            {
                ActiveFalse();
                ImageTickSpecial[2].SetActive(true);
                weaponlistRight[8].SetActive(true);
                weaponlistLeft[8].SetActive(true);
                PlayerPrefs.DeleteKey("CurrentWeapon");
                PlayerPrefs.SetString("CurrentWeapon", weaponlistRight[8].name);
            }
            else
            {
                CanvasPopUpDontOpen.SetActive(true);
                CanvasPopUpDontOpen.transform.Find("Text").GetComponent<Text>().text = "Unlock In ChestRoom";
                CheckBallSpecial();
            }
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "Ball (10)")
        {
            if (!ImageDisableSpecial[3].active)
            {
                ActiveFalse();
                ImageTickSpecial[3].SetActive(true);
                weaponlistRight[9].SetActive(true);
                weaponlistLeft[9].SetActive(true);
                PlayerPrefs.DeleteKey("CurrentWeapon");
                PlayerPrefs.SetString("CurrentWeapon", weaponlistRight[9].name);
            }
            else
            {
                CanvasPopUpDontOpen.SetActive(true);
                CanvasPopUpDontOpen.transform.Find("Text").GetComponent<Text>().text = "Unlock In ChestRoom";
                CheckBallSpecial();
            }
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "Ball (11)")
        {
            if (!ImageDisableSpecial[4].active)
            {
                ActiveFalse();
                ImageTickSpecial[4].SetActive(true);
                weaponlistRight[10].SetActive(true);
                weaponlistLeft[10].SetActive(true);
                PlayerPrefs.DeleteKey("CurrentWeapon");
                PlayerPrefs.SetString("CurrentWeapon", weaponlistRight[10].name);
            }
            else
            {
                CanvasPopUpDontOpen.SetActive(true);
                CanvasPopUpDontOpen.transform.Find("Text").GetComponent<Text>().text = "Unlock In ChestRoom";
                CheckBallSpecial();
            }
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "Ball (12)")
        {
            if (!ImageDisableSpecial[5].active)
            {
                ActiveFalse();
                ImageTickSpecial[5].SetActive(true);
                weaponlistRight[11].SetActive(true);
                weaponlistLeft[11].SetActive(true);
                PlayerPrefs.DeleteKey("CurrentWeapon");
                PlayerPrefs.SetString("CurrentWeapon", weaponlistRight[11].name);
            }
            else
            {
                CanvasPopUpDontOpen.SetActive(true);
                CanvasPopUpDontOpen.transform.Find("Text").GetComponent<Text>().text = "Unlock In ChestRoom";
                CheckBallSpecial();
            }
        }
    }
    public void ActiveFalse()
    {
        for (int i = 0; i < weaponlistRight.Length / 2; i++)
        {
            ImageTick[i].SetActive(false);
            ImageTickSpecial[i].SetActive(false);
            weaponlistRight[i].SetActive(false);
            weaponlistLeft[i].SetActive(false);
        }
        for (int i = weaponlistRight.Length / 2; i < weaponlistRight.Length; i++)
        {
            weaponlistRight[i].SetActive(false);
            weaponlistLeft[i].SetActive(false);
        }
    }
    public void BackScene()
    {
        transform.parent.gameObject.SetActive(false);
        GameStartScene.SetActive(true);
        Character.GetComponent<AutoSpin>().enabled = false;
        CanvasDiamond.SetActive(true);
        Character.GetComponent<ControllerPlayer>().ChangeCam("CamStart");
        Character.GetComponent<ControllerPlayer>().Save.GetComponent<Save>().ReadText();
        Character.GetComponent<ControllerPlayer>().Start();
    }

    private bool adsShowing = false;
    public void WatchAds()
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
            AdManager.Instance.ShowAdsReward(AddDiamondAds, "AddDiamondShopWeapon");
        }
#if !PROTOTYPE
        else
        {
            adsShowing = true;
            AdManager.Instance.ShowAdsReward(AddDiamondAds, "AddDiamondShopWeapon");
        }
#endif
    }
    private void AddDiamondAds(int value)
    {
        adsShowing = false;
        string diamondCurrent = PlayerPrefs.GetString("diamond");
        int diamondCurrentInt = int.Parse(diamondCurrent) + 1500;
        PlayerPrefs.SetString("diamond", diamondCurrentInt.ToString());
        DiamondText();
    }
}
