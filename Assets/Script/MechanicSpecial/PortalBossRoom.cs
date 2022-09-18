using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBossRoom : MonoBehaviour
{
    public Animator MyAnim;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            SetAnimOpen();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            SetAnimClose();
        }
    }
    public void SetAnimOpen()
    {
        MyAnim.SetTrigger("Open");
    }
    public void SetAnimClose()
    {
        MyAnim.SetTrigger("Close");
    }
}
