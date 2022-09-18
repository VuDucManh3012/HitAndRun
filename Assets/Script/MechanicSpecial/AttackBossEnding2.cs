using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBossEnding2 : MonoBehaviour
{
    [HideInInspector]
    public ControllerPlayer ControllerPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetPositionChar(Transform transform)
    {
        ControllerPlayer.SetPosition(transform);
    }
    public void SetVictory()
    {
        ControllerPlayer.Victory();
    }
}
