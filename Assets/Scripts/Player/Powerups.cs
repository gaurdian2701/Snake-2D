using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Powerups : MonoBehaviour
{
    private PlayerController playerController;
    private float originalSpeed;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        originalSpeed = playerController.GetMoveSpeed();
    }
    public void StartSpeedBoost()
    {
        StopCoroutine(nameof(SpeedBoost));
        playerController.EnableParticles(Color.yellow);
        StartCoroutine(nameof(SpeedBoost)); 
    }

    private IEnumerator SpeedBoost()
    {
        playerController.SetMoveSpeed(originalSpeed * 2f);
        yield return new WaitForSecondsRealtime(7f);
        playerController.SetMoveSpeed(originalSpeed);
    }
}
