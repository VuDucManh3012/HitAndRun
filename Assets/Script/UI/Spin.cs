using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using RocketTeam.Sdk.Services.Ads;
using TMPro;
public class Spin : MonoBehaviour
{
    public int numberOfGift = 7;
    public float timeRotate;
    public float numberCircleRotate;

    private const float CIRCLE = 360.0f;
    private float angleofOneGift;

    public Transform parrent;
    private float currentTime;

    public AnimationCurve curve;
    public TMP_Text Diamond;
    public Slider ProcessSlider;
    public Text ProcessText;
    public Text SpinSlotText;
    public GameObject SpinButton;
    public GameObject ImageDisableSpinButton;
    public GameObject[] ImageDisable;

    public int indexGift;

    private int indexdiamondbonus;
    private int diamondbonus;

    public GameObject[] listData;

    private bool AccessSpin;
    private bool AddDayBefore;

    public Save DiamondKey;
    [Header("PopupGiftLuckyWheel")]
    public GameObject PopUpGiftLuckyWheel;
    public Text TextPopUpGiftLuckyWheel;

    [Header("Key")]
    public GameObject AnimKey;

    [Header("CanvasChestRoom2")]
    public GameObject CanvasChestRoom2;
    public GameObject CanvasWheelLucky;

    [Header("CanvasPopUpProcess")]
    public GameObject CanvasPopUpProcess;
    public List<int> SkinNoBuy;
    public List<int> WeaponNoBuy;
    public List<Texture> ListSkin;
    public List<GameObject> ListWeapon;

    [Header("CanvasManager")]
    public CanvasManager CanvasManager;

    private bool OnAnimSliderProcess = false;

    /*
    PlayPref : SpinSlot-so vong quay , TimeNextSpin-tgian +1 vong quay tiep theo , WheelProcess-tien do vong quay hien tai
    */
    [ContextMenu("Delete")]
    public void Delete()
    {
        PlayerPrefs.DeleteKey("SpinSlot");
    }
    // Start is called before the first frame update
    public void Start()
    {
        AddDayBefore = true;
        angleofOneGift = CIRCLE / numberOfGift;
        SetPositionData();
        ReadText();
        SetProcess();
        CheckSpinSlot();
        CheckTimeSpin();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        CheckTimeSpin();
        if (OnAnimSliderProcess)
        {
            AnimProcess();
        }
    }
    void AnimProcess()
    {
        ProcessSlider.value = ProcessCurrent;
        ProcessCurrent += 0.05f;
        if (ProcessCurrent >= ProcessEnding)
        {
            OnAnimSliderProcess = false;
            AddProcess();
            SetProcess();
        }
    }
    void SetPositionData()
    {
        for (int i = 0; i < parrent.childCount; i++)
        {
            parrent.GetChild(i).localEulerAngles = new Vector3(0, 0, (-CIRCLE / numberOfGift) * i + 22.5f);
            //parrent.GetChild(i).GetChild(0).GetComponent<Text>().text = (i + 1).ToString();
        }
    }
    public void CheckSpinSlot()
    {
        if (PlayerPrefs.HasKey("SpinSlot"))
        {
            //co
            int spinslot = PlayerPrefs.GetInt("SpinSlot");
            SpinSlotText.text = PlayerPrefs.GetInt("SpinSlot").ToString();
            if (spinslot >= 1)
            {
                ImageDisableSpinButton.SetActive(false);
                AccessSpin = true;
            }
            else
            {
                ImageDisableSpinButton.SetActive(true);
                AccessSpin = false;
            }
        }
        else
        {
            //khong
            PlayerPrefs.SetInt("SpinSlot", 1);
            CheckSpinSlot();
        }
    }
    [ContextMenu("TestSpin")]
    public void TestSpin()
    {
        AddSpinSlot(10);
    }
    public void AddSpinSlot(int x)
    {
        int spinslot = PlayerPrefs.GetInt("SpinSlot") + x;
        PlayerPrefs.SetInt("SpinSlot", spinslot);
        CheckSpinSlot();
    }
    public void CheckTimeSpin()
    {
        if (PlayerPrefs.HasKey("TimeNextSpin"))
        {
            //co
            System.DateTime dayBefore = System.DateTime.Parse(PlayerPrefs.GetString("TimeNextSpin"));
            System.DateTime dayNow = System.DateTime.Now;
            System.TimeSpan t = dayBefore - dayNow;
            float Diff = Mathf.Abs(ToSingle(t.TotalSeconds));
            Diff = Diff - Diff % 1;
            float spinslot = ToSingle(Diff) / 14400 - ToSingle(Diff) / 14400 % 1;
            float timeNextSpin = 14400 - (ToSingle(Diff) - (14400 * spinslot));
            System.TimeSpan nextTime = System.TimeSpan.FromSeconds(timeNextSpin);

            AddSpinSlot(System.Convert.ToInt32(spinslot));
            //
            if (AddDayBefore)
            {
                System.DateTime t2 = System.DateTime.Now.AddSeconds(-(14400 - timeNextSpin));
                PlayerPrefs.SetString("TimeNextSpin", t2.ToString());
                AddDayBefore = false;
            }
            //
            if (!AccessSpin)
            {
                //
                SpinButton.transform.Find("Text").gameObject.GetComponent<Text>().text = nextTime.ToString();
                //
            }
            else
            {
                SpinButton.transform.Find("Text").gameObject.GetComponent<Text>().text = "FREE SPIN";
            }
        }
        else
        {
            //khong
            PlayerPrefs.SetString("TimeNextSpin", System.DateTime.Now.ToString());
            CheckTimeSpin();
        }
    }
    public void AddTimeNextSpin()
    {
        PlayerPrefs.SetString("TimeNextSpin", System.DateTime.Now.AddHours(4).ToString());
    }
    IEnumerator RotateWheel()
    {
        //SpinSlot--
        SubTractSpinSlot();
        //
        float startAngle = transform.localEulerAngles.z;
        currentTime = 0;
        int indexGiftRandom = Random.Range(1, numberOfGift + 1);
        indexdiamondbonus = indexGiftRandom - 1;
        indexGift = indexGiftRandom;
        float angleWant = (numberCircleRotate * CIRCLE) + angleofOneGift * indexGiftRandom - startAngle - angleofOneGift - 22.5f;

        while (currentTime < timeRotate)
        {
            yield return new WaitForEndOfFrame();
            currentTime += Time.deltaTime;

            float angleCurrent = angleWant * curve.Evaluate(currentTime / timeRotate);
            this.transform.localEulerAngles = new Vector3(0, 0, angleCurrent + startAngle);
        }
        AfterProcess();
    }
    private float ProcessCurrent;
    private float ProcessEnding;
    public void AfterProcess()
    {

        //AddDiamond
        DiamondBonus();
        AddDiamond();
        ReadText();
        //
        ProcessEnding = PlayerPrefs.GetInt("WheelProcess") + 1;
        ProcessCurrent = PlayerPrefs.GetInt("WheelProcess");
        OnAnimSliderProcess = true;
    }
    [ContextMenu("Rotate")]
    public void RotateNow()
    {
        if (AccessSpin)
        {
            StartCoroutine(RotateWheel());
        }
    }
    private bool adsShowing = false;
    public void AdsRotateNow()
    {
        ///Ads
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
            AdManager.Instance.ShowAdsReward(completeAds, "AddSpinSlot");
        }
#if !PROTOTYPE
        else
        {
            adsShowing = true;
            AdManager.Instance.ShowAdsReward(completeAds, "AddSpinSlot");
        }
#endif
    }
    private void completeAds(int value)
    {
        adsShowing = false;
        //Spin
        AddSpinSlot(1);
        RotateNow();
    }
    public void ReadText()
    {
        Diamond.text = PlayerPrefs.GetInt("diamond").ToFormatString();
    }
    public void checkKey()
    {
        if (System.Int32.Parse(PlayerPrefs.GetString("key")) >= 3)
        {
            CanvasChestRoom2.SetActive(true);
            CanvasWheelLucky.SetActive(false);
        }
    }
    public void DiamondBonus()
    {
        //lay so kim cuong thang
        diamondbonus = System.Int32.Parse(listData[indexdiamondbonus].transform.GetChild(0).gameObject.GetComponent<Text>().text);
        TextPopUpGiftLuckyWheel.text = diamondbonus.ToString();
        if (diamondbonus == 0)
        {
            int currentKey = System.Int32.Parse(PlayerPrefs.GetString("key"));
            currentKey += 1;
            PlayerPrefs.SetString("key", currentKey.ToString());
            AnimKey.SetActive(true);
        }
        else
        {
            //Bat PopUp
            PopUpGiftLuckyWheel.SetActive(true);
        }
    }
    public void AddDiamond()
    {
        if (diamondbonus != 0)
        {
            CanvasManager.DiamondFly(diamondbonus, null);
        }
    }
    public void AddProcess()
    {
        if (PlayerPrefs.HasKey("WheelProcess"))
        {
            //Co r
            int processNew = PlayerPrefs.GetInt("WheelProcess") + 1;
            PlayerPrefs.SetInt("WheelProcess", processNew);
            if (processNew == 4)
            {
                RandomProcess();
            }
            else if (processNew == 7)
            {
                RandomProcess();
            }
            else if (processNew == 10)
            {
                RandomProcess();
            }
        }
        else
        {
            //Ko co
            PlayerPrefs.SetInt("WheelProcess", 1);
        }
    }
    public void RandomProcess()
    {
        SkinNoBuy.Clear();
        WeaponNoBuy.Clear();
        for (int i = 1; i < 6; i++)
        {
            if (!PlayerPrefs.HasKey("skin " + i))
            {
                SkinNoBuy.Add(i);
            }
            if (!PlayerPrefs.HasKey("ball " + i))
            {
                WeaponNoBuy.Add(i);
            }
        }
        if (SkinNoBuy.Count != 0 && WeaponNoBuy.Count != 0)
        {
            if (Random.Range(0, 2) == 1)
            {
                int ran = Random.Range(0, SkinNoBuy.Count);
                PlayerPrefs.SetInt("skin " + SkinNoBuy[ran], 1);
                CanvasPopUpProcess.SetActive(true);
                CanvasPopUpProcess.GetComponent<CanvasPopupProcess>().ChangeSkin(SkinNoBuy[ran]);
                PlayerPrefs.SetString("CurrentSkin", ListSkin[SkinNoBuy[ran]].name);
            }
            else
            {
                int ran = Random.Range(0, WeaponNoBuy.Count);
                PlayerPrefs.SetInt("ball " + WeaponNoBuy[ran], 1);
                CanvasPopUpProcess.SetActive(true);
                CanvasPopUpProcess.GetComponent<CanvasPopupProcess>().ChangeSkin(WeaponNoBuy[ran] + 6);
                PlayerPrefs.SetString("CurrentWeapon", ListWeapon[WeaponNoBuy[ran]].name);
            }
        }
        else if (SkinNoBuy.Count != 0)
        {
            int ran = Random.Range(0, SkinNoBuy.Count);
            PlayerPrefs.SetInt("skin " + SkinNoBuy[ran], 1);
            CanvasPopUpProcess.GetComponent<CanvasPopupProcess>().ChangeSkin(SkinNoBuy[ran]);
            CanvasPopUpProcess.SetActive(true);
            PlayerPrefs.SetString("CurrentSkin", ListSkin[SkinNoBuy[ran]].name);
        }
        else if (WeaponNoBuy.Count != 0)
        {
            int ran = Random.Range(0, WeaponNoBuy.Count);
            PlayerPrefs.SetInt("ball " + WeaponNoBuy[ran], 1);
            CanvasPopUpProcess.GetComponent<CanvasPopupProcess>().ChangeSkin(WeaponNoBuy[ran] + 6);
            CanvasPopUpProcess.SetActive(true);
            PlayerPrefs.SetString("CurrentWeapon", ListWeapon[WeaponNoBuy[ran]].name);
        }
    }
    [ContextMenu("DeleteWheelProcess")]
    public void DeleteWheelProcess()
    {
        PlayerPrefs.SetInt("WheelProcess", 0);
    }
    public void SetProcess()
    {
        int processNow = PlayerPrefs.GetInt("WheelProcess");
        if (processNow >= 10)
        {
            for (int i = 0; i <= 3; i++)
            {
                ImageDisable[i].SetActive(false);
            }
        }
        else if (processNow >= 7)
        {
            for (int i = 0; i <= 2; i++)
            {
                ImageDisable[i].SetActive(false);
            }
        }
        else if (processNow >= 4)
        {
            for (int i = 0; i <= 1; i++)
            {
                ImageDisable[i].SetActive(false);
            }
        }
        else if (processNow >= 0)
        {
            ImageDisable[0].SetActive(false);
        }

        if (processNow <= 10)
        {
            ProcessSlider.value = processNow;
            ProcessText.text = "Process : " + processNow.ToString() + "/10";
        }
        else
        {
            PlayerPrefs.SetInt("WheelProcess", 10);
            SetProcess();
        }
    }
    public void SubTractSpinSlot()
    {
        int spinslot = PlayerPrefs.GetInt("SpinSlot") - 1;
        PlayerPrefs.SetInt("SpinSlot", spinslot);
        CheckSpinSlot();
    }
    public static float ToSingle(double value)
    {
        return (float)value;
    }
    public void BackScene()
    {
        transform.parent.parent.parent.gameObject.SetActive(false);
        DiamondKey.ReadText();
    }
}
