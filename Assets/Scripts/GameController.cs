using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private static GameController _instance;
    public static GameController Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    public List<DifficultyLevel> difficultyLevels;
    public SpaceshipSpawner spaceshipSpawner;
    public int maxShips;
    public Slider threatSlider;

    private float elpasedSinceLastSpawn = 0f;
    private float timeToNextSpawn = 0f;

    private int shipCount;

    public void UpdateShipCount(int value) 
    {
        shipCount += value;

        threatSlider.value = (float) shipCount / maxShips;

        if (shipCount >= maxShips) 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        elpasedSinceLastSpawn += Time.deltaTime;

        if (elpasedSinceLastSpawn >= timeToNextSpawn) 
        {
            TriggerShipsSpawn();
        }
    }

    private void TriggerShipsSpawn()
    {
        DifficultyLevel difficulty = GetCurrentDifficultyLevel();
        timeToNextSpawn = difficulty.timeToNextSpawn;
        elpasedSinceLastSpawn = 0f;
        spaceshipSpawner.SpawnShips(difficulty);
    }

    private DifficultyLevel GetCurrentDifficultyLevel() 
    {
        int currentScore = ScoreController.Instance.GetCurrentScore();

        for(int i = difficultyLevels.Count-1; i >= 0; i--)
        {
            if (currentScore >= difficultyLevels[i].scoreThreshold)
            {
                return difficultyLevels[i];
            }
        }

        return difficultyLevels[difficultyLevels.Count-1];
    }
}
