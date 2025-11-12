using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EscapeMenu : MonoBehaviour
{
    [SerializeField] private GameObject escapeMenu;
    [SerializeField] private GameObject settingsMenu;
    private bool isPaused = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    public void PauseGame()
    {
        isPaused = true;
        escapeMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
        isPaused = false;
        escapeMenu.SetActive(false);
        settingsMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void BackToMenu()
    {
        // ItemsLoader.Instance.SaveProgress(true);
        Time.timeScale = 1f;
        SaveGame();
        SceneManager.LoadScene("MainMenu");
    }
    public void SaveGame()
    {
        SavesManager.SaveProgress();
    }
}
