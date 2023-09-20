using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplePickup : MonoBehaviour
{
    public static Action ApplePicked;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent.GetComponent<PlayerController>() && collision.gameObject.name.EndsWith("d"))
        {
            ApplePicked.Invoke();
            Destroy(this.gameObject);
        }
    }
}
