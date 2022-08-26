using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour
{
    public CanvasManager canvasManager;
    public GameObject canvasNewSkinDemo;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        canvasManager.ScreenVictoryActive = true;
        if (transform.gameObject.active)
        {
            canvasNewSkinDemo.SetActive(false);
        }
    }
}
