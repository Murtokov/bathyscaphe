using UnityEngine;

public class TeleportCycle : MonoBehaviour
{
    public Transform otherEdge;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Vector3 offset = new Vector3(100, 0);
        if (transform.position.x <= otherEdge.position.x)
        {
            offset *= -1;
        }
        other.transform.position = otherEdge.position + offset;
    }
}
