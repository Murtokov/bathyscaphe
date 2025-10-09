using Microsoft.Unity.VisualStudio.Editor;
using UnityEditor.Rendering.Universal;
using UnityEngine;

public class Note : MonoBehaviour
{
    private bool inTrigger = false, opened = false;
    public GameObject scroll, contextHint;
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
                    scroll.SetActive(false);
                    opened = false;
                }
                else
                {
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
