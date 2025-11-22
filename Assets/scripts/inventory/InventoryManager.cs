using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public List<InventorySlot> slots;
    Transform inventoryPanel;
    public void Awake()
    {
        inventoryPanel = transform.GetChild(0).transform;
        for (int i = 0; i < inventoryPanel.childCount; i++)
        {
            if (inventoryPanel.GetChild(i).GetComponent<InventorySlot>() != null)
            {
                slots.Add(inventoryPanel.GetChild(i).GetComponent<InventorySlot>());
            }
        }
    }
    public void Start()
    {
        string filePath = Path.Combine(Application.persistentDataPath + "/Inventory.json");
        LoadInventory(filePath);
        // SaveInventory(filePath);
        // filePath = Path.Combine(Application.persistentDataPath + "/InventoryInitial.json");
        // SaveInventory(filePath);
    }
    public bool AddItem(Sprite icon, string decsription, string itemName)
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.isEmpty == true)
            {
                slot.description = decsription;
                slot.isEmpty = false;
                slot.itemName = itemName;
                slot.SetIcon(icon);
                string filePath = Path.Combine(Application.persistentDataPath + "/Inventory.json");
                SaveInventory(filePath);
                return true;
            }
        }
        return false;
    }
    public bool CheckItem(string itemName)
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.isEmpty == false && slot.itemName == itemName)
            {
                return true;
            }
        }
        return false;
    }
    public bool DeleteItem(string itemName)
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.isEmpty == false && slot.itemName == itemName)
            {
                slot.NullifyData();
                string filePath = Path.Combine(Application.persistentDataPath + "/Inventory.json");
                SaveInventory(filePath);
                return true;
            }
        }
        return false;
    }
    public void SaveInventory(string filePath)
    {
        InventoryData inventoryData = new InventoryData();
        foreach (InventorySlot slot in slots)
        {
            SlotData slotData = new()
            {
                description = slot.description,
                itemName = slot.itemName,
                isEmpty = slot.isEmpty,
                spriteName = slot.isEmpty ? null : slot.iconGameObject.GetComponent<Image>().sprite.name,
            };
            inventoryData.slots.Add(slotData);
        }
        string json = JsonUtility.ToJson(inventoryData);
        File.WriteAllText(filePath, json);
        Debug.Log("Inventory saved to: " + filePath);
    }
    public void LoadInventory(string filePath)
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            InventoryData inventoryData = JsonUtility.FromJson<InventoryData>(json);
            for (int i = 0; i < inventoryData.slots.Count; i++)
            {
                InventorySlot slot = slots[i];
                SlotData slotData = inventoryData.slots[i];
                Debug.Log(slotData.spriteName);
                if (!slotData.isEmpty)
                {
                    slot.isEmpty = slotData.isEmpty;
                    slot.isClicked = false;
                    slot.description = slotData.description;
                    slot.itemName = slotData.itemName;
                    string spriteName = slotData.spriteName;
                    Sprite sprite = Resources.Load<Sprite>(spriteName.Substring(0, spriteName.Length - 2));
                    slot.SetIcon(sprite);
                    if (sprite != null)
                    {
                        Debug.Log("✅ Спрайт загружен: " + sprite.name);
                    }
                    else
                    {
                        Debug.LogError("❌ Не удалось загрузить спрайт!");
                    }
                }
                else
                {
                    slot.NullifyData();
                }
            }
            Debug.Log("Inventory loaded from: " + filePath);
        }
        else
        {
            Debug.Log("Save file not found at: " + filePath);
        }
    }
}
