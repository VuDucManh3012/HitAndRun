using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCharacter : MonoBehaviour
{
    public GameObject Target;
    public Vector3 offSet;
    private Vector3 MyPosition;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = Target.transform.position;
        transform.position += offSet;
    }

    // Update is called once per frame
    void Update()
    {
        MyPosition = transform.position;
        MyPosition.z = Target.transform.position.z + offSet.z;
        transform.position = MyPosition;
    }
}
