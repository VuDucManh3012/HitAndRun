using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2 : MonoBehaviour
{
    public bool unsetTarget;
    public Transform Target;
    public float smooth = 0.125f;
    public Vector3 offset;
    private ControllerPlayer controllerPlayer;
    public bool onJump;
    public float PositionYOnJump = 2.3f;
    // Start is called before the first frame update
    void Start()
    {
        onJump = false;
        controllerPlayer = Target.GetComponent<ControllerPlayer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //new Vector3(Target.transform.position.x, 0,Target.transform.position.z)
        //Target.transform.position
        if (onJump)
        {
            Vector3 smoothPosition = Vector3.Lerp(transform.position, new Vector3(Target.transform.position.x, PositionYOnJump , Target.transform.position.z), smooth);
            transform.position = smoothPosition;
        }
        else
        {
            Vector3 smoothPosition = Vector3.Lerp(transform.position, Target.transform.position, smooth);
            transform.position = smoothPosition;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Character")
        {
            onJump = false;
            controllerPlayer.OnJump = false;
        }
    }
}

