using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotionLerp : MonoBehaviour
{
    public float minTimeScale = 0.1f;
    private float fixedDeltaTimeNormal;

    private bool subTractTime = false;
    private void Start()
    {
        fixedDeltaTimeNormal = Time.fixedDeltaTime;
        SubtractTimeScale();
    }
    // Update is called once per frame
    private void FixedUpdate()
    { 
    }
    private void SubtractTimeScale()
    {
        Time.timeScale = minTimeScale;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
    public void SetTimeScaleNormal()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = fixedDeltaTimeNormal;
    }
}
