using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

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

    private const string THREAT_TEXT = "Threat Level ";

    public List<DifficultyLevel> difficultyLevels;
    public SpaceshipSpawner spaceshipSpawner;
    public Slider threatSlider;
    public Image threatLevelFill;
    public TextMeshProUGUI threatLevelText;
    public List<ThreatLevel> threatLevels;
    public float criticalTime;
    public float criticalFlashInterval;

    private float elpasedSinceLastSpawn = 0f;
    private float timeToNextSpawn = 0f;

    private int shipCount;
    private Coroutine criticalCoroutine = null;
    private Coroutine gameOverCoroutine = null;

    private void Start()
    {
        UpdateThreatLevelSlider();
    }

    public void UpdateShipCount(int value) 
    {
        shipCount += value;

        UpdateThreatLevelSlider();

        if (shipCount < 0) 
        {
            shipCount = 0;
        }

        if (shipCount == 0) 
        {
            Debug.Log("SHIP 0");
            elpasedSinceLastSpawn = timeToNextSpawn;
        }
    }

    // Update is called once per frame
    void Update()
    {
        elpasedSinceLastSpawn += Time.deltaTime;

        if (elpasedSinceLastSpawn >= timeToNextSpawn && gameOverCoroutine == null && spaceshipSpawner.CanSpawn()) 
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

    private ThreatLevel GetCurrentThreatLevel() 
    {
        for(int i = threatLevels.Count-1; i >= 0; i--)
        {
            if (shipCount >= threatLevels[i].enemyMinThreshold)
            {
                if (i == threatLevels.Count-1 && criticalCoroutine == null) 
                {
                    // start critical timer
                    criticalCoroutine = StartCoroutine(CriticalTimer());
                    gameOverCoroutine = StartCoroutine(GameOverTimer());
                }
                else if (i < threatLevels.Count-1)
                {
                    // cancel coroutine if active and set slider to active
                    if (criticalCoroutine != null)
                    {
                        StopCoroutine(criticalCoroutine);
                        criticalCoroutine = null;
                        threatLevelFill.enabled = true;
                    }
                    if (gameOverCoroutine != null) 
                    {
                        StopCoroutine(gameOverCoroutine);
                        gameOverCoroutine = null;
                    }
                }
                return threatLevels[i];
            }
        }

        return threatLevels[0];
    }

    private void UpdateThreatLevelSlider() 
    {
        ThreatLevel threatLevel = GetCurrentThreatLevel();
        threatSlider.value = threatLevel.level+1;
        threatLevelFill.color = threatLevel.color;
        threatLevelText.text = THREAT_TEXT + threatLevel.threatLevelName;
    }

    private IEnumerator CriticalTimer() 
    {
        while (true)
        {
            yield return new WaitForSeconds(criticalFlashInterval);
            threatLevelFill.enabled = false;
            yield return new WaitForSeconds(criticalFlashInterval);
            threatLevelFill.enabled = true;
        }
    }

    private IEnumerator GameOverTimer() 
    {
        yield return new WaitForSeconds(criticalTime);
        GameOverManager.Instance.StartGameover();
    }
}
