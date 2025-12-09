using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject continueButton;
    private void Awake()
    {
        MainConfig mainConfig = SavesManager.LoadConfig<MainConfig>("MainConfig");
        if (mainConfig == null || true)
        {
            Level1MainBase level1MainBase = new Level1MainBase();
            Level1Ocean level1Ocean = new Level1Ocean();
            Level1ParkourRoom level1ParkourRoom = new Level1ParkourRoom();
            Level1UpperBaseLeftSide level1UpperBaseLeftSide = new Level1UpperBaseLeftSide();
            Level1UpperBaseRightSide level1UpperBaseRightSide = new Level1UpperBaseRightSide();
            Level2Ocean level2Ocean = new Level2Ocean();
            Level3Ocean level3Ocean = new Level3Ocean();
            MainConfig mainConfig1 = new MainConfig();
            SubmarineConfig submarineConfig = new SubmarineConfig();
            // orig
            SavesManager.SaveConfig<Level1MainBase>(level1MainBase, "Level1MainBase");
            SavesManager.SaveConfig<Level1Ocean>(level1Ocean, "Level1Ocean");
            SavesManager.SaveConfig<Level1ParkourRoom>(level1ParkourRoom, "Level1ParkourRoom");
            SavesManager.SaveConfig<Level1UpperBaseLeftSide>(level1UpperBaseLeftSide, "Level1UpperBaseLeftSide");
            SavesManager.SaveConfig<Level1UpperBaseRightSide>(level1UpperBaseRightSide, "Level1UpperBaseRightSide");
            SavesManager.SaveConfig<Level2Ocean>(level2Ocean, "Level2Ocean");
            SavesManager.SaveConfig<Level3Ocean>(level3Ocean, "Level3Ocean");
            SavesManager.SaveConfig<MainConfig>(mainConfig1, "MainConfig");
            SavesManager.SaveConfig<SubmarineConfig>(submarineConfig, "SubmarineConfig");
            InventoryManager Inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryManager>();
            string filePath = Path.Combine(Application.persistentDataPath, "Inventory.json");
            Inventory.SaveInventory(filePath);
            // initial
            SavesManager.SaveConfig<Level1MainBase>(level1MainBase, "Level1MainBaseInitial");
            SavesManager.SaveConfig<Level1Ocean>(level1Ocean, "Level1OceanInitial");
            SavesManager.SaveConfig<Level1ParkourRoom>(level1ParkourRoom, "Level1ParkourRoomInitial");
            SavesManager.SaveConfig<Level1UpperBaseLeftSide>(level1UpperBaseLeftSide, "Level1UpperBaseLeftSideInitial");
            SavesManager.SaveConfig<Level1UpperBaseRightSide>(level1UpperBaseRightSide, "Level1UpperBaseRightSideInitial");
            SavesManager.SaveConfig<Level2Ocean>(level2Ocean, "Level2OceanInitial");
            SavesManager.SaveConfig<Level3Ocean>(level3Ocean, "Level3OceanInitial");
            SavesManager.SaveConfig<MainConfig>(mainConfig1, "MainConfigInitial");
            SavesManager.SaveConfig<SubmarineConfig>(submarineConfig, "SubmarineConfigInitial");
            filePath = Path.Combine(Application.persistentDataPath, "InventoryInitial.json");
            Inventory.SaveInventory(filePath);
        }
        mainConfig = SavesManager.LoadConfig<MainConfig>("MainConfig");
        continueButton = transform.GetChild(0).gameObject;
        if (mainConfig.isGameStarted)
        {
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }
    }
    public void ContinueGame()
    {
        MainConfig mainConfig = SavesManager.LoadConfig<MainConfig>("MainConfig");
        SceneManager.LoadScene(mainConfig.lastScene);
    }
    public void NewGame()
    {
        string savePath = Application.persistentDataPath;
        string[] initialFiles = Directory.GetFiles(savePath, "*Initial.json");
        foreach (string initialFile in initialFiles)
        {
            string baseFile = Path.GetFileName(initialFile).Replace("Initial", "");
            string targetFile = Path.Combine(savePath, baseFile);
            File.Copy(initialFile, targetFile, true);
        }
        
        Debug.Log("🎮 Инициализация сохранений завершена!");
        MainConfig mainConfig = SavesManager.LoadConfig<MainConfig>("MainConfig");
        mainConfig.isGameStarted = true;
        SavesManager.SaveConfig<MainConfig>(mainConfig, "MainConfig");
        SceneManager.LoadScene("Level1MainBase");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
