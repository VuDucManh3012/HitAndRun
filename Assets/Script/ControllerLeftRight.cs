using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerLeftRight : MonoBehaviour
{
    private Touch touch;
    public float? deltaX = null;
    private Vector2 startPos;
    private Vector2 direction;
    public bool AcceptMoveCharacterChildren;
    // Start is called before the first frame update
    void Start()
    {
        AcceptMoveCharacterChildren = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            if (AcceptMoveCharacterChildren)
            {
                touch = Input.GetTouch(0);

                switch (touch.phase)
                {
                    //When a touch has first been detected, change the message and record the starting position
                    case TouchPhase.Began:
                        // Record initial touch position.
                        startPos = touch.position;
                        break;

                    //Determine if the touch is a moving touch
                    case TouchPhase.Moved:

                        // Determine direction by comparing the current touch position with the initial on
                        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + touch.deltaPosition.x * Time.deltaTime *2f, transform.position.y, transform.position.z), 0.1f);
                        //xoay theo direc 
                        //
                        break;
                }
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            deltaX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            deltaX = null;
        }
        if (deltaX != null)
        {
            if (AcceptMoveCharacterChildren)
            {
                float dis = Input.mousePosition.x - deltaX.Value;
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + dis *Time.deltaTime, transform.position.y, transform.position.z), 0.1f);
                deltaX = Input.mousePosition.x;
            }

        }
    }
}
