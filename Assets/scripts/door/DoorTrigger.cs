using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public enum DoorState { Closed, Open }
    public DoorState CurrentState = DoorState.Closed;

    public delegate void TriggerAction(DoorState newState);
    public event TriggerAction OnStateChanged;

    private bool playerNearDoor = false;
    private void Update()
    {
        if (playerNearDoor && Input.GetKeyDown(KeyCode.E))
        {
            ToggleState();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearDoor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearDoor = false;
        }
    }

    private void ToggleState()
    {
        if (CurrentState == DoorState.Closed)
        {
            CurrentState = DoorState.Open;
        }
        else
        {
            CurrentState = DoorState.Closed;
        }

        OnStateChanged?.Invoke(CurrentState);
    }
}
