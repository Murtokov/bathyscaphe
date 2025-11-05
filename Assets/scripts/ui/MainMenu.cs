using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // [SerializeField] private GameObject continueButton;
    // private void Awake()
    // {
    //     continueButton = transform.GetChild(0).gameObject;
    //     ConfigData configData = ConfigManager.LoadConfig();
    //     if (configData.hasGuideShowed)
    //     {
    //         continueButton.SetActive(true);
    //     }
    //     else
    //     {
    //         continueButton.SetActive(false);
    //     }
    // }
    public void ContinueGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    void EnsureFileExists(string filePath, string defaultContent = "{}")
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, defaultContent);
        }
    }
    public void NewGame()
    {
        SceneManager.LoadScene("SampleScene");
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
