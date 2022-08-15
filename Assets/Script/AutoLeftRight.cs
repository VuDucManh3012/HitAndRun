using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoLeftRight : MonoBehaviour
{
    public float left, right;

    public float Speed;

    private bool moveRight =true;

    public bool onAutoSpin;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(moveRight)
        {
            transform.position = new Vector3(transform.position.x + Speed * Time.deltaTime , transform.position.y ,transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x - Speed * Time.deltaTime, transform.position.y, transform.position.z);
        }
       if(transform.position.x <= left)
        {
            moveRight = true;
        }
       else if(transform.position.x >= right)
        {
            moveRight = false;
        }
    }
}
