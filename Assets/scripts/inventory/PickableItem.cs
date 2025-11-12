using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickableItem : MonoBehaviour
{
    public bool inTrigger = false;
    public GameObject contextHint;
    public string decsription = "You will get 10% extra shield";
    public string itemName = "shield plate";
    public string fieldName = "";
    void Start()
    {
        contextHint = transform.GetChild(0).gameObject;
        contextHint.SetActive(false);
        string sceneName = SceneManager.GetActiveScene().name;
        object config = SavesManager.LoadConfigForScene(sceneName);
        if (config != null)
        {
            var field = config.GetType().GetField(fieldName);
            if (field != null && (bool)field.GetValue(config))
            {
                gameObject.SetActive(false);
            }
        }
    }

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
                string sceneName = SceneManager.GetActiveScene().name;
                object config = SavesManager.LoadConfigForScene(sceneName);
                if (config != null)
                {
                    var field = config.GetType().GetField(fieldName);
                    if (field != null)
                    {
                        field.SetValue(config, true);
                        var method = typeof(SavesManager).GetMethod("SaveConfig")
                            .MakeGenericMethod(config.GetType());
                        method.Invoke(null, new object[] { config, sceneName });
                    }
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
