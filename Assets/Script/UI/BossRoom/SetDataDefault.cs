using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class SetDataDefault : MonoBehaviour
{
    public List<Boss> ListDataNormal;
    public List<Boss> ListDataDefault;

    [ContextMenu("SetDataBoss")]
    public void SetDefault()
    {
        for (int i = 0; i < ListDataNormal.Count; i++)
        {
            ListDataNormal[i].numberId = ListDataDefault[i].numberId;
            ListDataNormal[i].name = ListDataDefault[i].name;
            ListDataNormal[i].image = ListDataDefault[i].image;
            ListDataNormal[i].imageShadow = ListDataDefault[i].imageShadow;
            ListDataNormal[i].healthCurrent = ListDataDefault[i].healthCurrent;
            ListDataNormal[i].healthMax = ListDataDefault[i].healthMax;
            ListDataNormal[i].fighted = ListDataDefault[i].fighted;
            ListDataNormal[i].levelToFight = ListDataDefault[i].levelToFight;
            ListDataNormal[i].dead = ListDataDefault[i].dead;
        }
    }
}
