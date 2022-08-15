using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public Transform start;

    private void Start()
    {
        try
        {
            start = transform.parent.parent.Find("BlackHole").Find("BlackHole").transform;
        }
        catch
        {

        }

    }
    // Update is called once per frame
    void Update()
    {

    }
}
