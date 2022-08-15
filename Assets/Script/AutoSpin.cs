using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSpin : MonoBehaviour
{
    public GameObject Character;
    public bool autoSpin;
    private float gocxoay=0;
    public float deltagocxoay = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        gocxoay = 180;
    }

    // Update is called once per frame
    void Update()
    {
        if (autoSpin)
        {
            Spin();
        }
    }
    public void Spin()
    {
        gocxoay += deltagocxoay;
        Character.transform.localEulerAngles = new Vector3(0, gocxoay, 0);
        if(gocxoay >= 540)
        {
            gocxoay = 180;
        }
    }
}
