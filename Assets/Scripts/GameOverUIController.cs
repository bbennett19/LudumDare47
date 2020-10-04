using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverUIController : MonoBehaviour
{
    private const string HIGH_SCORE = "High Score: ";

    public TextMeshProUGUI highScoreText;

    private void OnEnable() {
        highScoreText.text = HIGH_SCORE + HighScoreManager.Instance.GetHighScore();
    }

    public void Restart() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
