using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Light : MonoBehaviour
{
    private Transform parentTransform;
    private Vector3 initialLocalPosition;
    private float initialZRotation;

    void Start()
    {
        if (transform.parent != null)
        {
            parentTransform = transform.parent.parent;
            initialLocalPosition = transform.localPosition;
            initialZRotation = transform.localEulerAngles.z;
        }
        else
        {
            Debug.LogWarning("Этот объект не имеет родителя!");
        }
    }

    void LateUpdate()
    {
        if (parentTransform != null)
        {
            SpriteRenderer parentSR = parentTransform.GetComponent<SpriteRenderer>();
            if (parentSR != null)
            {
                float flipSign = parentSR.flipX ? -1f : 1f;

                // Инвертируем позицию по X
                transform.localPosition = new Vector3(initialLocalPosition.x * flipSign,
                                                      initialLocalPosition.y,
                                                      initialLocalPosition.z);

                // Инвертируем угол Z, чтобы светил в противоположную сторону
                transform.localEulerAngles = new Vector3(
                    transform.localEulerAngles.x,
                    transform.localEulerAngles.y,
                    initialZRotation * flipSign
                );
            }
        }
    }
}
