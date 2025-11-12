using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool isEmpty = true, isClicked = false;
    public GameObject iconGameObject;
    public TMP_Text itemDecsription;
    public string description;
    public string itemName;
    public void Awake()
    {
        iconGameObject = transform.GetChild(0).gameObject;
        itemDecsription = transform.GetChild(1).GetComponent<TMP_Text>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        itemDecsription.text = description;
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        itemDecsription.text = "";
    }
    public void SetIcon(Sprite icon)
    {
        Image image = iconGameObject.GetComponent<Image>();
        image.color = new Color(1, 1, 1, 1);
        image.sprite = icon;
        image.preserveAspect = true;
        image.type = Image.Type.Simple;
    }
    public void NullifyData()
    {
        isEmpty = true;
        iconGameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        iconGameObject.GetComponent<Image>().sprite = null;
        itemDecsription.text = "";
        description = "";
        itemName = "";
        isClicked = false;
    }
}
