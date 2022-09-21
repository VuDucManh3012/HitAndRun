using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotion : MonoBehaviour
{
    private float timeScaleDisSubtract = 0.004f;
    private float minTimeScale = 0.1f;
    private float fixedDeltaTimeNormal;

    private bool subTractTime = false;
    private void Start()
    {
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
}
