using Microsoft.Unity.VisualStudio.Editor;
using UnityEditor.Rendering.Universal;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public Sprite spriteOn;
    public Sprite spriteOff;
    private bool inTrigger = false, switched = false;
    public GameObject contextHint;
    void Start()
    {
        contextHint = transform.GetChild(0).gameObject;
        contextHint.SetActive(false);
        if (switched)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = spriteOff;
        }
        if (!switched)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = spriteOn;
        }
    }
    public void Update()
    {
        if (inTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!switched)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = spriteOff;
                    switched = true;
                    GameObject lever = GameObject.FindGameObjectWithTag("Light");
                    lever.transform.GetChild(0).gameObject.SetActive(true);
                }
                else
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = spriteOn;
                    switched = false;
                    GameObject lever = GameObject.FindGameObjectWithTag("Light");
                    lever.transform.GetChild(0).gameObject.SetActive(false);
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
