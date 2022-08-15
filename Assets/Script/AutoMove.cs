using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour
{
    Rigidbody myBody;
    public ControllerPlayer ControllerPlayer;
    public float Speed;
    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * Speed * Time.deltaTime,Space.World);
    }
    public void SetSpeed(int speed)
    {
        Speed = speed;
    }
}
