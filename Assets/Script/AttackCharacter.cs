using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCharacter : MonoBehaviour
{
    public bool Subtracting = true;
    float TimefixedDelta;
    public float TimeScaleWish = 0.7f;
    public float DoGiamTimeScale = 0.02f;
    public float DoTangTimeScale = 0.5f;
    public float TimeScaleCurrent;
    // Start is called before the first frame update
    void Start()
    {
        TimefixedDelta = Time.fixedDeltaTime;
    }
    void PlusTimeScale()
    {
        Time.timeScale += DoTangTimeScale;
        Time.fixedDeltaTime = TimefixedDelta;
    }
    void SubtractTimeScale()
    {
        Time.timeScale -= DoGiamTimeScale;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    // Update is called once per frame
    void Update()
    {
        TimeScaleCurrent = Time.timeScale;
        transform.rotation = new Quaternion(0, 0, 0, 1);

        if (Subtracting && Time.timeScale <= TimeScaleWish)
        {
            Time.timeScale = TimeScaleWish;
        }
        else if (!Subtracting && Time.timeScale >= 1.2f)
        {
            Time.timeScale = 1.2f;
        }
        else if (!Subtracting)
        {
            PlusTimeScale();
        }
        else
        {
            SubtractTimeScale();
        }
    }
    public void setSubTractTimeScale(bool subtract)
    {
        Subtracting = subtract;
    }
    public void SetTimeScaleNormal()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = TimefixedDelta;
    }

}
