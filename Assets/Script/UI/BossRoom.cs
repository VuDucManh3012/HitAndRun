using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BossRoom : MonoBehaviour
{
    public TMP_Text TextQualityTicket;

    public int indexBoss;

    private CanvasManager CanvasManager;

    [Header("ListBoss")]
    public List<Boss> ListInfoBoss;

    [Header("InfoBoss")]
    private int numberId;
    private string name;
    private Sprite image;
    private Sprite imageShadow;
    private int healthCurrent;
    private int healthMax;
    private bool fighted;
    private int levelToFight;

    [Header("DisplayInfo")]
    public TMP_Text TextNameBoss;
    public Image ImageBoss;
    public TMP_Text TextHealthBoss;
    public Slider SliderHealthBoss;
    public GameObject HealthBoss;
    public GameObject HealthBarCantFight;
    public TMP_Text TextCantFight;

    [Header("ButtonFight")]
    public GameObject ButtonChallengeNormal;
    public GameObject ButtonLocked;
    public GameObject ButtonAdsChallenge;

    // Start is called before the first frame update
    void Start()
    {
        indexBoss = 0;
        CanvasManager = transform.parent.GetComponent<CanvasManager>();
        SetTextQualityTicket();
        DisplayInfoBoss();
    }
    public int GetQualityTicketBossRoom()
    {
        if (!PlayerPrefs.HasKey("TicketBossRoom"))
        {
            PlayerPrefs.SetInt("TicketBossRoom", 0);
        }
        return PlayerPrefs.GetInt("TicketBossRoom");
    }
    void SetTextQualityTicket()
    {
        TextQualityTicket.text = GetQualityTicketBossRoom().ToFormatString();
    }
    void GetInfo()
    {
        Boss currentBoss = ListInfoBoss[indexBoss];
        this.numberId = currentBoss.numberId;
        this.name = currentBoss.name;
        this.image = currentBoss.image;
        this.imageShadow = currentBoss.imageShadow;
        this.healthCurrent = currentBoss.healthCurrent;
        this.healthMax = currentBoss.healthMax;
        this.fighted = currentBoss.fighted;
        this.levelToFight = currentBoss.levelToFight;
    }
    void DisplayInfoBoss()
    {
        GetInfo();
        CheckCanFight();
        if (!CheckConditionCanFight.CanFight)
        {
            //Ko The Danh
            SetTextCantFight();
        }
        else
        {
            //Co the danh boss
            SetTextCanFight();
        }
        OnOffHealthBarCanFight();
        Debug.LogWarning(CheckConditionCanFight.CanFight + "," + CheckConditionCanFight.TypeFight);
    }
    class CheckConditionCanFight
    {
        public static bool CanFight;
        public static int TypeFight;
    }
    void CheckCanFight()
    {
        if (indexBoss > 0 && !ListInfoBoss[indexBoss - 1].fighted)
        {
            //chua danh boss truoc
            CheckConditionCanFight.CanFight = false;
            CheckConditionCanFight.TypeFight = 2;
        }
        else if (levelToFight > CanvasManager.characterController.myLevel)
        {
            //Khong du level
            CheckConditionCanFight.CanFight = false;
            CheckConditionCanFight.TypeFight = 1;
        }
        else if (GetQualityTicketBossRoom() == 0)
        {
            //ko co ticket, du lv , da danh boss trc
            CheckConditionCanFight.CanFight = true;
            CheckConditionCanFight.TypeFight = 2;
        }
        else
        {
            //Du ticket, du lv , da danh boss trc
            CheckConditionCanFight.CanFight = true;
            CheckConditionCanFight.TypeFight = 1;
        }
    }
    void OnOffHealthBarCanFight()
    {
        if (CheckConditionCanFight.CanFight)
        {
            //Cothedanh
            this.HealthBoss.SetActive(true);
            this.HealthBarCantFight.SetActive(false);
            Debug.Log("aaa");
        }
        else if (!CheckConditionCanFight.CanFight)
        {
            //Khongthedanh
            this.HealthBoss.SetActive(false);
            this.HealthBarCantFight.SetActive(true);
            Debug.Log("bbb");
        }
    }
    void OnOffButtonChallenge(GameObject ButtonOn)
    {
        this.ButtonChallengeNormal.SetActive(false);
        this.ButtonLocked.SetActive(false);
        this.ButtonAdsChallenge.SetActive(false);
        ButtonOn.SetActive(true);
    }
    void SetTextCanFight()
    {
        TextNameBoss.text = numberId.ToString() + ". " + name;
        ImageBoss.sprite = image;
        TextHealthBoss.text = healthCurrent + "/" + healthMax;
        SliderHealthBoss.value = healthCurrent / healthMax;

        if (CheckConditionCanFight.TypeFight == 1)
        {
            //Du ticket, du lv , da danh boss trc
            OnOffButtonChallenge(ButtonChallengeNormal);
        }
        else
        {
            //Ko co ticket, du lv , da danh boss trc
            OnOffButtonChallenge(ButtonAdsChallenge);
        }
    }

    void SetTextCantFight()
    {
        TextNameBoss.text = "????";
        ImageBoss.sprite = imageShadow;

        if (CheckConditionCanFight.TypeFight == 1)
        {
            //Khong du lv
            TextCantFight.text = "Level " + "<color=red>" + levelToFight;
        }
        else
        {
            //chua danh boss truoc
            TextCantFight.text = "Kill <color=red> PrevBoss";
        }
        OnOffButtonChallenge(ButtonLocked);
    }
    public void DisplayInfoPreviousBoss()
    {
        if (indexBoss > 0)
        {
            indexBoss -= 1;
            DisplayInfoBoss();
        }
    }
    public void DisplayInfoNextBoss()
    {
        if (indexBoss < ListInfoBoss.Count - 1)
        {
            indexBoss += 1;
            DisplayInfoBoss();
        }
    }
}
