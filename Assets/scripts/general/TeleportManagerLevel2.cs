using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportManagerLevel2 : MonoBehaviour
{
    public bool inTrigger = false;
    public GameObject contextHint;
    public void Start()
    {
        contextHint.SetActive(false);
    }
    public void Update()
    {
        if (inTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                SaveLastScene(gameObject.name);
                SceneManager.LoadScene(gameObject.name);
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Submarine"))
        {
            inTrigger = true;
            contextHint.SetActive(true);
            SaveLastPosition();
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Submarine"))
        {
            inTrigger = false;
            contextHint.SetActive(false);
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
