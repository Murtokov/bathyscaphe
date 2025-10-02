using UnityEngine;

public class SimpleTriggerTest : MonoBehaviour
{
    private void Start()
    {
        // Проверка настроек при старте
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            Debug.Log($"Trigger: {collider.isTrigger}, Enabled: {collider.enabled}");
        }
        else
        {
            Debug.LogError("Collider not found!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Вход: {other.name} | Tag: {other.tag} | Layer: {LayerMask.LayerToName(other.gameObject.layer)}");

        // Проверяем Rigidbody у входящего объекта
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Debug.Log($"Rigidbody: Kinematic={rb.isKinematic}");
        }
        else
        {
            Debug.LogWarning($"No Rigidbody on {other.name}");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log($"Выход: {other.name}");
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log($"Внутри: {other.name} - Frame: {Time.frameCount}");
    }
}