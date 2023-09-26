using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Pickup : MonoBehaviour
{
    [SerializeField] private float enabledCooldownTime;
    [SerializeField] private float disabledCooldownTime;

    private SpriteRenderer sprite;
    private BoxCollider2D powerUpCollider;
    private bool componentsEnabled;
    private BoxCollider2D playArea;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        powerUpCollider = GetComponent<BoxCollider2D>();
        componentsEnabled = true;
    }
    private void Start()
    {
        playArea = PlayAreaBounds.Instance.GetComponent<BoxCollider2D>();
        RandomizePosition();
    }

    public void RandomizePosition()
    {
        Bounds playBounds = playArea.bounds;
        float xBounds = Random.Range(playBounds.min.x, playBounds.max.x);
        float yBounds = Random.Range(playBounds.min.y, playBounds.max.y);

        transform.position = new Vector3(Mathf.Round(xBounds), Mathf.Round(yBounds), 0);

        RefreshRandomization();
    }

    private void CooldownPowerUp()
    {
        componentsEnabled = !componentsEnabled;
        powerUpCollider.enabled = componentsEnabled;
        sprite.enabled = componentsEnabled;
    }

    private void RefreshRandomization()
    {
        CooldownPowerUp();
        CancelInvoke(nameof(RandomizePosition));

        if (componentsEnabled)
            Invoke(nameof(RandomizePosition), enabledCooldownTime);
        else
            Invoke(nameof(RandomizePosition), disabledCooldownTime);
    }
}
