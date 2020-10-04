using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DefficultyLevel", order = 1)]
public class DifficultyLevel : ScriptableObject
{
    public int scoreThreshold;
    public float timeToNextSpawn;
    public int numberSpawnRangeMin;
    public int numberSpawnRangeMax;
    [Range(0, 1)]
    public float ship1Weight;
    [Range(0, 1)]
    public float ship2Weight;
    [Range(0, 1)]
    public float ship3Weight;
}
