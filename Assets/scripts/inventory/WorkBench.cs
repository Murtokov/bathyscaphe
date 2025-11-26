using UnityEngine;

public class WorkBench : MonoBehaviour
{
    public GameObject contextHint;
    private bool inTrigger = false, isForUpgrade = false;
    public 
    void Start()
    {
        contextHint = transform.GetChild(0).gameObject;
        contextHint.SetActive(false);
    }

    public void Update()
    {
        if (inTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inTrigger = true;
            contextHint.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inTrigger = false;
            contextHint.SetActive(false);
        }
    }
}
