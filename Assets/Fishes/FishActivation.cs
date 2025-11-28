using UnityEngine;

public class FishActivation : MonoBehaviour
{
    public GameObject fish;
    void Start()
    {
        fish = transform.GetChild(0).gameObject;
        fish.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Submarine"))
        {
            fish.SetActive(true);
        }
    }
}
