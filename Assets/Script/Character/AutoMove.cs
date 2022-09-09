using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour
{
    [SerializeField] Rigidbody myBody;
    public ControllerPlayer ControllerPlayer;
    public float Speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vel = myBody.transform.InverseTransformVector(myBody.velocity);
        vel.z = Speed;
        myBody.velocity = myBody.transform.TransformVector(vel);
    }
    public void SetSpeed(int speed)
    {
        Speed = speed;
    }
}
