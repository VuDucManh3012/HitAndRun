using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasAttackBoss : MonoBehaviour
{
    public Slider SliderBackGroundHealthBoss;
    public Slider SliderHealthBoss;
    private float valueBackGroungHealthBoss;

    public Slider SliderTime;

    [Header("TapAttack")]
    public GameObject ImageLeft;
    public GameObject ImageRight;
    public GameObject TextLeft;
    public GameObject TextRight;
    bool attackCrit;

    [Header("CanvasResultBossRoom")]
    public GameObject CanvasResultBossRoom;

    public GameObject ButtonAttack;

    private Boss DataBoss;
    private bool statusBoss;

    private bool AttackRight;

    private int myLevel;
    private int Damagecrit;

    private int currentHealth;
    private int maxHealth;

    private bool timeoutChecked;
    public static CanvasAttackBoss Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        SetSliderStart();
        myLevel = System.Convert.ToInt32(ControllerPlayer.Instance.GetMyLevel());
        SliderBackGroundHealthBoss.value = currentHealth;

        if (!PlayerPrefs.HasKey("UpgradeCritBoss"))
        {
            PlayerPrefs.SetInt("UpgradeCritBoss", 2);
        }
        Damagecrit = 1 * PlayerPrefs.GetInt("UpgradeCritBoss");
    }
    public void TapAttack()
    {
        int damage = myLevel;
        TranformAttackBoss.Instance.AddPosition();
        DamageBoss(damage, true);
        SetSliderHealthBoss();
        BossInBossRoom.Instance.SetTriggerAnimIsAttacked();
        if (AttackRight)
        {
            ImageRight.SetActive(false);
            ImageRight.SetActive(true);
            GameObject text = Instantiate(TextRight, ImageRight.transform.parent);
            if (attackCrit)
            {
                text.transform.GetComponent<TMP_Text>().text = (damage * Damagecrit).ToFormatString();
            }
            else
            {
                text.transform.GetComponent<TMP_Text>().text = damage.ToFormatString();
            }
        }
        else
        {
            ImageLeft.SetActive(false);
            ImageLeft.SetActive(true);
            GameObject text = Instantiate(TextLeft, ImageLeft.transform.parent);
            if (attackCrit)
            {
                text.transform.GetComponent<TMP_Text>().text = (damage * Damagecrit).ToFormatString();
            }
            else
            {
                text.transform.GetComponent<TMP_Text>().text = damage.ToFormatString();
            }
        }
        AttackRight = !AttackRight;
        if (currentHealth <= 0 && SliderTime.value > 0 && !timeoutChecked)
        {
            TimeOut();
        }
    }
    private void Update()
    {
        if (valueBackGroungHealthBoss > currentHealth)
        {
            valueBackGroungHealthBoss -= 10;
        }
        SliderBackGroundHealthBoss.value = valueBackGroungHealthBoss;
        if (SliderTime.value <= 0 && currentHealth > 0 && !timeoutChecked)
        {
            TimeOut();
        }
    }
    void SetSliderStart()
    {
        GetData();
        SetSliderDefault();
        SetSliderHealthBoss();
    }
    void GetData()
    {
        DataBoss = BossRoom.Instance.GetDataBoss();
        currentHealth = DataBoss.healthCurrent;
        maxHealth = DataBoss.healthMax;
        valueBackGroungHealthBoss = currentHealth;
    }
    public int GetCurrentHealthBoss()
    {
        return currentHealth;
    }
    public int GetMaxHealthBoss()
    {
        return maxHealth;
    }
    void SetSliderDefault()
    {
        SliderHealthBoss.maxValue = maxHealth;
        SliderBackGroundHealthBoss.maxValue = maxHealth;
    }
    void SetSliderHealthBoss()
    {
        SliderHealthBoss.value = currentHealth;
    }
    void DamageBoss(int damage, bool randomCrit)
    {
        if (randomCrit)
        {
            if (Random.Range(0, 10) <= 4)
            {
                currentHealth -= damage;
                attackCrit = false;
            }
            else
            {
                //crit
                currentHealth -= damage * Damagecrit;
                attackCrit = true;
            }
        }
        else
        {
            currentHealth -= damage * Damagecrit;
            attackCrit = true;
        }
    }
    IEnumerator AttackSpecial(int damage)
    {
        yield return new WaitForSeconds(1.3f);
        float time = 0.1f;
        for (int i = 0; i <= 10; i++)
        {
            yield return new WaitForSeconds(time);
            ImageRight.SetActive(false);
            ImageRight.SetActive(true);
            SetSliderHealthBoss();
            BossInBossRoom.Instance.SetTriggerAnimIsAttacked();
            DamageBoss(damage, false);
            GameObject text = Instantiate(TextRight, ImageRight.transform.parent);
            text.transform.GetComponent<TMP_Text>().text = damage.ToFormatString();
            time += 0.02f;
            if (i == 10)
            {
                if (currentHealth <= 0)
                {
                    BossInBossRoom.Instance.OnRagdoll();
                }
                //OFfTornado
                ControllerPlayer.Instance.OffParticle(14);
                yield return new WaitForSeconds(0.3f);
                SetnimDanceCharacter();
                yield return new WaitForSeconds(1f);
                SetCanvasVictory();
            }
        }
    }
    void SpecialSkill()
    {
        StartCoroutine(AttackSpecial(myLevel));
    }
    void SetnimDanceCharacter()
    {
        //Anim Char
        ControllerPlayer.Instance.SetDance();
        //ChangeCam
        ControllerPlayer.Instance.ChangeCam("CamStart");
    }
    void SetCanvasVictory()
    {
        //Sau khi special skill

        //CanvasVictory
        CanvasResultBossRoom.SetActive(true);


        //OffThisCanvas
        this.gameObject.SetActive(false);
    }
    public void TimeOut()
    {
        //anim
        ControllerPlayer.Instance.SetTriggerAnim("KnockOutFull");
        //xoa list transform
        TranformAttackBoss.Instance.OffCompenent();
        //change cam
        ControllerPlayer.Instance.ChangeCam("CamAttackBossRoom1");
        //tat Tap Attack
        ButtonAttack.SetActive(false);
        //attack10lan-SpecialSkill
        SpecialSkill();
        timeoutChecked = true;
    }
    public void SetData()
    {
        DataBoss.healthCurrent = currentHealth;
        DataBoss.fighted = true;
        if (currentHealth <= 0)
        {
            statusBoss = true;
        }
        DataBoss.dead = statusBoss;
    }
}
