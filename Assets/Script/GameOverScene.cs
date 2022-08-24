using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScene : MonoBehaviour
{
    public GameObject ModelGameOver;
    public GameObject CamDead;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ModelGameOver.SetActive(true);
        CamDead.SetActive(false);
    }
}
