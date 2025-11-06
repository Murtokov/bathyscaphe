using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportManager : MonoBehaviour
{
    public bool inTrigger = false;
    public GameObject contextHint;
    public void Start()
    {
        if (!(contextHint == null))
        {
            contextHint.SetActive(false);
        }
    }
    public void Update()
    {
        if (inTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadScene(gameObject.name);
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Submarine"))
        {
            if (gameObject.name == "Exit")
            {
                InventoryManager Inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryManager>();
                if (Inventory.CheckItem("golden key"))
                {
                    contextHint.SetActive(true);
                    inTrigger = true;
                }
            }
            else
            {
                inTrigger = true;
                contextHint.SetActive(true);
            }
        }
        if (collision.CompareTag("Player"))
        {
            if (SceneManager.GetActiveScene().name == "Level1MainBase")
            {
                inTrigger = true;
                contextHint.SetActive(true);
            }
            else
            {
                SceneManager.LoadScene(gameObject.name);
            }
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Submarine"))
        {
            inTrigger = false;
            contextHint.SetActive(false);
        }
        if (collision.CompareTag("Player"))
        {
            if (SceneManager.GetActiveScene().name == "Level1MainBase")
            {
                inTrigger = true;
                contextHint.SetActive(true);
            }
        }
    }
}
