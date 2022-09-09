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
        try
        {
            upperLevel = transform.Find("UpperLevel").gameObject;
        }
        catch
        {

        }
        try
        {
            lowerLevel = transform.Find("LowerLevel").gameObject;
        }
        catch
        {

        }
        if (Enemy1)
        {
            Animator myanim = GetComponentInChildren<Animator>();
            myanim.SetInteger("AnimRandom", Random.Range(1, 3));
            if (EnemyEnding)
            {
                levelText.SetActive(true);
                levelText.GetComponent<TextMeshPro>().text = "LV " + level.ToString();
                SetUpperLower(false, true);
            }
            else
            {
                SetUpperLower(false, false);
            }
        }
        else if (Enemy2)
        {
            levelText.SetActive(true);
            levelText.GetComponent<TextMeshPro>().text = "LV " + level.ToString();
            if (EnemyEnding)
            {
                SetUpperLower(false, true);
            }
            else
            {
                SetUpperLower(true, false);
            }
        }
        else if (Enemy3)
        {
            levelText.SetActive(true);
            levelText.GetComponent<TextMeshPro>().text = "LV " + level.ToString();
            SetUpperLower(false, true);
        }
    }
    public void ChangeStatusLevelUpper()
    {
        if (Enemy1 && EnemyEnding)
        {
            SetUpperLower(false, true);
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
            SetUpperLower(false, true);
            Animator myanim = GetComponent<Animator>();
            myanim.SetBool("LevelUpper", false);
        }
        else if (Enemy3)
        {
            SetUpperLower(false, true);
            Animator myanim = GetComponent<Animator>();
            myanim.SetBool("LevelUpper", false);
        }
    }
    public void ChangeStatusLevelLower()
    {
        if (Enemy1 && EnemyEnding)
        {
            SetUpperLower(true, false);
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
            SetUpperLower(false, false);
        }
        else if (Enemy2 && EnemyEnding)
        {
            SetUpperLower(true, false);
            Animator myanim = GetComponent<Animator>();
            myanim.SetBool("LevelUpper", true);
        }
        else if (Enemy2 && !EnemyEnding)
        {
            SetUpperLower(true, false);
        }
        else if (Enemy3)
        {
            SetUpperLower(true, false);
            Animator myanim = GetComponent<Animator>();
            myanim.SetBool("LevelUpper", true);
        }
    }
    public void SetUpperLower(bool Lower, bool Upper)
    {

        try
        {

            lowerLevel.SetActive(Lower);
        }
        catch
        {

        }
        try
        {
            upperLevel.SetActive(Upper);
        }
        catch
        {
        }
    }

}
