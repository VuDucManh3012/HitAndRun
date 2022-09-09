using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomKey : MonoBehaviour
{
    public List<GameObject> ListMap;
    public List<GameObject> ListDiamond;
    public GameObject Key;
    // Start is called before the first frame update
    public void Start()
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
        if (ListDiamond.Count > 10)
        {
            int index = Random.RandomRange(ListDiamond.Count / 2, ListDiamond.Count);
            //
            Transform myPosition = ListDiamond[index].transform;
            ListDiamond[index].SetActive(false);
            Instantiate(Key, myPosition);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
