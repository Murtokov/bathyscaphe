using System.IO;
using UnityEngine;

public class OctopusHealth : FishHealth
{
    protected override void _Die()
    {
        Sprite sprite = Resources.Load<Sprite>("golden key level1 (1)");
        string description = "Opens The Door";
        string itemName = "golden key";
        InventoryManager Inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryManager>();
        string filePath = Path.Combine(Application.persistentDataPath, "Inventory.json");
        if (Inventory.AddItem(sprite, description, itemName))
        {
            Inventory.SaveInventory(filePath);
        }
        else
        {
            Debug.Log("WTFFFF?????");
        }
        Destroy(gameObject);
    }
}
