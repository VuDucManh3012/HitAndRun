using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectArrow : MonoBehaviour
{
    public GameObject ModelCharacter;
    public Vector3 arrow;
    public double angle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        angle = Vector3.AngleBetween(new Vector3(ModelCharacter.transform.position.x, 0, 0), new Vector3(transform.position.x, 0, 0));
    }
}
