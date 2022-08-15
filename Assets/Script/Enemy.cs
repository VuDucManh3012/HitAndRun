using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    public double level = 0;
    public double levelBonus = 1;
    public bool unDamage = false;
    public float SeBonus = 0;
    public GameObject levelText;
    public bool onTextLevel = false;
    public bool EnemyEnding;
    private GameObject upperLevel, lowerLevel;

    // Start is called before the first frame update
    void Start()
    {
        if (EnemyEnding)
        {
            levelBonus = 1;
            SeBonus = (float)(level / 100);
        }
        levelText.SetActive(false);
        if (onTextLevel)
        {
            levelText.SetActive(true);
            levelText.GetComponent<TextMeshPro>().text = "LV " + level.ToString();
        }
        if (onTextLevel && !EnemyEnding)
        {
            upperLevel = transform.Find("UpperLevel").gameObject;
            lowerLevel = transform.Find("LowerLevel").gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ChangeStatusLevelUpper()
    {
        if (onTextLevel && !EnemyEnding)
        {
            upperLevel.SetActive(true);
            lowerLevel.SetActive(false);
            if (level >= 10)
            {
                Animator myanim = GetComponent<Animator>();
                myanim.SetBool("LevelUpper", false);
            }
        }
    }
    public void ChangeStatusLevelLower()
    {
        if (onTextLevel && !EnemyEnding)
        {
            upperLevel.SetActive(false);
            lowerLevel.SetActive(true);
            if (level >= 10)
            {
                Animator myanim = GetComponent<Animator>();
                myanim.SetBool("LevelUpper", true);
            }
        }
    }
}
