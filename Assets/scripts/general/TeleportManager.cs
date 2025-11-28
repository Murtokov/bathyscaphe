using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportManager : MonoBehaviour
{
    public bool inTrigger = false;
    public GameObject contextHint;
    public void Start()
    {
        if (!(contextHint == null))
        {
            contextHint.SetActive(false);
        }
    }
    public void Update()
    {
        if (inTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (gameObject.name == "Exit")
                {
                    InventoryManager Inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryManager>();
                    Level1Ocean level1Ocean = SavesManager.LoadConfig<Level1Ocean>("Level1Ocean");
                    if (level1Ocean.isDoorToLevel2Opened)
                    {
                        SaveLastScene("Level2Ocean");
                        SaveLastPosition();
                        SceneManager.LoadScene("Level2Ocean");
                    }
                    else
                    {
                        if (Inventory.DeleteItem("golden key"))
                        {
                            level1Ocean.isDoorToLevel2Opened = true;
                            SavesManager.SaveConfig<Level1Ocean>(level1Ocean, "Level1Ocean");
                            SaveLastScene("Level2Ocean");
                            SaveLastPosition();
                            SceneManager.LoadScene("Level2Ocean");
                        }
                        else
                        {
                            Debug.Log("WTF");
                        }
                    }
                }
                else
                {
                    SaveLastScene(gameObject.name);
                    if (SceneManager.GetActiveScene().name != "Level1MainBase")
                    {
                        SaveLastPosition();
                    }
                    SceneManager.LoadScene(gameObject.name);
                }
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Submarine"))
        {
            if (gameObject.name == "Exit")
            {
                InventoryManager Inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryManager>();
                Level1Ocean level1Ocean = SavesManager.LoadConfig<Level1Ocean>("Level1Ocean");
                if (Inventory.CheckItem("golden key") || level1Ocean.isDoorToLevel2Opened)
                {
                    contextHint.SetActive(true);
                    inTrigger = true;
                }
            }
            else
            {
                if (SceneManager.GetActiveScene().name == "Level1OctopusArena")
                {
                    SaveLastScene(gameObject.name);
                    SaveLastPosition();
                    SceneManager.LoadScene(gameObject.name);
                }
                else
                {
                    inTrigger = true;
                    contextHint.SetActive(true);
                }
            }
        }
        if (collision.CompareTag("Player") && SceneManager.GetActiveScene().name != "Level1Ocean")
        {
            if (SceneManager.GetActiveScene().name == "Level1MainBase")
            {
                inTrigger = true;
                contextHint.SetActive(true);
            }
            else
            {
                SaveLastScene(gameObject.name);
                SceneManager.LoadScene(gameObject.name);
            }
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
        if (collision.CompareTag("Player"))
        {
            if (SceneManager.GetActiveScene().name == "Level1MainBase")
            {
                inTrigger = false;
                if (contextHint != null)
                {
                    contextHint.SetActive(false);
                }
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
        SubmarineConfig submarineConfig = SavesManager.LoadConfig<SubmarineConfig>("SubmarineConfig");
        GameObject submarine = GameObject.FindGameObjectWithTag("Submarine");
        submarineConfig.lastPosition = submarine.transform.position;
        SavesManager.SaveConfig<SubmarineConfig>(submarineConfig, "SubmarineConfig");
    }
}
