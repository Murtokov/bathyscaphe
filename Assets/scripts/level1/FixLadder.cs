using UnityEngine;

public class FixLadder : MonoBehaviour
{
    public bool inTrigger = false;
    public GameObject contextHint;
    public void Start()
    {
        contextHint = transform.GetChild(0).gameObject;
        contextHint.SetActive(false);
    }
    public void Update()
    {
        if (inTrigger)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                InventoryManager Inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryManager>();
                if (Inventory.DeleteItem("logs"))
                {
                    transform.parent.GetChild(1).gameObject.SetActive(true);
                    transform.parent.GetComponent<LadderManager>().isFixed = true;
                    gameObject.SetActive(false);
                }
                contextHint.SetActive(false);
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InventoryManager Inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryManager>();
            if (Inventory.CheckItem("logs"))
            {
                contextHint.SetActive(true);
                inTrigger = true;
            }
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inTrigger = false;
            contextHint.SetActive(false);
        }
    }
}
