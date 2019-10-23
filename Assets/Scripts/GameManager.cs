using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public delegate void GameStarted();
    public static event GameStarted gameStartedEvent;

    public Animator canvasAnimator;

    bool gameStarted;

    public void StartGame()
    {
        gameStarted = true;
        TurnOffCanvasHelpTexts();
        gameStartedEvent();
        PlayerController.playerDeathEvent += OnGameOver;
    }

    public void OnGameOver()
    {
        gameStarted = false;
    }

    void TurnOffCanvasHelpTexts()
    {
        canvasAnimator.SetBool("DisappearHelpText", true);
    }
}
