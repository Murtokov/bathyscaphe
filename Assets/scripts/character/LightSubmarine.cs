using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightSubmarine : MonoBehaviour
{
    private Transform parentTransform;
    private Vector3 initialLocalPosition;
    private float initialZRotation;

    void Start()
    {
        parentTransform = transform.parent.parent;
        initialLocalPosition = transform.localPosition;
        initialZRotation = transform.localEulerAngles.z;
    }

    void LateUpdate()
    {
        if (parentTransform != null)
        {
            SpriteRenderer parentSR = parentTransform.GetComponent<SpriteRenderer>();
            if (parentSR != null)
            {
                float flipSign = parentSR.flipX ? -1f : 1f;

                transform.localPosition = new Vector3(initialLocalPosition.x * flipSign,
                                                      initialLocalPosition.y,
                                                      initialLocalPosition.z);

                transform.localEulerAngles = new Vector3(
                    transform.localEulerAngles.x,
                    transform.localEulerAngles.y,
                    initialZRotation * flipSign
                );
            }
        }
    }
}
