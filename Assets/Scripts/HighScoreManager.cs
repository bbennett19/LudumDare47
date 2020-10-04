using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    // Start 
    private static HighScoreManager _instance;
    public static HighScoreManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private float currentHighScore = 0;

    public float GetHighScore() 
    {
        return currentHighScore;
    }

    public void NewScore(float score) 
    {
        if (score > currentHighScore)
        {
            currentHighScore = score;
        }
    }
    
}
