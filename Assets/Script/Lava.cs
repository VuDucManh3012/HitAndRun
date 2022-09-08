using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    float scrollSpeed = 0.02f;
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
        rend.material.mainTextureOffset = new Vector2(offset, 0);
    }
}
