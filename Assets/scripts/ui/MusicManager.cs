using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource BackgroundMusic;
    [SerializeField] private Slider slider;
    [SerializeField] private Sprite musicOn;
    [SerializeField] private Sprite musicOff;
    [SerializeField] private GameObject musicButton;
    private float previousVolume = 1f;
    void Awake()
    {
        MainConfig mainConfig = SavesManager.LoadConfig<MainConfig>("MainConfig");
        slider.value = mainConfig.musicVolume;
    }
    void Start()
    {
        UpdateValue();   
    }
    public void UpdateValue()
    {
        BackgroundMusic.volume = slider.value;
        if (slider.value > 0)
        {
            previousVolume = slider.value;
        }
        if (BackgroundMusic.volume == 0)
        {
            musicButton.GetComponent<Image>().sprite = musicOff;
        }
        else
        {
            musicButton.GetComponent<Image>().sprite = musicOn;
        }
    }
    public void EnableMusic()
    {
        if (BackgroundMusic.volume == 0) {
            BackgroundMusic.volume = previousVolume;
            slider.value = previousVolume;
        }
        else
        {
            BackgroundMusic.volume = 0;
            slider.value = 0;
        }
    }
    public void SaveVolume()
    {
        MainConfig mainConfig = SavesManager.LoadConfig<MainConfig>("MainConfig");
        mainConfig.musicVolume = BackgroundMusic.volume;
        SavesManager.SaveConfig<MainConfig>(mainConfig, "MainConfig");
    }
}
