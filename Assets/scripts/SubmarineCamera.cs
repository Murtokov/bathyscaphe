using System;
using UnityEngine;

public class SubmarineCamera : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = transform.parent.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Debug.Log(rb.linearVelocity);

        float offsetX = CalculateOffset(rb.linearVelocity.x);
        float offsetY = CalculateOffset(rb.linearVelocity.y);

        transform.localPosition = new Vector3(offsetX, offsetY, -10);
    }

    private float CalculateOffset(float velocity)
    {
        return - Mathf.Sign(velocity) * Mathf.Min((float)(Math.Pow((1.0 / 80.0) * Math.Abs(velocity) - Math.Pow(2, 1.0 / 3.0), 3) + 2), 2);
    }
}