using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour {

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;
    public GameObject player;

    int bestScore;
    int currentScore;
    bool gameStarted;

    void Start () {
        PlayerController.playerDeathEvent += OnGameOver;
        GameManager.gameStartedEvent += OnGameStart;
        currentScore = 0;
        bestScore = PlayerPrefs.GetInt("HighScore");
        bestScoreText.text = "Best: " + bestScore;
	}
	
	// Update is called once per frame
	void Update () {
        CalculateScore();
	}

    void CalculateScore()
    {
        if (gameStarted)
        {
            if (player != null)
            {
                currentScore = (int)Vector3.Distance(Vector3.zero, player.transform.position);
                scoreText.text = "" + currentScore;
                if (currentScore > bestScore) {
                    bestScore = currentScore;
                    PlayerPrefs.SetInt("HighScore", bestScore);
                }
            }
        }
    }

    void OnGameStart() {
        gameStarted = true;
        bestScoreText.gameObject.SetActive(false);
    }

    void OnGameOver() {
        gameStarted = false;
        bestScoreText.gameObject.SetActive(true);
        bestScoreText.text = "Best: " + bestScore;
        scoreText.text = "";
    }
}
