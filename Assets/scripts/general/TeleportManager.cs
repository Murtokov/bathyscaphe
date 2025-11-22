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
                SaveLastScene(gameObject.name);
                if (SceneManager.GetActiveScene().name != "Level1MainBase")
                {
                    SaveLastPosition();
                }
                SceneManager.LoadScene(gameObject.name);
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
                if (Inventory.CheckItem("golden key"))
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
        if (collision.CompareTag("Player"))
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
