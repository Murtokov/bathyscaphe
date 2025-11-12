using System.Collections.Generic;

[System.Serializable]
public class SlotData
{
    public string itemName;
    public bool isEmpty;
    public string description;
    public string spriteName;
}

[System.Serializable]
public class InventoryData
{
    public List<SlotData> slots;
    public InventoryData()
    {
        slots = new List<SlotData>();
    }
}