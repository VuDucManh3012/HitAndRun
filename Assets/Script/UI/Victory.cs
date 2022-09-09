using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour
{
    public CanvasManager canvasManager;
    public GameObject canvasNewSkinDemo;

    [Header("ButoonInLv1")]
    public List<GameObject> ButtonOffInLv1;
    public GameObject ButtonLv1;
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
        if (!onButtonAdsLV1)
        {
            if (System.Int32.Parse(PlayerPrefs.GetString("stage")) - 1 == 1)
            {
                foreach(GameObject item in ButtonOffInLv1)
                {
                    item.SetActive(false);
                }
                ButtonLv1.SetActive(true);
            }
            onButtonAdsLV1 = true;
        }
    }
    bool onButtonAdsLV1 = false;
}
