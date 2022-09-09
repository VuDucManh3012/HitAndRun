using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScene : MonoBehaviour
{
    public GameObject ModelGameOver;
    public GameObject CamDead;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.gameObject.activeInHierarchy)
        {
            ModelGameOver.SetActive(true);
            CamDead.SetActive(false);
        }
    }
}
