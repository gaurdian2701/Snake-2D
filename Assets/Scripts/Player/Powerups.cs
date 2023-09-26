using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Powerups : MonoBehaviour
{
    [SerializeField] private float powerUpTime;

    private PlayerController playerController;
    private ParticleController particleController;
    private SpriteRenderer playerSprite;
    private ScoreManager scoreManager;

    private float originalSpeed;
    private Color originalColor;
    private float originalScoringValue;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playerSprite = GetComponent<SpriteRenderer>();
        particleController = GetComponent<ParticleController>();

        ScoreManager.SendScores += InitializeOriginalScoringValue;
    }

    private void Start()
    {
        originalSpeed = playerController.GetMoveSpeed();
        originalColor = playerSprite.material.color;
        originalScoringValue = scoreManager.GetDefaultScoringValue();
    }

    private void InitializeOriginalScoringValue(ScoreManager scorer)
    {
        scoreManager = scorer;
        originalScoringValue = scoreManager.GetDefaultScoringValue();
        ScoreManager.SendScores -= InitializeOriginalScoringValue;
    }

    #region SpeedBoost
    public void StartSpeedBoost()
    {
        StopCoroutine(nameof(SpeedBoost));
        particleController.EnableParticles(Color.yellow);
        StartCoroutine(nameof(SpeedBoost)); 
    }

    private IEnumerator SpeedBoost()
    {
        playerController.SetMoveSpeed(originalSpeed * 2f);

        yield return new WaitForSecondsRealtime(powerUpTime);

        playerController.SetMoveSpeed(originalSpeed);
    }
    #endregion

    #region ShieldPowerUp

    public void StartInvincibility()
    {
        StopCoroutine(nameof(ShieldPowerUp));
        particleController.EnableParticles(Color.blue);
        StartCoroutine(nameof(ShieldPowerUp));
    }

    private IEnumerator ShieldPowerUp()
    {
        playerController.ToggleInvincibility();
        playerSprite.material.color = Color.white + Color.blue;

        yield return new WaitForSecondsRealtime(powerUpTime);

        playerController.ToggleInvincibility();
        playerSprite.material.color = originalColor;
    }
    #endregion

    #region ScoreMultiplier
    public void StartScoreMultiplier()
    {
        StopCoroutine(nameof(ScoreMultiplier));
        particleController.EnableParticles(Color.yellow);
        StartCoroutine(nameof(ScoreMultiplier));
    }

    private IEnumerator ScoreMultiplier()
    {
        scoreManager.SetDefaultScoringValue(originalScoringValue * 2);
        playerSprite.material.color = Color.white + Color.yellow;

        yield return new WaitForSecondsRealtime(powerUpTime);

        scoreManager.SetDefaultScoringValue(originalScoringValue);
        playerSprite.material.color = originalColor;
    }
    #endregion
}
