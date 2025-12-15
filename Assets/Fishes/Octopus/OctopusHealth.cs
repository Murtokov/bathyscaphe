using System.IO;
using UnityEngine;

public class OctopusHealth : FishHealth
{
    public override void Die()
    {
        Level1Ocean level1Ocean = SavesManager.LoadConfig<Level1Ocean>("Level1Ocean");
        if (!level1Ocean.isOctopusDefeated)
        {
            level1Ocean.isOctopusDefeated = true;
            SavesManager.SaveConfig<Level1Ocean>(level1Ocean, "Level1Ocean");
            Sprite sprite = Resources.Load<Sprite>("golden key level1 (1)");
            string description = "Opens The Door";
            string itemName = "golden key";
            InventoryManager Inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryManager>();
            string filePath = Path.Combine(Application.persistentDataPath, "Inventory.json");
            if (Inventory.AddItem(sprite, description, itemName))
            {
                Inventory.SaveInventory(filePath);
            }
        }
        if (transform.parent != null)
        {
            Destroy(transform.parent.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
