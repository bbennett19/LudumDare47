using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ThreatLevel", order = 1)]
public class ThreatLevel : ScriptableObject
{
    public int level;
    public int enemyMinThreshold;
    public Color color;
    public string threatLevelName;
}
