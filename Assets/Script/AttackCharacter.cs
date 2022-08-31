using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCharacter : MonoBehaviour
{
    public bool Subtracting = true;
    float TimefixedDelta;
    public float TimeScaleCurrent;
    // Start is called before the first frame update
    void Start()
    {
        TimefixedDelta = Time.fixedDeltaTime;
    }
    void PlusTimeScale()
    {
        Time.timeScale += 0.5f;
        Time.fixedDeltaTime = TimefixedDelta;
    }
    void SubtractTimeScale()
    {
        Time.timeScale -= 0.015f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = new Quaternion(0, 0, 0, 1);

        TimeScaleCurrent=Time.timeScale;
        if (Subtracting && Time.timeScale <= 0.7f)
        {
            Time.timeScale = 0.7f;
        }
        else if (!Subtracting && Time.timeScale >= 1)
        {
            Time.timeScale = 1;
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

}
