using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBossEnding2 : MonoBehaviour
{
    [HideInInspector]
    public ControllerPlayer ControllerPlayer;
    public void SetPositionChar(Transform transform)
    {
        ControllerPlayer.SetPosition(transform);
    }
    public void SetVictory()
    {
        ControllerPlayer.Victory();
    }
}
