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
    public bool EnemyEnding;
    private GameObject upperLevel, lowerLevel;
    public bool Enemy1, Enemy2, Enemy3;

    // Start is called before the first frame update
    void Start()
    {
        if (EnemyEnding)
        {
            levelBonus = 1;
            SeBonus = (float)(level / 100);
            unDamage = true;
        }
        levelText.SetActive(false);
        upperLevel = transform.Find("UpperLevel").gameObject;
        lowerLevel = transform.Find("LowerLevel").gameObject;
        if (Enemy1)
        {
            Animator myanim = GetComponentInChildren<Animator>();
            myanim.SetInteger("AnimRandom", Random.RandomRange(1, 3));
            if (EnemyEnding)
            {
                levelText.SetActive(true);
                levelText.GetComponent<TextMeshPro>().text = "LV " + level.ToString();
                upperLevel.SetActive(true);
            }
            else
            {
                upperLevel.SetActive(false);
                lowerLevel.SetActive(false);
            }
        }
        else if (Enemy2)
        {
            levelText.SetActive(true);
            levelText.GetComponent<TextMeshPro>().text = "LV " + level.ToString();
            if (EnemyEnding)
            {
                upperLevel.SetActive(true);
                lowerLevel.SetActive(false);
            }
            else
            {
                upperLevel.SetActive(false);
                lowerLevel.SetActive(true);
            }
        }
        else if (Enemy3)
        {
            levelText.SetActive(true);
            levelText.GetComponent<TextMeshPro>().text = "LV " + level.ToString();
            upperLevel.SetActive(true);
            lowerLevel.SetActive(false);
        }
    }
    public void ChangeStatusLevelUpper()
    {
        if (Enemy1 && EnemyEnding)
        {
            upperLevel.SetActive(true);
            lowerLevel.SetActive(false);
            Animator myanim = GetComponentInChildren<Animator>();
            myanim.SetBool("LevelUpper", false);
        }
        else if (Enemy2 && !EnemyEnding)
        {
            Animator myanim = GetComponent<Animator>();
            myanim.SetBool("LevelUpper", false);
        }
        else if (Enemy2 && EnemyEnding)
        {
            upperLevel.SetActive(true);
            lowerLevel.SetActive(false);
            Animator myanim = GetComponent<Animator>();
            myanim.SetBool("LevelUpper", false);
        }
        else if (Enemy3)
        {
            upperLevel.SetActive(true);
            lowerLevel.SetActive(false);
            Animator myanim = GetComponent<Animator>();
            myanim.SetBool("LevelUpper", false);
        }
    }
    public void ChangeStatusLevelLower()
    {
        if (Enemy1 && EnemyEnding)
        {
            upperLevel.SetActive(false);
            lowerLevel.SetActive(true);
            try
            {
                Animator myanim = GetComponentInChildren<Animator>();
                myanim.SetBool("LevelUpper", true);
            }
            catch
            {

            }

        }
        else if (Enemy1 && !EnemyEnding)
        {
            upperLevel.SetActive(false);
            lowerLevel.SetActive(false);
        }
        else if (Enemy2 && EnemyEnding)
        {
            upperLevel.SetActive(false);
            lowerLevel.SetActive(true);
            Animator myanim = GetComponent<Animator>();
            myanim.SetBool("LevelUpper", true);
        }
        else if (Enemy2 && !EnemyEnding)
        {
            upperLevel.SetActive(false);
            lowerLevel.SetActive(true);
        }
        else if (Enemy3)
        {
            upperLevel.SetActive(false);
            lowerLevel.SetActive(true);
            Animator myanim = GetComponent<Animator>();
            myanim.SetBool("LevelUpper", true);
        }
    }
}
