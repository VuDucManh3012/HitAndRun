using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMap2 : MonoBehaviour
{
    private int stage = 1;
    private GameObject StartOj, EndOj, newmap, newMechanic;
    [Header("So Luong Random")]
    [Range(0, 2)]
    public int MechanicMin;
    [Range(0, 4)]
    public int MechanicMax;

    [Header("Vi Tri Mechanic")]
    public int SoLuongMechanicCoTheTrongMap;
    public List<GameObject> ListMechanicCoTheTrongMap;
    public List<GameObject> ListMechanicPhaiCoTrongMap;
    public List<GameObject> ListMechanicFound;
    public List<int> ViTriMechanic;
    public List<GameObject> ViTriMechanicSauSort;

    [Header("Cac Loai Mechanic")]
    public List<GameObject> TuongChan;
    public List<GameObject> Lava;
    public List<GameObject> CuaChan;
    public List<GameObject> Coc;
    public List<GameObject> Dam;
    public List<GameObject> TruGai;
    public List<GameObject> Cua;
    public List<GameObject> ConLac;
    public List<GameObject> CuaMove;
    public List<GameObject> ConQuay;
    public List<GameObject> TruGai4;
    public List<GameObject> Bua;
    public List<GameObject> DuongSut;
    public List<GameObject> Cau;
    public List<GameObject> Tuong;
    public List<GameObject> DuongCong;


    [Header("Phan Loai Mechanic")]
    public List<GameObject> Level1;
    public List<GameObject> Level2;
    public List<GameObject> Level3;
    public List<GameObject> Level4;
    public List<GameObject> Level5;

    [Header("List To Spawn")]
    public List<GameObject> Level1ToSpawn;
    public List<GameObject> Level2ToSpawn;
    public List<GameObject> Level3ToSpawn;
    public List<GameObject> Level4ToSpawn;
    public List<GameObject> Level5ToSpawn;

    [Header("List History")]
    public List<GameObject> Level1History;
    public List<GameObject> Level2History;
    public List<GameObject> Level3History;
    public List<GameObject> Level4History;
    public List<GameObject> Level5History;

    [Header("So Luong Map Spawn")]
    public int[] MapLevel1;
    public int[] MapLevel2;
    public int[] MapLevel3;
    public int[] MapLevel4;
    public int[] MapLevel5;

    [Header("StageEnding")]
    public List<GameObject> stageEnding;

    [Header("MapHaveHole")]
    public List<GameObject> MapHole;

    [Header("ToSpawn")]
    public List<GameObject> ListMapFound;

    [Header("RandomKey")]
    public List<GameObject> ListMap;
    public List<GameObject> ListDiamond;
    public GameObject Key;

    [Header("TotalLevel")]
    public List<GameObject> ListMapTotalLevel;
    public List<GameObject> ListEnemyInTotalLevel;
    public double ToTalLevelInMap;
    // Start is called before the first frame update
    public void Start()
    {

        if (PlayerPrefs.HasKey("stage"))
        {
            stage = System.Int32.Parse(PlayerPrefs.GetString("stage"));
        }
        else
        {
            stage = 1;
        }
        checkStageToSpawn();
        TotalLevelInMap();
        RandomKey();
        transform.GetComponent<EnemyCheckLevel>().Start();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Start is called before the first frame update
    public void RandomKey()
    {
        //add diamond list
        for (int i = 1; i < transform.childCount; i++)
        {
            ListMap.Add(transform.GetChild(i).gameObject);
        }
        for (int i = 1; i < ListMap.Count; i++)
        {
            for (int i2 = 0; i2 < ListMap[i].transform.childCount; i2++)
            {
                if (ListMap[i].transform.GetChild(i2).tag == "Diamond")
                {
                    ListDiamond.Add(ListMap[i].transform.GetChild(i2).gameObject);
                }
                else if (ListMap[i].transform.GetChild(i2).tag == "GroupDiamond")
                {
                    for (int i3 = 0; i3 < ListMap[i].transform.GetChild(i2).childCount; i3++)
                    {
                        ListDiamond.Add(ListMap[i].transform.GetChild(i2).GetChild(i3).gameObject);
                    }
                }
            }
        }

        //random key
        //chon diamond de thay
        if (Random.RandomRange(0, 2) == 1)
        {
            if (ListDiamond.Count > 10)
            {
                int index = Random.RandomRange(ListDiamond.Count / 2, ListDiamond.Count);
                //
                Vector3 myPosition = ListDiamond[index].transform.position;
                ListDiamond[index].SetActive(false);
                Instantiate(Key, myPosition, Quaternion.Euler(-90, 0, 0));
            }
        }
    }
    public void checkStageToSpawn()
    {
        if (PlayerPrefs.HasKey("StageBefore"))
        {
            if (PlayerPrefs.GetInt("StageBefore") == stage)
            {
                SetListMapFound();
                SpawnFromListMapFound();
            }
            else
            {
                CreatListToSpawn();
                SpawnFromListMapFound();
            }
        }
        else
        {
            CreatListToSpawn();
            SpawnFromListMapFound();
        }

    }
    public void CreatListBeforeCreatListToSpawn()
    {
        foreach (GameObject item in Level1)
        {
            Level1ToSpawn.Add(item);
        }
        foreach (GameObject item in Level2)
        {
            Level2ToSpawn.Add(item);
        }
        foreach (GameObject item in Level3)
        {
            Level3ToSpawn.Add(item);
        }
        foreach (GameObject item in Level4)
        {
            Level4ToSpawn.Add(item);
        }
        foreach (GameObject item in Level5)
        {
            Level5ToSpawn.Add(item);
        }
        //them history xoa tospawn
        if (PlayerPrefs.HasKey("Level1History 0"))
        {
            for (int i = 0; i < PlayerPrefs.GetInt("Level1HistoryLength"); i++)
            {
                if (!Level1History.Contains(Level1[PlayerPrefs.GetInt("Level1History " + i)]))
                {
                    Level1History.Add(Level1[PlayerPrefs.GetInt("Level1History " + i)]);
                    Level1ToSpawn.Remove(Level1[PlayerPrefs.GetInt("Level1History " + i)]);
                }
                else
                {
                    break;
                }

            }
        }
        if (PlayerPrefs.HasKey("Level2History 0"))
        {
            for (int i = 0; i < PlayerPrefs.GetInt("Level2HistoryLength"); i++)
            {
                if (!Level2History.Contains(Level2[PlayerPrefs.GetInt("Level2History " + i)]))
                {
                    Level2History.Add(Level2[PlayerPrefs.GetInt("Level2History " + i)]);
                    Level2ToSpawn.Remove(Level2[PlayerPrefs.GetInt("Level2History " + i)]);
                }
                else
                {
                    break;
                }

            }
        }
        if (PlayerPrefs.HasKey("Level3History 0"))
        {
            for (int i = 0; i < PlayerPrefs.GetInt("Level3HistoryLength"); i++)
            {
                if (!Level3History.Contains(Level3[PlayerPrefs.GetInt("Level3History " + i)]))
                {
                    Level3History.Add(Level3[PlayerPrefs.GetInt("Level3History " + i)]);
                    Level3ToSpawn.Remove(Level3[PlayerPrefs.GetInt("Level3History " + i)]);
                }
                else
                {
                    break;
                }

            }
        }
        if (PlayerPrefs.HasKey("Level4History 0"))
        {
            for (int i = 0; i < PlayerPrefs.GetInt("Level4HistoryLength"); i++)
            {
                if (!Level4History.Contains(Level4[PlayerPrefs.GetInt("Level4History " + i)]))
                {
                    Level4History.Add(Level4[PlayerPrefs.GetInt("Level4History " + i)]);
                    Level4ToSpawn.Remove(Level4[PlayerPrefs.GetInt("Level4History " + i)]);
                }
                else
                {
                    break;
                }

            }
        }
        if (PlayerPrefs.HasKey("Level5History 0"))
        {
            for (int i = 0; i < PlayerPrefs.GetInt("Level5HistoryLength"); i++)
            {
                if (!Level5History.Contains(Level5[PlayerPrefs.GetInt("Level5History " + i)]))
                {
                    Level5History.Add(Level5[PlayerPrefs.GetInt("Level5History " + i)]);
                    Level5ToSpawn.Remove(Level5[PlayerPrefs.GetInt("Level5History " + i)]);
                }
                else
                {
                    break;
                }

            }
        }

        AddMechanicInList();
    }
    public void AddMechanicInList()
    {
        //Set List Mechanic CoTheCo , PhaiCo trong map
        ListMechanicCoTheTrongMap.Clear();
        //Random so luong mechanic trong map
        SoLuongMechanicCoTheTrongMap = 2;
        for (int i = 1; i <= stage; i++)
        {
            ListMechanicPhaiCoTrongMap.Clear();
            switch (i)
            {
                case 1:
                    ListMechanicPhaiCoTrongMap.Add(TuongChan[0]);
                    break;
                case 2:
                    ListMechanicPhaiCoTrongMap.Add(Cau[0]);
                    ListMechanicCoTheTrongMap.AddRange(TuongChan);
                    break;
                case 3:
                    ListMechanicPhaiCoTrongMap.Add(Lava[0]);
                    ListMechanicCoTheTrongMap.AddRange(Cau);
                    break;
                case 4:
                    ListMechanicPhaiCoTrongMap.Add(Tuong[0]);
                    ListMechanicCoTheTrongMap.AddRange(Lava);
                    break;
                case 5:
                    ListMechanicPhaiCoTrongMap.Add(Cua[0]);
                    ListMechanicCoTheTrongMap.AddRange(Tuong);
                    break;
                case 6:
                    ListMechanicPhaiCoTrongMap.Add(CuaChan[0]);
                    ListMechanicCoTheTrongMap.AddRange(Cua);
                    break;
                case 7:
                    ListMechanicPhaiCoTrongMap.Add(DuongCong[0]);
                    ListMechanicCoTheTrongMap.AddRange(CuaChan);
                    break;
                case 8:
                    ListMechanicPhaiCoTrongMap.Add(Coc[0]);
                    ListMechanicCoTheTrongMap.AddRange(DuongCong);
                    break;
                case 9:
                    ListMechanicPhaiCoTrongMap.Add(Dam[0]);
                    ListMechanicCoTheTrongMap.AddRange(Coc);
                    break;
                case 10:
                    ListMechanicPhaiCoTrongMap.Add(DuongSut[0]);
                    ListMechanicCoTheTrongMap.AddRange(Dam);
                    break;
                case 11:
                    ListMechanicPhaiCoTrongMap.Add(TruGai[0]);
                    ListMechanicCoTheTrongMap.AddRange(TruGai);
                    break;
                case 12:
                    ListMechanicPhaiCoTrongMap.Add(ConLac[0]);
                    ListMechanicCoTheTrongMap.AddRange(TruGai);
                    break;
                case 13:
                    ListMechanicPhaiCoTrongMap.Add(CuaMove[0]);
                    ListMechanicCoTheTrongMap.AddRange(ConLac);
                    break;
                case 14:
                    ListMechanicPhaiCoTrongMap.Add(ConQuay[0]);
                    ListMechanicCoTheTrongMap.AddRange(CuaMove);
                    break;
                case 15:
                    ListMechanicPhaiCoTrongMap.Add(TruGai4[0]);
                    ListMechanicCoTheTrongMap.AddRange(ConQuay);
                    break;
                case 16:
                    ListMechanicPhaiCoTrongMap.Add(Bua[0]);
                    ListMechanicCoTheTrongMap.AddRange(TruGai4);
                    break;
                case 17:
                    ListMechanicCoTheTrongMap.AddRange(Bua);
                    break;
                default:
                    SoLuongMechanicCoTheTrongMap = 3;
                    break;
            }
        }
        //
        //Set Thu Tu Xuat Hien Mechanic
        if (ListMechanicCoTheTrongMap.Count != 0)
        {
            for (int i = 0; i < SoLuongMechanicCoTheTrongMap; i++)
            {
                ListMechanicFound.Add(ListMechanicCoTheTrongMap[Random.Range(0, ListMechanicCoTheTrongMap.Count)]);
            }
        }
        if (ListMechanicPhaiCoTrongMap.Count != 0)
        {
            ListMechanicFound.InsertRange(Random.Range(0, ListMechanicFound.Count), ListMechanicPhaiCoTrongMap);
        }

        foreach (GameObject item in ListMechanicFound)
        {
            int random = Random.Range(1, 6);
            ViTriMechanic.Add(random);
        }
        for (int i = 0; i < ViTriMechanic.Count; i++)
        {
            if (ViTriMechanic[i] > ViTriMechanicSauSort.Count)
            {
                ViTriMechanicSauSort.Add(ListMechanicFound[i]);
            }
            else
            {
                ViTriMechanicSauSort.Insert(ViTriMechanic[i] - 1, ListMechanicFound[i]);
            }
        }
    }
    public void CreatListToSpawn()
    {
        CreatListBeforeCreatListToSpawn();
        int random;
        int a, b, c, d, e;
        GameObject mapfound;
        if (stage < 35)
        {
            stage = stage / 7;
        }
        else
        {
            stage = stage % 35 / 7;
        }
        switch (stage)
        {
            case 0:
                //2(1),2(2),1(3)
                a = MapLevel1[0];
                b = MapLevel1[1];
                c = MapLevel1[2];
                d = MapLevel1[3];
                e = MapLevel1[4];
                break;
            case 1:
                //2(2),2(3),1(4)
                a = MapLevel2[0];
                b = MapLevel2[1];
                c = MapLevel2[2];
                d = MapLevel2[3];
                e = MapLevel2[4];
                break;
            case 2:
                //2(3),2(4),1(5)
                a = MapLevel3[0];
                b = MapLevel3[1];
                c = MapLevel3[2];
                d = MapLevel3[3];
                e = MapLevel3[4];
                break;
            case 3:
                //2(3),2(4),1(5)
                a = MapLevel4[0];
                b = MapLevel4[1];
                c = MapLevel4[2];
                d = MapLevel4[3];
                e = MapLevel4[4];
                break;
            case 4:
                //1(3),2(4),2(5)
                a = MapLevel5[0];
                b = MapLevel5[1];
                c = MapLevel5[2];
                d = MapLevel5[3];
                e = MapLevel5[4];
                break;
            default:
                a = MapLevel1[0];
                b = MapLevel1[1];
                c = MapLevel1[2];
                d = MapLevel1[3];
                e = MapLevel1[4];
                break;
        }
        //Chon map trong map phan loai
        if (a != 0)
        {
            for (int i = 0; i < a; i++)
            {
                ///khi LevelToSpawn =0 -> Them LevelHistory vao LevelToSpawn va xoa key trong PlayerPref
                if (Level1ToSpawn.Count <= a)
                {
                    Level1ToSpawn.AddRange(Level1History);
                    Level1History.Clear();
                    for (int i2 = 0; i2 <= Level1History.Count; i2++)
                    {
                        PlayerPrefs.DeleteKey("Level1History " + i2);
                    }
                }
                random = Random.Range(0, Level1ToSpawn.Count);
                mapfound = Level1ToSpawn[random];
                ListMapFound.Add(mapfound);
                Level1ToSpawn.Remove(mapfound);
                Level1History.Add(mapfound);
                PlayerPrefs.SetInt("Level1History " + (Level1History.Count - 1), Level1.IndexOf(mapfound));
            }
            PlayerPrefs.SetInt("Level1HistoryLength", Level1History.Count);
        }
        if (b != 0)
        {
            for (int i = 0; i < b; i++)
            {
                ///khi LevelToSpawn =0 -> Them LevelHistory vao LevelToSpawn va xoa key trong PlayerPref
                if (Level2ToSpawn.Count <= b)
                {
                    Level2ToSpawn.AddRange(Level2History);
                    Level2History.Clear();
                    for (int i2 = 0; i2 <= Level2History.Count; i2++)
                    {
                        PlayerPrefs.DeleteKey("Level2History " + i2);
                    }
                }
                random = Random.Range(0, Level2ToSpawn.Count);
                mapfound = Level2ToSpawn[random];
                ListMapFound.Add(mapfound);
                Level2ToSpawn.Remove(mapfound);
                Level2History.Add(mapfound);
                PlayerPrefs.SetInt("Level2History " + (Level2History.Count - 1), Level2.IndexOf(mapfound));
            }
            PlayerPrefs.SetInt("Level2HistoryLength", Level2History.Count);
        }
        if (c != 0)
        {
            for (int i = 0; i < c; i++)
            {
                ///khi LevelToSpawn =0 -> Them LevelHistory vao LevelToSpawn va xoa key trong PlayerPref
                if (Level3ToSpawn.Count <= c)
                {
                    Level3ToSpawn.AddRange(Level3History);
                    Level3History.Clear();
                    for (int i2 = 0; i2 <= Level3History.Count; i2++)
                    {
                        PlayerPrefs.DeleteKey("Level3History " + i2);
                    }
                }
                random = Random.Range(0, Level3ToSpawn.Count);
                mapfound = Level3ToSpawn[random];
                ListMapFound.Add(mapfound);
                Level3ToSpawn.Remove(mapfound);
                Level3History.Add(mapfound);
                PlayerPrefs.SetInt("Level3History " + (Level3History.Count - 1), Level3.IndexOf(mapfound));
            }
            PlayerPrefs.SetInt("Level3HistoryLength", Level3History.Count);
        }
        if (d != 0)
        {
            for (int i = 0; i < d; i++)
            {
                ///khi LevelToSpawn =0 -> Them LevelHistory vao LevelToSpawn va xoa key trong PlayerPref
                if (Level4ToSpawn.Count <= d)
                {
                    Level4ToSpawn.AddRange(Level4History);
                    Level4History.Clear();
                    for (int i2 = 0; i2 <= Level4History.Count; i2++)
                    {
                        PlayerPrefs.DeleteKey("Level4History " + i2);
                    }
                }
                random = Random.Range(0, Level4ToSpawn.Count);
                mapfound = Level4ToSpawn[random];
                ListMapFound.Add(mapfound);
                Level4ToSpawn.Remove(mapfound);
                Level4History.Add(mapfound);
                PlayerPrefs.SetInt("Level4History " + (Level4History.Count - 1), Level4.IndexOf(mapfound));
            }
            PlayerPrefs.SetInt("Level4HistoryLength", Level4History.Count);
        }
        if (e != 0)
        {
            for (int i = 0; i < e; i++)
            {
                ///khi LevelToSpawn =0 -> Them LevelHistory vao LevelToSpawn va xoa key trong PlayerPref
                if (Level5ToSpawn.Count <= e)
                {
                    Level5ToSpawn.AddRange(Level5History);
                    Level5History.Clear();
                    for (int i2 = 0; i2 <= Level5History.Count; i2++)
                    {
                        PlayerPrefs.DeleteKey("Level5History " + i2);
                    }
                }
                random = Random.Range(0, Level5ToSpawn.Count);
                mapfound = Level5ToSpawn[random];
                ListMapFound.Add(mapfound);
                Level5ToSpawn.Remove(mapfound);
                Level5History.Add(mapfound);
                PlayerPrefs.SetInt("Level5History " + (Level5History.Count - 1), Level5.IndexOf(mapfound));
            }
            PlayerPrefs.SetInt("Level5HistoryLength", Level5History.Count);
        }
        //////////////////////////////
        ///

        stage = System.Int32.Parse(PlayerPrefs.GetString("stage"));

        //add them hole
        random = Random.Range(0, 3);
        if (random == 1)
        {
            Debug.LogWarning("Co the Thang");
            if (ToTalLevelInMap > 400)
            {
                random = Random.Range(0, MapHole.Count);
                ListMapFound.Add(MapHole[random]);
            }
            else if (ToTalLevelInMap > 300)
            {
                random = Random.Range(0, MapHole.Count - 3);
                ListMapFound.Add(MapHole[random]);
            }
            else if (ToTalLevelInMap > 200)
            {
                random = Random.Range(0, MapHole.Count - 6);
                ListMapFound.Add(MapHole[random]);
            }
            else if (ToTalLevelInMap > 150)
            {
                random = Random.Range(0, MapHole.Count - 7);
                ListMapFound.Add(MapHole[random]);
            }
            else if (ToTalLevelInMap > 100)
            {
                random = Random.Range(0, MapHole.Count - 8);
                ListMapFound.Add(MapHole[random]);
            }
            else
            {
                random = Random.Range(0, MapHole.Count - 9);
                ListMapFound.Add(MapHole[random]);
            }
        }
        else
        {
            Debug.LogWarning("Random");
            random = Random.Range(0, MapHole.Count);
            ListMapFound.Add(MapHole[random]);
        }
        ///////////////////////////////////////////////
        //add them ending
        random = Random.Range(0, stageEnding.Count);
        ListMapFound.Add(stageEnding[random]);
        /////////////////////////////
        ///
        /// 
        /// Luu lai map found   

        for (int i = 0; i < ListMapFound.Count; i++)
        {
            PlayerPrefs.SetString("ListMapFound " + i, ListMapFound[i].name);
            PlayerPrefs.SetInt("ListMapFoundBeforeLength", ListMapFound.Count);
        }
        ///Luu lai stage before
        PlayerPrefs.SetInt("StageBefore", stage);
    }
    void TotalLevelInMap()
    {
        ToTalLevelInMap = 0;
        for (int i = 1; i < transform.childCount - 2; i++)
        {
            ListMapTotalLevel.Add(transform.GetChild(i).gameObject);
        }

        for (int i = 1; i < ListMapTotalLevel.Count; i++)
        {
            for (int i2 = 0; i2 < ListMapTotalLevel[i].transform.childCount; i2++)
            {
                if (ListMapTotalLevel[i].transform.GetChild(i2).tag == "Enemy")
                {
                    ListEnemyInTotalLevel.Add(ListMapTotalLevel[i].transform.GetChild(i2).gameObject);
                    ToTalLevelInMap += ListMapTotalLevel[i].transform.GetChild(i2).gameObject.GetComponent<Enemy>().level;
                }
                else if (ListMapTotalLevel[i].transform.GetChild(i2).tag == "Enemy3" && ListMapTotalLevel[i].transform.GetChild(i2).transform.GetComponent<Enemy>().levelBonus >= 40)
                {
                    ListEnemyInTotalLevel.Add(ListMapTotalLevel[i].transform.GetChild(i2).gameObject);
                    ToTalLevelInMap += ListMapTotalLevel[i].transform.GetChild(i2).gameObject.GetComponent<Enemy>().levelBonus;
                }
                else if (ListMapTotalLevel[i].transform.GetChild(i2).tag == "GroupEnemy")
                {
                    for (int i3 = 0; i3 < ListMapTotalLevel[i].transform.GetChild(i2).childCount; i3++)
                    {
                        ListEnemyInTotalLevel.Add(ListMapTotalLevel[i].transform.GetChild(i2).GetChild(i3).gameObject);
                        ToTalLevelInMap += ListMapTotalLevel[i].transform.GetChild(i2).GetChild(i3).gameObject.GetComponent<Enemy>().levelBonus;
                    }
                }
            }
        }
    }
    public void SpawnFromListMapFound()
    {
        for (int i = 0; i <= ListMapFound.Count - 1; i++)
        {
            //spawn Mechanic
            try
            {
                newMechanic = Instantiate(ViTriMechanicSauSort[i], transform);
                if (i == 0)
                {
                    Transform startSpawn = this.transform.Find("StartSpawn").transform;

                    newMechanic.transform.position = startSpawn.position;

                    StartOj = newMechanic.transform.Find("Start").gameObject;

                    newMechanic.transform.position = transform.position + (newMechanic.transform.position - StartOj.transform.position);

                    EndOj = newMechanic.transform.Find("End").gameObject;

                }
                else
                {
                    newMechanic.transform.position = EndOj.transform.position;

                    newMechanic.transform.position = newMechanic.transform.position + (newMechanic.transform.position - newMechanic.transform.Find("Start").transform.position);

                    EndOj = newMechanic.transform.Find("End").gameObject;
                }
            }
            catch
            {

            }

            //
            //spawn Map
            newmap = Instantiate(ListMapFound[i], transform);

            newmap.transform.position = EndOj.transform.position;

            newmap.transform.position = newmap.transform.position + (newmap.transform.position - newmap.transform.Find("Start").transform.position);

            EndOj = newmap.transform.Find("End").gameObject;

            //

        }
    }
    public void SetListMapFound()
    {
        AddMechanicInList();
        int a, b, c, d, e;
        int index = 0;
        string nameMapFound = PlayerPrefs.GetString("ListMapFound " + index);
        if (stage < 35)
        {
            stage = stage / 7;
        }
        else
        {
            stage = stage % 35 / 7;
        }
        switch (stage)
        {
            case 0:
                //2(1),2(2),1(3)
                a = MapLevel1[0];
                b = MapLevel1[1];
                c = MapLevel1[2];
                d = MapLevel1[3];
                e = MapLevel1[4];
                break;
            case 1:
                //2(2),2(3),1(4)
                a = MapLevel2[0];
                b = MapLevel2[1];
                c = MapLevel2[2];
                d = MapLevel2[3];
                e = MapLevel2[4];
                break;
            case 2:
                //2(3),2(4),1(5)
                a = MapLevel3[0];
                b = MapLevel3[1];
                c = MapLevel3[2];
                d = MapLevel3[3];
                e = MapLevel3[4];
                break;
            case 3:
                //2(3),2(4),1(5)
                a = MapLevel4[0];
                b = MapLevel4[1];
                c = MapLevel4[2];
                d = MapLevel4[3];
                e = MapLevel4[4];
                break;
            case 4:
                //1(3),2(4),2(5)
                a = MapLevel5[0];
                b = MapLevel5[1];
                c = MapLevel5[2];
                d = MapLevel5[3];
                e = MapLevel5[4];
                break;
            default:
                a = MapLevel1[0];
                b = MapLevel1[1];
                c = MapLevel1[2];
                d = MapLevel1[3];
                e = MapLevel1[4];
                break;
        }
        if (a != 0)
        {
            for (int i = 0; i < a; i++)
            {
                foreach (GameObject item in Level1)
                {
                    if (item.name == nameMapFound)
                    {
                        ListMapFound.Add(item);
                        break;
                    }
                }
                index++;
                nameMapFound = PlayerPrefs.GetString("ListMapFound " + index);
            }
        }
        if (b != 0)
        {
            for (int i = 0; i < b; i++)
            {
                foreach (GameObject item in Level2)
                {
                    if (item.name == nameMapFound)
                    {
                        ListMapFound.Add(item);
                        break;
                    }
                }
                index++;
                nameMapFound = PlayerPrefs.GetString("ListMapFound " + index);
            }
        }
        if (c != 0)
        {
            for (int i = 0; i < c; i++)
            {
                foreach (GameObject item in Level3)
                {
                    if (item.name == nameMapFound)
                    {
                        ListMapFound.Add(item);
                        break;
                    }
                }
                index++;
                nameMapFound = PlayerPrefs.GetString("ListMapFound " + index);
            }
        }
        if (d != 0)
        {
            for (int i = 0; i < d; i++)
            {
                foreach (GameObject item in Level4)
                {
                    if (item.name == nameMapFound)
                    {
                        ListMapFound.Add(item);
                        break;
                    }
                }
                index++;
                nameMapFound = PlayerPrefs.GetString("ListMapFound " + index);
            }
        }
        if (e != 0)
        {
            for (int i = 0; i < e; i++)
            {
                foreach (GameObject item in Level5)
                {
                    if (item.name == nameMapFound)
                    {
                        ListMapFound.Add(item);
                        break;
                    }
                }
                index++;
                nameMapFound = PlayerPrefs.GetString("ListMapFound " + index);
            }
        }
        //add them hole
        int random;
        stage = System.Int32.Parse(PlayerPrefs.GetString("stage"));
        if (Random.Range(0, 2) == 1)
        {
            random = Random.Range(0, MapHole.Count);
            ListMapFound.Add(MapHole[random]);
        }
        //add them ending
        random = Random.Range(0, stageEnding.Count);
        ListMapFound.Add(stageEnding[random]);
    }
}
