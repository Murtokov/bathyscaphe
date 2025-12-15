using System.IO;
using UnityEngine;

public class OctopusHealth : FishHealth
{
<<<<<<< Updated upstream
    protected override void _Die()
=======
    public override void Die()
>>>>>>> Stashed changes
    {
        Debug.Log("WTFFFF?????");
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
<<<<<<< Updated upstream
        Destroy(gameObject);
=======

        if (transform.parent != null)
        {
            Destroy(transform.parent.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
>>>>>>> Stashed changes
    }
}
