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
        //myBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.forward * Speed * Time.deltaTime,Space.World);
        Vector3 vel = myBody.transform.InverseTransformVector(myBody.velocity);
        vel.z = Speed;
        myBody.velocity = myBody.transform.TransformVector(vel);
    }
    public void SetSpeed(int speed)
    {
        Speed = speed;
    }
}
