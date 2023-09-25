using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private GameObject gameOverScreen;
    private GameObject pauseScreen;

    private bool gamePaused = false;
    private void Awake()
    {
        gameOverScreen = transform.Find("GameOverScreen").gameObject;
        pauseScreen = transform.Find("PauseScreen").gameObject;
        PlayerController.InitiateGameOverCondition += InitiateGameOverScreen;
        PlayerController.GamePaused += TogglePauseScreen;
    }

    private void Start()
    {
        if(gameOverScreen != null && gameOverScreen.activeInHierarchy)
            gameOverScreen.SetActive(false);

        if (pauseScreen != null && pauseScreen.activeInHierarchy)
            pauseScreen.SetActive(false);
    }

    private void OnDestroy()
    {
        PlayerController.InitiateGameOverCondition -= InitiateGameOverScreen;
        PlayerController.GamePaused -= TogglePauseScreen;
    }

    private void InitiateGameOverScreen() => gameOverScreen.SetActive(true);

    public void TogglePauseScreen()
    {
        if (Time.timeScale == 0f)
            return;

        gamePaused = !gamePaused;
        pauseScreen.SetActive(gamePaused);
    }
}
