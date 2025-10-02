using UnityEngine;
using static DoorTrigger;

public class ShadeStatus : MonoBehaviour
{
    private Material mat;
    private Color originalColor;

    private void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            mat = renderer.material;
            originalColor = mat.color;
        }

        DoorTrigger trigger = GetComponentInParent<DoorTrigger>();
        if (trigger != null)
        {
            trigger.OnStateChanged += ChangeTransparent;
        }
    }

    private void ChangeTransparent(DoorState newState)
    {
        if (newState == DoorState.Open)
        {
            SetAlpha(0);
        } 
        else
        {
            SetAlpha(originalColor.a);
        }
    }

    private void SetAlpha(float alpha)
    {
        Color c = mat.color;
        c.a = alpha;
        mat.color = c;
    }
}
