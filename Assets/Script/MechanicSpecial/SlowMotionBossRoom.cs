using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotionBossRoom : MonoBehaviour
{
    private float timeScaleDisSubtract = 0.006f;
    private float minTimeScale = 0.05f;
    private float fixedDeltaTimeNormal;

    private bool subTractTime = false;

    [Header("BossRoom")]
    private BossRoomMechanic BossRoomMechanic;
    private void Start()
    {
        fixedDeltaTimeNormal = Time.fixedDeltaTime;
        BossRoomMechanic = transform.parent.GetComponent<BossRoomMechanic>();
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!subTractTime)
        {
            SubtractTimeScale();
            if (Time.timeScale <= minTimeScale)
            {
                subTractTime = true;
                SetTimeScaleNormal();
                Action();
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
    private void Action()
    {
        BossRoomMechanic.StartSlide();
        transform.gameObject.SetActive(false);
    }
}
