using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public class SpawnMap : MonoBehaviour
{
    public GameObject[] AllMap;
    public GameObject[] map1_5;
    public GameObject[] map5_10;
    public GameObject[] map10_15;
    public GameObject[] map15_20;
    public GameObject[] map20up;

    public GameObject[] mapHole;

    private GameObject mapFound;
    private GameObject[] ListMapFound;
    public GameObject[] mapJump;
    private GameObject mapFoundBefore;


    public GameObject[] stageEnding;
    private GameObject stageEndingFound;

    private GameObject StartOj, EndOj, newmap;
    private int index;
    public int stage;

    [Header("Enable Map")]
    public GameObject Character;
    private float NextTimeToCheck;
    public float DistanceTimeToCheck;
    public List<Vector3> PositionMap;
    public int SoLuongMap;

    // Start is called before the first frame update
    void Start()
    {
        stage = System.Int32.Parse(PlayerPrefs.GetString("stage"));
        checkStageToSpawn();
        NextTimeToCheck = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void checkStageToSpawn()
    {
        if (PlayerPrefs.HasKey("StageBefore"))
        {
            if (PlayerPrefs.GetInt("StageBefore") == stage)
            {
                setListMapFound();
                spawnFromListMap();
            }
            else
            {
                creatListMapFound();
                spawnFromListMap();
            }
        }
        else
        {
            creatListMapFound();
            spawnFromListMap();
        }

    }
    void setListMapFound()
    {
        if (PlayerPrefs.GetInt("ListMapFoundLength") == SoLuongMap)
        {
            ListMapFound = new GameObject[SoLuongMap];
            for (int i = 0; i <= SoLuongMap-1; i++)
            {
                string name = PlayerPrefs.GetString("MapFound " + i);
                foreach (GameObject item in AllMap)
                {
                    if (item.name == name)
                    {
                        ListMapFound[i] = item;
                        break;

                    }
                }
            }
        }
        else
        {
            ListMapFound = new GameObject[SoLuongMap+1];
            for (int i = 0; i <= SoLuongMap; i++)
            {
                string name = PlayerPrefs.GetString("MapFound " + i);
                foreach (GameObject item in AllMap)
                {
                    if (item.name == name)
                    {
                        ListMapFound[i] = item;
                        break;
                    }
                }
            }
        }

    }
    void spawnFromListMap()
    {
        for (int i = 0; i <= ListMapFound.Length - 1; i++)
        {
            newmap = Instantiate(ListMapFound[i], transform);
            if (i == 0)
            {
                Transform startSpawn = this.transform.Find("StartSpawn").transform;

                newmap.transform.position = startSpawn.position;

                StartOj = newmap.transform.Find("Start").gameObject;

                newmap.transform.position = transform.position + (newmap.transform.position - StartOj.transform.position);

                EndOj = newmap.transform.Find("End").gameObject;

            }
            else
            {
                newmap.transform.position = EndOj.transform.position;

                newmap.transform.position = newmap.transform.position + (newmap.transform.position - newmap.transform.Find("Start").transform.position);

                EndOj = newmap.transform.Find("End").gameObject;
            }
            PositionMap.Add(newmap.transform.position);
        }
        index = Random.Range(0, 3);
        stageEndingFound = stageEnding[index];
        GameObject SE = Instantiate(stageEndingFound,transform);
        StartOj = SE.transform.Find("Start").gameObject;
        SE.transform.position = SE.transform.position + (EndOj.transform.position - StartOj.transform.position);

        PositionMap.Add(SE.transform.position);
    }
    [ContextMenu("CreatList")]
    public void creatListMapFound()
    {
        if (stage == 3)
        {
            ListMapFound = new GameObject[SoLuongMap+1];
            PlayerPrefs.SetInt("ListMapFoundLength", SoLuongMap+1);
        }
        else
        {
            PlayerPrefs.SetInt("ListMapFoundLength", SoLuongMap);
            ListMapFound = new GameObject[SoLuongMap];
        }
        for (int i = 0; i <= ListMapFound.Length - 1; i++)
        {
            //
            if (i == ListMapFound.Length - 1)
            {
                if (stage == 3)
                {
                    ListMapFound[ListMapFound.Length - 1] = mapHole[0];
                    PlayerPrefs.SetString("MapFound " + i, ListMapFound[ListMapFound.Length - 1].name);
                    PlayerPrefs.SetInt("StageBefore", stage);
                    break;
                }
            }
            //
            if (stage > 20)
            {
                index = Random.Range(0, 20);
                mapFound = map20up[index];
            }
            else if (stage > 15)
            {
                index = Random.Range(0, 20);
                mapFound = map15_20[index];
            }
            else if (stage > 10)
            {
                index = Random.Range(0, 20);
                mapFound = map10_15[index];
            }
            else if (stage > 5)
            {
                index = Random.Range(0, 20);
                mapFound = map5_10[index];
            }
            else if (stage > 0)
            {
                index = Random.Range(0, 20);
                mapFound = map1_5[index];
            }
            //
            for (int u = 0; u <= mapJump.Length - 1; u++)
            {
                if (mapFound == mapJump[u])
                {
                    for (int u2 = 0; u2 <= mapJump.Length - 1; u2++)
                    {
                        if (mapFoundBefore == mapJump[u2])
                        {
                            i--;
                        }
                    }
                }
            }
            if (stage > 20)
            {
                mapFoundBefore = map20up[index];
            }
            else if (stage > 15)
            {
                mapFoundBefore = map15_20[index];
            }
            else if (stage > 10)
            {
                mapFoundBefore = map10_15[index];
            }
            else if (stage > 5)
            {
                mapFoundBefore = map5_10[index];
            }
            else if (stage > 0)
            {
                mapFoundBefore = map1_5[index];
            }
            ListMapFound[i] = mapFound;
            PlayerPrefs.SetString("MapFound " + i, mapFound.name);
            PlayerPrefs.SetInt("StageBefore", stage);

        }
    }
}
