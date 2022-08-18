using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCheckLevel : MonoBehaviour
{
    public List<GameObject> ListMap;
    public List<GameObject> ListEnemyNeedCheck;
    private double LevelChar;
    // Start is called before the first frame update
    public void Start()
    {
        addListEnemyNeedCheck();
    }
    public void addListEnemyNeedCheck()
    {
        for (int i = 1; i < transform.childCount; i++)
        {
            ListMap.Add(transform.GetChild(i).gameObject);
        }

        for (int i = 1; i < ListMap.Count; i++)
        {
            for (int i2 = 0; i2 < ListMap[i].transform.childCount; i2++)
            {
                if (ListMap[i].transform.GetChild(i2).tag == "Enemy" && ListMap[i].transform.GetChild(i2).transform.GetComponent<Enemy>().level >= 10)
                {
                    ListEnemyNeedCheck.Add(ListMap[i].transform.GetChild(i2).gameObject);
                }
                else if (ListMap[i].transform.GetChild(i2).tag == "Enemy3")
                {
                    ListEnemyNeedCheck.Add(ListMap[i].transform.GetChild(i2).gameObject);
                }
                else if (ListMap[i].transform.GetChild(i2).tag == "GroupEnemy")
                {
                    for (int i3 = 0; i3 < ListMap[i].transform.GetChild(i2).childCount; i3++)
                    {
                        if (ListMap[i].transform.GetChild(i2).GetChild(i3).GetComponent<Enemy>().level >= 10)
                        {
                            ListEnemyNeedCheck.Add(ListMap[i].transform.GetChild(i2).GetChild(i3).gameObject);
                        }
                    }
                }
                else if (ListMap[i].transform.GetChild(i2).tag == "Hole")
                {
                    GameObject Hole = ListMap[i].transform.GetChild(i2).gameObject;
                    for (int i3 = 0; i3 < Hole.transform.childCount; i3++)
                    {
                        if (Hole.transform.GetChild(i3).tag == "Enemy" && Hole.transform.GetChild(i3).GetComponent<Enemy>().level >= 10)
                        {
                            ListEnemyNeedCheck.Add(Hole.transform.GetChild(i3).gameObject);
                        }
                        else if (Hole.transform.GetChild(i3).tag == "Enemy3")
                        {
                            ListEnemyNeedCheck.Add(Hole.transform.GetChild(i3).gameObject);
                        }
                        else if (Hole.transform.GetChild(i3).tag == "GroupEnemy")
                        {
                            for (int i4 = 0; i4 < Hole.transform.GetChild(i3).childCount; i4++)
                            {
                                if (Hole.transform.GetChild(i3).GetChild(i4).GetComponent<Enemy>().level >= 10)
                                {
                                    ListEnemyNeedCheck.Add(Hole.transform.GetChild(i3).GetChild(i4).gameObject);
                                }
                            }
                        }
                    }
                }
                else if (ListMap[i].transform.GetChild(i2).tag == "HoleParent")
                {
                    GameObject HoleParent = ListMap[i].transform.GetChild(i2).gameObject;
                    for (int i3 = 0; i3 < HoleParent.transform.childCount; i3++)
                    {
                        if (HoleParent.transform.GetChild(i3).tag == "Hole")
                        {
                            GameObject Hole = HoleParent.transform.GetChild(i3).gameObject;
                            for (int i4 = 0; i4 < Hole.transform.childCount; i4++)
                            {
                                if (Hole.transform.GetChild(i4).tag == "Enemy" && Hole.transform.GetChild(i4).GetComponent<Enemy>().level >= 10)
                                {
                                    ListEnemyNeedCheck.Add(Hole.transform.GetChild(i4).gameObject);
                                }
                                else if (Hole.transform.GetChild(i4).tag == "Enemy3")
                                {
                                    ListEnemyNeedCheck.Add(Hole.transform.GetChild(i4).gameObject);
                                }
                                else if (Hole.transform.GetChild(i4).tag == "GroupEnemy")
                                {
                                    for (int i5 = 0; i5 < Hole.transform.GetChild(i4).childCount; i5++)
                                    {
                                        if (Hole.transform.GetChild(i4).GetChild(i5).GetComponent<Enemy>().level >= 10)
                                        {
                                            ListEnemyNeedCheck.Add(Hole.transform.GetChild(i4).GetChild(i5).gameObject);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void checkLevelEnemy(double myLevel)
    {
        LevelChar = myLevel;
        foreach (GameObject item in ListEnemyNeedCheck)
        {
            if (item.transform.GetComponent<Enemy>().level <= LevelChar)
            {
                item.transform.GetComponent<Enemy>().ChangeStatusLevelLower();
            }
            else
            {
                item.transform.GetComponent<Enemy>().ChangeStatusLevelUpper();
            }
        }
    }
}
