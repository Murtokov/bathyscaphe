using System;
using Unity.VisualScripting;
using UnityEngine;

public class PickableItem : MonoBehaviour
{
    public bool inTrigger = false;
    public GameObject contextHint;
    public string decsription = "You will get 10% extra shield";
    public string itemName = "shield plate";
    void Start()
    {
        contextHint = transform.GetChild(0).gameObject;
        contextHint.SetActive(false);
    }

    [System.Obsolete]
    public void Update()
    {
        if (inTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                InventoryManager Inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryManager>();
                if (Inventory.AddItem(transform.GetComponent<SpriteRenderer>().sprite, decsription, itemName))
                {
                    Destroy(gameObject);
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
