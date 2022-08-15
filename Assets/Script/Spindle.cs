using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spindle : MonoBehaviour
{
    public bool SpinX, SpinY, SpinZ;
    public float VeclocitySpin;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (SpinX)
        {
            transform.RotateAround(transform.position, new Vector3(1f, 0f, 0f), VeclocitySpin * Time.deltaTime);
        }
        else if (SpinY)
        {
            transform.RotateAround(transform.position, new Vector3(0f, 1f, 0f), VeclocitySpin * Time.deltaTime);
        }
        else if (SpinZ)
        {
            transform.RotateAround(transform.position, new Vector3(0f, 0f, 1f), VeclocitySpin * Time.deltaTime);
        }

    }
}
