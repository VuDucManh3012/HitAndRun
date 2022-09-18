using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBossEnding : MonoBehaviour
{
    private float timeScaleDisSubtract = 0.008f;
    private float minTimeScale = 0.05f;
    private float fixedDeltaTimeNormal;

    [Header("Character")]
    public GameObject Character;
    private ControllerPlayer ControllerPlayer;

    private bool subTractTime = false;

    public GameObject ListTransform;
    private void Start()
    {
        ControllerPlayer = Character.GetComponent<ControllerPlayer>();
        fixedDeltaTimeNormal = Time.fixedDeltaTime;
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!subTractTime)
        {
            SubtractTimeScale();
            if (Time.timeScale <= minTimeScale)
            {
                SetTimeScaleNormal();
                subTractTime = true;
                ChangeCam();
                ListTransform.SetActive(true);
            }
        }
    }
    private void SubtractTimeScale()
    {
        Time.timeScale -= timeScaleDisSubtract;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
    private void SetTimeScaleNormal()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = fixedDeltaTimeNormal;
    }
    private void ChangeCam()
    {
        ControllerPlayer.ChangeCam("CamBossEnding2");
    }
}
