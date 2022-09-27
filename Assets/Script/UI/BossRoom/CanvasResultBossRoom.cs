using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CanvasResultBossRoom : MonoBehaviour
{
    private int maxHealthBoss;
    private int currentHealthBoss;

    public Image imageBoss;

    public Slider sliderResult;
    public List<GameObject> ListChest;

    private bool checkedChest1, checkedChest2, checkedChest3;

    public TMP_Text textDamageDone;
    public GameObject ImageComplete;
    // Start is called before the first frame update
    void Start()
    {
        currentHealthBoss = CanvasAttackBoss.Instance.GetCurrentHealthBoss();
        maxHealthBoss = CanvasAttackBoss.Instance.GetMaxHealthBoss();
        imageBoss.sprite = BossRoom.Instance.GetImageBoss();

        sliderResult.maxValue = maxHealthBoss;
        sliderResult.value = maxHealthBoss;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (sliderResult.value > currentHealthBoss)
        {
            sliderResult.value -= maxHealthBoss / 200;
            checkChest();
            float damageDone = (1f - (sliderResult.value / sliderResult.maxValue))*100f;
            textDamageDone.text = damageDone.ToString() + "% damage done";
            if (sliderResult.value == 0)
            {
                ImageComplete.SetActive(true);
            }
        }
    }
    void checkChest()
    {
        if (sliderResult.value <= sliderResult.maxValue / 4 && !checkedChest1)
        {
            ListChest[0].SetActive(true);
            ListChest[0].transform.parent.GetChild(0).gameObject.SetActive(false);
            CanvasManager.Instance.DiamondFly(100, ListChest[0].transform);
            checkedChest1 = true;
        }
        else if (sliderResult.value <= sliderResult.maxValue / 2 && !checkedChest2)
        {
            ListChest[1].SetActive(true);
            ListChest[1].transform.parent.GetChild(0).gameObject.SetActive(false);
            CanvasManager.Instance.DiamondFly(100, ListChest[1].transform);
            checkedChest2 = true;
        }
        else if (sliderResult.value <= (sliderResult.maxValue / 4) * 3 && !checkedChest3)
        {
            ListChest[2].SetActive(true);
            ListChest[2].transform.parent.GetChild(0).gameObject.SetActive(false);
            CanvasManager.Instance.DiamondFly(100, ListChest[2].transform);
            checkedChest3 = true;
        }
    }
    public void Continous()
    {
        CanvasAttackBoss.Instance.SetData();
        CanvasManager.Instance.Continues();
    }
}
