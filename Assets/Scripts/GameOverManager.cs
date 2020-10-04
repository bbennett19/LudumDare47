using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    private static GameOverManager _instance;
    public static GameOverManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }
    
    public GameObject mainUI;
    public GameObject gameOverUI;

    private List<SpaceshipController> spaceships = new List<SpaceshipController>();
    private PlayerController character;

    public void RegisterSpaceship(SpaceshipController spaceship) 
    {
        spaceships.Add(spaceship);
    }

    public void UnregisterSpaceship(SpaceshipController spaceship)
    {
        spaceships.Remove(spaceship);
    }

    public void RegisterCharacter(PlayerController character)
    {
        this.character = character;
    }

    public void StartGameover()
    {
        // coordinate things
        foreach(SpaceshipController controller in spaceships)
        {
            controller.StartGameOver();
        }
        character.GameOver();
        HighScoreManager.Instance.NewScore(ScoreController.Instance.GetCurrentScore());
        StartCoroutine(SwapUI());
    }

    private IEnumerator SwapUI() 
    {
        yield return new WaitForSeconds(8);
        mainUI.SetActive(false);
        gameOverUI.SetActive(true);
    }
}
