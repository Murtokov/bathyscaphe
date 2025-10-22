using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
                slots[slots.Count - 1].id = i;
            }
        }
    }
    public bool AddItem(Sprite icon, string decsription, string itemName)
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.isEmpty == true)
            {
                slot.decsription = decsription;
                slot.isEmpty = false;
                slot.itemName = itemName;
                slot.SetIcon(icon);
                return true;
            }
        }
        return false;
    }
}
