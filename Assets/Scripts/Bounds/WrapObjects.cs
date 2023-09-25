using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrapObjects : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private float offset;

    private static WrapObjects instance;
    private Vector3 bodyPosition;

    public static WrapObjects Instance { get { return instance; } }


    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        else
            Destroy(this.gameObject);
    }

    public void WrapPosition(GameObject body)
    {
        if (body == null)
            return;

        bodyPosition = body.transform.position;
        float xVal = bodyPosition.x;
        float yVal = bodyPosition.y;

        if (Player.transform.up.x == 0f)
            PerformYWrapping(xVal, yVal, ref body);

        else
            PerformXWrapping(xVal, yVal, ref body);
    }

    private void PerformXWrapping(float xVal, float yVal, ref GameObject body)
    {
        offset *= Mathf.Sign(xVal);
        bodyPosition = new Vector3(-xVal + offset, yVal, 0f);
        body.transform.position = bodyPosition;
    }

    private void PerformYWrapping(float xVal, float yVal, ref GameObject body)
    {
        offset *= Mathf.Sign(yVal);
        bodyPosition = new Vector3(xVal, -yVal + offset, 0f);
        body.transform.position = bodyPosition;
    }
}
