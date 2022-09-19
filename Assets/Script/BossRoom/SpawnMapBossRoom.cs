using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMapBossRoom : MonoBehaviour
{
    public List<GameObject> MapToSpawn;

    public int QuantityMap;

    public GameObject MapBossRoom;

    [HideInInspector]
    public List<GameObject> ListMapFound;

    private GameObject startObj, endObj, newMapObj;

    // Start is called before the first frame update
    void Start()
    {
        RandomMap();
        StartSpawn();
    }
    private void RandomMap()
    {
        for (int i = 0; i <= QuantityMap; i++)
        {
            int indexMap = Random.Range(0, MapToSpawn.Count);
            ListMapFound.Add(MapToSpawn[indexMap]);
        }
    }
    private void StartSpawn()
    {
        Transform startSpawn = this.transform.Find("StartSpawn").transform;
        for (int i = 0; i < ListMapFound.Count; i++)
        {
            if (endObj != null)
            {
                startSpawn = endObj.transform;
            }
            newMapObj = Instantiate(ListMapFound[i], transform);

            newMapObj.transform.position = startSpawn.position;

            startObj = newMapObj.transform.Find("Start").gameObject;

            newMapObj.transform.position = newMapObj.transform.position + (newMapObj.transform.position - startObj.transform.position);

            endObj = newMapObj.transform.Find("End").gameObject;
        }

        newMapObj = Instantiate(MapBossRoom, transform);

        newMapObj.transform.position = endObj.transform.position;

        startObj = newMapObj.transform.Find("Start").gameObject;

        newMapObj.transform.position = newMapObj.transform.position + (newMapObj.transform.position - startObj.transform.position);
    }
}
