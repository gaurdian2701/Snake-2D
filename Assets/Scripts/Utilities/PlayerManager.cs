using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Transform P1pos;
    [SerializeField] private Transform P2pos;

    private GameObject playerPrefab;
    private void Awake()
    {
        playerPrefab = GetComponent<PlayerInputManager>().playerPrefab;
    }
    private void Start()
    {
        var p1 = PlayerInput.Instantiate(playerPrefab, controlScheme: "Player1", pairWithDevice: Keyboard.current);
        var p2 = PlayerInput.Instantiate(playerPrefab, controlScheme: "Player2", pairWithDevice: Keyboard.current);

        SetPosition(p1.gameObject, P1pos.position);
        SetPosition(p2.gameObject, P2pos.position);
    }

    private void SetPosition(GameObject player, Vector3 position)
    {
        player.transform.position = position;
    }
}
