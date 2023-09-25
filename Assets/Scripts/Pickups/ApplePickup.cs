using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplePickup : Pickup
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            collision.GetComponent<PlayerController>().IncreaseSize();
            RandomizePosition();
        }
    }
}
