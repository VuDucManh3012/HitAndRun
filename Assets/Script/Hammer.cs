using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    private float gocxoay;
    public bool onDown;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (onDown)
        {
            gocxoay -= 0.4f;
            transform.localEulerAngles = new Vector3(0, 0, gocxoay);
        }
        else
        {
            gocxoay += 0.4f;
            transform.localEulerAngles = new Vector3(0, 0, gocxoay);
        }
        if(gocxoay >= 90)
        {
            onDown = true;
        }
        if (gocxoay <= -5)
        {
            onDown = false;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Road")
        {
            onDown = false;
            Debug.Log("aaa");
        }

    }
}
