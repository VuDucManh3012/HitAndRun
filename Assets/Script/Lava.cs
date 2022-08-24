﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    float scrollSpeed = 0.5f;
    Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float offset = Time.time * scrollSpeed;
        rend.material.SetTextureOffset("MainTex", new Vector2(offset, 0));
        rend.material.mainTextureOffset = new Vector2(offset, 0);
    }
}
