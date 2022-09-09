using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using RocketTeam.Sdk.Services.Ads;


public class ChestRoom : MonoBehaviour
{
    public GameObject[] ListTranfrom;
    public GameObject[] ListItem;
    public GameObject[] ListItemAfterMix;
    public GameObject[] ListImageDisable;

    public Text KeyText;
    public ControllerPlayer ControllerPlayer;

    public Sprite[] ListImageSpecialGift;
    public GameObject ImageItemSpecial;
    public GameObject ImagePanel;
    public GameObject ButtonAdsKey;

    public List<GameObject> ListWeaponSpecial;

    [Header("CanvasNewSkin")]
    public Image ImageNewSkin;
    public GameObject CanvasNewSkin;
    private int currentItemSpecial;

    [Header("Save")]
    public Save DiamondKey;

    [Header("CanvasManager")]
    public CanvasManager CanvasManager;

    // Start is called before the first frame update
    void Start()
    {
        MixListItem();
        SortListItem();
        SetKeyText();
        SetImageSpecial();
    }
    private void Update()
    {
        checkButtonAdsKey();
    }
    public void checkButtonAdsKey()
    {
        if (System.Int32.Parse(PlayerPrefs.GetString("key")) == 0)
        {
            ButtonAdsKey.SetActive(true);
        }
        else
        {
            ButtonAdsKey.SetActive(false);
        }
    }
    public void SetImageSpecial()
    {
        for (int i = 0; i < 6; i++)
        {
            if (PlayerPrefs.GetInt("weaponSpecial" + i) == 0)
            {
                ImagePanel.GetComponent<Image>().sprite = ListImageSpecialGift[i];
                ImageItemSpecial.GetComponent<Image>().sprite = ListImageSpecialGift[i];
                currentItemSpecial = i;
                break;
            }
        }
    }
    public void AddDiamond(int DiamondBonus)
    {
        //Add diamond
        int currentDiamond = System.Int32.Parse(PlayerPrefs.GetString("diamond"));
        currentDiamond += DiamondBonus;
        PlayerPrefs.SetString("diamond", currentDiamond.ToString());
        //cap nhat lai diamond
        DiamondKey.ReadText();
    }
    public void OpenChest()
    {
        if (System.Int32.Parse(PlayerPrefs.GetString("key")) >= 1)
        {
            if (EventSystem.current.currentSelectedGameObject.transform.GetChild(1).gameObject.active)
            {
                SubtractKey();
            }
            //DisableImage,set openedchest
            if (EventSystem.current.currentSelectedGameObject.name == "Item (1)")
            {
                Debug.Log("1");
                ListImageDisable[0].SetActive(false);
                PlayerPrefs.SetInt("OpenedChest " + 0, 1);
                AddDiamond(100);
                CanvasManager.DiamondFlyAdsReward();
            }
            else if (EventSystem.current.currentSelectedGameObject.name == "Item (2)")
            {
                Debug.Log("2");
                ListImageDisable[1].SetActive(false);
                PlayerPrefs.SetInt("OpenedChest " + 1, 1);
                AddDiamond(200);
                CanvasManager.DiamondFlyAdsReward();
            }
            else if (EventSystem.current.currentSelectedGameObject.name == "Item (3)")
            {
                Debug.Log("3");
                ListImageDisable[2].SetActive(false);
                PlayerPrefs.SetInt("OpenedChest " + 2, 1);
                AddDiamond(300);
                CanvasManager.DiamondFlyAdsReward();
            }
            else if (EventSystem.current.currentSelectedGameObject.name == "Item (4)")
            {
                Debug.Log("4");
                ListImageDisable[3].SetActive(false);
                PlayerPrefs.SetInt("OpenedChest " + 3, 1);
                AddDiamond(400);
                CanvasManager.DiamondFlyAdsReward();
            }
            else if (EventSystem.current.currentSelectedGameObject.name == "Item (5)")
            {
                Debug.Log("5");
                ListImageDisable[4].SetActive(false);
                PlayerPrefs.SetInt("OpenedChest " + 4, 1);
                AddDiamond(500);
                CanvasManager.DiamondFlyAdsReward();
            }
            else if (EventSystem.current.currentSelectedGameObject.name == "Item (6)")
            {
                Debug.Log("6");
                ListImageDisable[5].SetActive(false);
                PlayerPrefs.SetInt("OpenedChest " + 5, 1);
                AddDiamond(600);
                CanvasManager.DiamondFlyAdsReward();
            }
            else if (EventSystem.current.currentSelectedGameObject.name == "Item (7)")
            {
                Debug.Log("7");
                ListImageDisable[6].SetActive(false);
                PlayerPrefs.SetInt("OpenedChest " + 6, 1);
                AddDiamond(700);
                CanvasManager.DiamondFlyAdsReward();
            }
            else if (EventSystem.current.currentSelectedGameObject.name == "Item (8)")
            {
                Debug.Log("8");
                ListImageDisable[7].SetActive(false);
                PlayerPrefs.SetInt("OpenedChest " + 7, 1);
                AddDiamond(800);
                CanvasManager.DiamondFlyAdsReward();

            }
            else if (EventSystem.current.currentSelectedGameObject.name == "Item (9)")
            {
                Debug.Log("9");
                ListImageDisable[8].SetActive(false);
                PlayerPrefs.SetInt("OpenedChest " + 8, 1);
                int adsPoint = PlayerPrefs.GetInt("weaponSpecial" + currentItemSpecial) + 2;
                PlayerPrefs.SetInt("weaponSpecial" + currentItemSpecial, adsPoint);
                PlayerPrefs.SetString("CurrentWeapon", ListWeaponSpecial[currentItemSpecial].name);
                CanvasNewSkin.SetActive(true);
                transform.GetChild(0).gameObject.SetActive(false);
                ImageNewSkin.sprite = ListImageSpecialGift[currentItemSpecial];
            }
        }
    }
    public void SubtractKey()
    {
        //subtract key
        int CurrentKey = System.Int32.Parse(PlayerPrefs.GetString("key"));
        CurrentKey -= 1;
        PlayerPrefs.SetString("key", CurrentKey.ToString());
        SetKeyText();
        checkButtonAdsKey();
    }
    public void SetKeyText()
    {
        KeyText.text = PlayerPrefs.GetString("key");
    }
    public void MixListItem()
    {
        for (int i = 0; i < ListItem.Length; i++)
        {
            int ran = Random.RandomRange(1, 9);
            GameObject temp = ListItem[ran];
            ListItem[ran] = ListItem[i];
            ListItem[i] = temp;
        }
        for (int i = 0; i < ListItem.Length; i++)
        {
            ListItem[i].transform.position = ListTranfrom[i].transform.position;
            PlayerPrefs.SetString("ChestRoomAfterMix " + i, ListItem[i].name);
        }

    }
    public void FirstTimeMeet()
    {
        if (PlayerPrefs.HasKey("ChestRoomAfterMix " + 0))
        {
            SortListItem();
        }
        else
        {
            MixListItem();
            SortListItem();
        }
    }
    public void SortListItem()
    {
        ListItemAfterMix = new GameObject[9];
        for (int i = 0; i < ListItem.Length; i++)
        {
            for (int i2 = 0; i2 < ListItem.Length; i2++)
            {
                if (PlayerPrefs.GetString("ChestRoomAfterMix " + i) == ListItem[i2].name)
                {
                    ListItemAfterMix[i] = ListItem[i2];
                }
            }

        }
        for (int i = 0; i < ListItem.Length; i++)
        {
            ListItemAfterMix[i].transform.position = ListTranfrom[i].transform.position;
        }
    }
    public void AdsKey()
    {
        ///Ads
        AdManager.Instance.ShowAdsReward(completeAds, "AddKeyChestRoom");
        void completeAds(int value)
        {
            AnalyticManager.LogWatchAds("AddKey", 1);
            int currentKey = int.Parse(PlayerPrefs.GetString("key"));
            currentKey += 3;
            PlayerPrefs.SetString("key", currentKey.ToString());
            SetKeyText();
        }
    }
    [ContextMenu("DeleteKey")]
    public void DeleKey()
    {
        PlayerPrefs.SetString("key", 9.ToString());
        for (int i = 0; i < 9; i++)
        {
            PlayerPrefs.DeleteKey("ChestRoomAfterMix " + i);
            PlayerPrefs.DeleteKey("OpenedChest " + i);
        }
    }
}
