using UnityEngine;

public class TeleportCycle : MonoBehaviour
{
    public Transform otherEdge;
    public Transform player;
    public Transform playerCamera;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject);
        if (other.CompareTag("Submarine"))
        {
            Debug.Log("TP");
            Vector3 offset = new Vector3(30, 0);
            if (transform.position.x <= otherEdge.position.x)
            {
                offset *= -1;
            }
            player.position = otherEdge.position + offset;
            playerCamera.position = otherEdge.position + offset;
        }
    }
}
