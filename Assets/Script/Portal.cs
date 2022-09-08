using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    float scrollSpeed = 0.0002f;
    Renderer rend;
    float offset;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        offset += scrollSpeed;
        rend.material.mainTextureOffset = new Vector2(0 , offset);
    }
}
