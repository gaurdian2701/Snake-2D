using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAreaBounds : MonoBehaviour
{
    private static PlayAreaBounds instance;
    public static PlayAreaBounds Instance { get { return instance; } }


    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    private void OnDestroy()
    {
        if(instance == this)
            instance = null;
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
