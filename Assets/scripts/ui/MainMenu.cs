using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject continueButton;
    private void Awake()
    {
        MainConfig mainConfig = SavesManager.LoadConfig<MainConfig>("MainConfig");
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
        SceneManager.LoadScene("Level1MainBase");
    }
    public void NewGame()
    {
        MainConfig mainConfig = SavesManager.LoadConfig<MainConfig>("MainConfig");
        mainConfig.isGameStarted = true;
        SavesManager.SaveConfig<MainConfig>(mainConfig, "MainConfig");
        string savePath = Application.persistentDataPath;
        string[] initialFiles = Directory.GetFiles(savePath, "*Initial.json");
        foreach (string initialFile in initialFiles)
        {
            string baseFile = Path.GetFileName(initialFile).Replace("Initial", "");
            string targetFile = Path.Combine(savePath, baseFile);
            File.Copy(initialFile, targetFile, true);
        }
        
        Debug.Log("🎮 Инициализация сохранений завершена!");
        SceneManager.LoadScene("Level1MainBase");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void CopyJson(string sourcePath, string destinationPath)
    {
        if (File.Exists(sourcePath))
        {
            File.Copy(sourcePath, destinationPath, overwrite: true);
        }
    }
}
