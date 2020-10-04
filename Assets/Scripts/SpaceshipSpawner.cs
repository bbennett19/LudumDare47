using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipSpawner : MonoBehaviour
{
    public float spawnWidth;
    public float spawnHeight;
    public Vector2 spawnCenter;
    public Vector2 spawnTimeDelayRange;

    public GameObject spaceship1;
    public GameObject spaceship2;
    public GameObject spaceship3;

    public void SpawnShips(DifficultyLevel difficulty) 
    {
        StartCoroutine(SpawnShipsInternal(difficulty));
    }

    private IEnumerator SpawnShipsInternal(DifficultyLevel difficulty) 
    {
        int spawnCount = Random.Range(difficulty.numberSpawnRangeMin, difficulty.numberSpawnRangeMax);
        
        for(int i = 0; i < spawnCount; i++) 
        {
            yield return new WaitForSeconds(Random.Range(spawnTimeDelayRange.x, spawnTimeDelayRange.y));
            SpawnShip(GetShip(difficulty));
        }
    }

    private void SpawnShip(GameObject ship) 
    {
        Vector3 pos = new Vector3(Random.Range(spawnCenter.x - spawnWidth / 2, spawnCenter.x + spawnWidth / 2),
                                  Random.Range(spawnCenter.y - spawnHeight / 2, spawnCenter.y + spawnHeight / 2),
                                  0);
        Instantiate(ship, pos, Quaternion.identity);
    }

    private GameObject GetShip(DifficultyLevel difficulty) 
    {
        float totalWeight = difficulty.ship3Weight + difficulty.ship2Weight + difficulty.ship1Weight;

        if (totalWeight != 0) {
            float thresholdValue = Random.Range(0f, 1f);
            float ship3Threshold = difficulty.ship3Weight / totalWeight;
            float ship2Threshold = difficulty.ship2Weight / totalWeight;

            if (thresholdValue < ship3Threshold)
            {
                return spaceship3;
            }

            thresholdValue -= ship3Threshold;

            if (thresholdValue < ship2Threshold)
            {
                return spaceship2;
            }
        }

        // default to easiest
        return spaceship1;
    }
}
