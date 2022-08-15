using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoss : MonoBehaviour
{
    public GameObject[] ArrayBossModel;
    public int IntBossToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("IntBossToSpawn"))
        {
            if (PlayerPrefs.GetInt("IntBossToSpawn") >= ArrayBossModel.Length)
            {
                PlayerPrefs.SetInt("IntBossToSpawn", 0);
                IntBossToSpawn = PlayerPrefs.GetInt("IntBossToSpawn");
            }
            else
            {
                IntBossToSpawn = PlayerPrefs.GetInt("IntBossToSpawn");
            }
        }
        else
        {
            IntBossToSpawn = 0;
            PlayerPrefs.SetInt("IntBossToSpawn", 0);
        }
        ArrayBossModel[IntBossToSpawn].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
