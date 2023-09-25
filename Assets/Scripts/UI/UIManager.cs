using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private GameObject gameOverScreen;
    private void Awake()
    {
        gameOverScreen = transform.Find("GameOverScreen").gameObject;
        PlayerController.InitiateGameOverCondition += InitiateGameOverScreen;
    }

    private void Start()
    {
        if(gameOverScreen != null && gameOverScreen.activeInHierarchy)
            gameOverScreen.SetActive(false);
    }

    private void OnDestroy()
    {
        PlayerController.InitiateGameOverCondition -= InitiateGameOverScreen;
    }

    private void InitiateGameOverScreen() => gameOverScreen.SetActive(true);
}
