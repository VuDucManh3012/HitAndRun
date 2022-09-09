using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    public bool onpendulum;
    private float gocxoay =0;
    public float x, y;
    private bool onLeft;
    // Start is called before the first frame update
    void Start()
    {
        gocxoay = 90;
        onLeft = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (onpendulum)
        {
            OnPendulum();
        }
    }
    public void OnPendulum()
    {

        if (gocxoay <= -90 )
        {
            onLeft = false;
        }else if(gocxoay >= 90)
        {
            onLeft = true;
        }
        if (onLeft)
        {
            gocxoay -= 0.5f;
            transform.localEulerAngles = new Vector3(x, y, gocxoay);
        }
        else
        {
            gocxoay += 0.5f;
            transform.localEulerAngles = new Vector3(x, y, gocxoay);
        }
    }
}
