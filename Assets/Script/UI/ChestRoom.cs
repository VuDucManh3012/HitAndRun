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

    [Header("DiamondFly")]
    public MoneyClaimFx MoneyClaimFx;
    private Transform SpawnPoint;

    [Header("DisableImage")]
    public Animator DisableImageAnimator;
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
        
    }
    public void checkButtonAdsKey()
    {
        if (System.Int32.Parse(PlayerPrefs.GetString("key")) == 0)
        {
            StartCoroutine(OnButtonAdsKey());
            DisableImageAnimator.SetTrigger("Disable");
        }
        else
        {
            ButtonAdsKey.SetActive(false);
        }
    }
    IEnumerator OnButtonAdsKey()
    {
        yield return new WaitForSeconds(2f);
        ButtonAdsKey.SetActive(true);
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
    private void AddDiamond(int DiamondBonus)
    {
        //Add diamond
        CanvasManager.DiamondFlyAdsReward(DiamondBonus);
        //cap nhat lai diamond
        DiamondKey.ReadText();
        //FX
        MoneyClaimFx.ClaimMoneyOnlyFx(10, SpawnPoint);
    }
    public void SetSpawnPoint(Transform SpawnPointNew)
    {
        SpawnPoint = SpawnPointNew;
    }
    public void OpenChest(int indexChest)
    {
        if (System.Int32.Parse(PlayerPrefs.GetString("key")) >= 1)
        {
            if (EventSystem.current.currentSelectedGameObject.transform.GetChild(1).gameObject.activeInHierarchy)
            {
                SubtractKey();
                //DisableImage,set openedchest
                ListImageDisable[indexChest].SetActive(false);
                PlayerPrefs.SetInt("OpenedChest " + indexChest, 1);
                if (indexChest < 8)
                {
                    AddDiamond(indexChest + 1 * 100);
                }
                else
                {
                    int adsPoint = PlayerPrefs.GetInt("weaponSpecial" + currentItemSpecial) + 2;
                    PlayerPrefs.SetInt("weaponSpecial" + currentItemSpecial, adsPoint);
                    PlayerPrefs.SetString("CurrentWeapon", ListWeaponSpecial[currentItemSpecial].name);
                    ControllerPlayer.SetWeapon();
                    CanvasNewSkin.SetActive(true);
                    transform.GetChild(0).gameObject.SetActive(false);
                    ImageNewSkin.sprite = ListImageSpecialGift[currentItemSpecial];
                }
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
            int ran = Random.Range(1, 9);
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
