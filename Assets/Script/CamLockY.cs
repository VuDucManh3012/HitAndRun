using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamLockY : MonoBehaviour
{
    Vector3 myposition;
    float Y;
    // Start is called before the first frame update
    void Start()
    {
        Y = myposition.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = myposition;
        myposition.y = Y;
    }
}
