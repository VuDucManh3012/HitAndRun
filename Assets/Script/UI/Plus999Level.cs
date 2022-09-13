using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plus999Level : MonoBehaviour
{
    public GameObject ImageADS;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("First999Level"))
        {
            ImageADS.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
