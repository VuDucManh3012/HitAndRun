using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Boss", menuName =("Boss"))]
public class Boss : ScriptableObject
{
    public int numberId;
    public string name;
    public Sprite image;
    public Sprite imageShadow;
    public int healthCurrent;
    public int healthMax;
    public bool fighted;
    public int levelToFight;
}
