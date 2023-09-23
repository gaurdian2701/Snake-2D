using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAreaBounds : MonoBehaviour
{
    private BoxCollider2D playBounds;
    private void Start()
    {
        playBounds = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject != null && collision.gameObject.GetComponent<PlayerController>())
        {
            collision.gameObject.GetComponent<PlayerController>().WrapPosition();
            collision.GetComponent<PlayerController>().enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != null && collision.gameObject.GetComponent<PlayerController>())
        {
            collision.GetComponent<PlayerController>().enabled = true;
        }
    }
}
