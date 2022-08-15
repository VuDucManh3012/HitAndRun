using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public float Height=1f;
    private float Ystart ,YtoMove;
    private Vector3 myPosition;
    private bool Up;
    // Start is called before the first frame update
    void Start()
    {
        Up = true;
        Ystart = transform.position.y;
        YtoMove = transform.position.y + Height;
    }

    // Update is called once per frame
    void Update()
    {
        if (Up)
        {
            myPosition = transform.position;
            myPosition.y += 0.01f;
            transform.position = myPosition;
            if (transform.position.y >= YtoMove)
            {
                Up = false;
            }
        }
        else
        {
            myPosition = transform.position;
            myPosition.y -= 0.01f;
            transform.position = myPosition;
            if (transform.position.y <= Ystart)
            {
                Up = true;
            }
        }

    }
}
