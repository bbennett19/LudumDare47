using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    private static ScoreController _instance;
    public static ScoreController Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    private const string SCORE_TEXT = "Score: ";

    public TextMeshProUGUI scoreText;
    private int score = 0;
    

    public void Reset() {
        UpdateScore(-score);
    }

    public void UpdateScore(int change) 
    {
        score += change;
        scoreText.text = SCORE_TEXT + score;
    }

    public int GetCurrentScore() {
        return score;
    }
}
