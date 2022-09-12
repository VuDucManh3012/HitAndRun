using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    private float x = -0.1f;
    private float y = 0.7f;
    private float z = 0.7f;
    private float timeDestroy = 0.4f;
    public bool EnemyEnding = false;
    public bool EnemyInStageEnding=false;
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            EnemyInStageEnding = transform.parent.GetComponent<Enemy>().EnemyEnding;
        }
        catch
        {

        }
        
        if (!EnemyEnding)
        {
            Destroy(transform.gameObject, timeDestroy);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        position.z += z;
        if (!EnemyInStageEnding)
        {
            position.y += y * 0.8f;
        }
        else
        {
            position.y += y * 1.5f;
        }
        position.x += x;
        transform.position = position;
    }
}
