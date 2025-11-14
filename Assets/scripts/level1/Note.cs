using UnityEngine;

public class Note : MonoBehaviour
{
    public GameObject scroll, contextHint;
    private bool inTrigger = false, opened = false;

    public void Start()
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
                if (opened)
                {
                    Time.timeScale = 1f;
                    scroll.SetActive(false);
                    opened = false;
                }
                else
                {
                    Time.timeScale = 0f;
                    scroll.SetActive(true);
                    opened = true;
                }
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