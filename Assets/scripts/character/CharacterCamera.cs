using System;
using UnityEngine;

public class CharacterCamera : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 velocitySmoothing = Vector3.zero;
    private float smoothTime = 0.15f;

    [SerializeField] private float deadZone = 0.5f;
    [SerializeField] private float maxOffset = 1.5f;

    void Start()
    {
        rb = transform.parent.GetComponent<Rigidbody2D>();
    }

    void LateUpdate()
    {
        Vector2 vel = rb.linearVelocity;

        float offsetX = CalculateOffset(vel.x);
        float offsetY = CalculateOffset(vel.y);

        Vector3 targetLocalPosition = new Vector3(offsetX, offsetY, -10);

        transform.localPosition = Vector3.SmoothDamp(
            transform.localPosition,
            targetLocalPosition,
            ref velocitySmoothing,
            smoothTime
        );
    }

    private float CalculateOffset(float velocity)
    {
        if (Mathf.Abs(velocity) < deadZone)
            return 0f;

        float offset = Mathf.Sign(velocity) * Mathf.Pow(Mathf.Abs(velocity) / 20f, 1.5f) * maxOffset;
        return offset;
    }
}
