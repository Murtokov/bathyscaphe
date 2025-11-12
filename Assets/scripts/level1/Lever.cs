using Microsoft.Unity.VisualStudio.Editor;
using UnityEditor.Rendering.Universal;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public Sprite spriteOn;
    public Sprite spriteOff;
    private bool inTrigger = false, switched = false;
    public GameObject contextHint;
    void Start()
    {
        Level1MainBase level1MainBase = new Level1MainBase();
        SavesManager.SaveConfig<Level1MainBase>(level1MainBase, "Level1MainBase");
        Level1ParkourRoom level1ParkourRoom = new Level1ParkourRoom();
        SavesManager.SaveConfig<Level1ParkourRoom>(level1ParkourRoom, "Level1ParkourRoom");
        Level1UpperBaseLeftSide level1UpperBaseLeftSide= new Level1UpperBaseLeftSide();
        SavesManager.SaveConfig<Level1UpperBaseLeftSide>(level1UpperBaseLeftSide, "Level1UpperBaseLeftSide");
        Level1UpperBaseRightSide level1UpperBaseRightSide= new Level1UpperBaseRightSide();
        SavesManager.SaveConfig<Level1UpperBaseRightSide>(level1UpperBaseRightSide, "Level1UpperBaseRightSide");
        contextHint = transform.GetChild(0).gameObject;
        contextHint.SetActive(false);
        if (switched)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = spriteOff;
        }
        if (!switched)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = spriteOn;
        }
    }
    public void Update()
    {
        if (inTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!switched)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = spriteOff;
                    switched = true;
                    GameObject lever = GameObject.FindGameObjectWithTag("Light");
                    lever.transform.GetChild(0).gameObject.SetActive(true);
                }
                else
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = spriteOn;
                    switched = false;
                    GameObject lever = GameObject.FindGameObjectWithTag("Light");
                    lever.transform.GetChild(0).gameObject.SetActive(false);
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
