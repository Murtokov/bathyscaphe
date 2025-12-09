using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level1Door : MonoBehaviour
{
    public bool inTrigger = false;
    public GameObject contextHint;
    public TMP_Text txt;
    public void Start()
    {
        Level1Ocean level1Ocean = SavesManager.LoadConfig<Level1Ocean>("Level1Ocean");
        if (level1Ocean.isDoorToLevel2Opened)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }
        contextHint.SetActive(false);
        txt = contextHint.GetComponent<TMP_Text>();
    }
    public void Update()
    {
        if (inTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Level1Ocean level1Ocean = SavesManager.LoadConfig<Level1Ocean>("Level1Ocean");
                if (level1Ocean.isDoorToLevel2Opened)
                {
                    SaveLastScene("Level2Ocean");
                    SaveLastPosition();
                    SceneManager.LoadScene("Level2Ocean");
                }
                else
                {
                    InventoryManager Inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryManager>();
                    if (Inventory.DeleteItem("golden key"))
                    {
                        level1Ocean.isDoorToLevel2Opened = true;
                        SavesManager.SaveConfig<Level1Ocean>(level1Ocean, "Level1Ocean");
                        OpenDoor();
                    }
                    else
                    {
                        txt.text = "Keys Required";
                    }
                }
            }
        }
    }
    private void OpenDoor()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
    }
    private void CloseDoor()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(false);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Submarine"))
        {
            txt.text = "[E]";
            contextHint.SetActive(true);
            inTrigger = true;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Submarine"))
        {
            inTrigger = false;
            if (contextHint != null)
            {
                contextHint.SetActive(false);    
            }
        }
    }
    public void SaveLastScene(string sceneName)
    {
        MainConfig mainConfig = SavesManager.LoadConfig<MainConfig>("MainConfig");
        mainConfig.lastScene = sceneName;
        SavesManager.SaveConfig<MainConfig>(mainConfig, "MainConfig");
    }
    public void SaveLastPosition()
    {
        if (SceneManager.GetActiveScene().name == "Level1Ocean")
        {
            Level1Ocean level1Ocean = SavesManager.LoadConfig<Level1Ocean>("Level1Ocean");
            GameObject submarine = GameObject.FindGameObjectWithTag("Submarine");
            level1Ocean.lastPosition = submarine.transform.position;
            SavesManager.SaveConfig<Level1Ocean>(level1Ocean, "Level1Ocean");
        }
    }
}
