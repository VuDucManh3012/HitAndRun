using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    Rigidbody rigidbody;
    public float x = 0.1f;
    public float y = 0.7f;
    public float z = 0.7f;
    public float timeDestroy = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        Destroy(transform.gameObject, timeDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        position.z += z;
        position.y += y;
        position.x += x;
        transform.position = position;
    }
}
