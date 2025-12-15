using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource soundSource;
    [SerializeField] private Slider slider;
    [SerializeField] private Sprite soundOn;
    [SerializeField] private Sprite soundOff;
    [SerializeField] private GameObject soundButton;
    private float previousVolume = 1;
    void Awake()
    {
        MainConfig mainConfig = SavesManager.LoadConfig<MainConfig>("MainConfig");
        slider.value = mainConfig.soundVolume;
    }
    // void Start()
    // {
    //     UpdateValue();   
    // }
    public void UpdateValue()
    {
        soundSource.volume = slider.value;
        if (slider.value > 0)
        {
            previousVolume = slider.value;
        }
        if (soundSource.volume == 0) 
        {
            soundButton.GetComponent<Image>().sprite = soundOff;
        }
        else
        {
            soundButton.GetComponent<Image>().sprite = soundOn;
        }
    }
    public void EnableSound()
    {
        if (soundSource.volume == 0) {
            soundSource.volume = previousVolume;
            slider.value = previousVolume;
        }
        else
        {
            soundSource.volume = 0;
            slider.value = 0;
        }
    }
    public void SaveVolume()
    {
        MainConfig mainConfig = SavesManager.LoadConfig<MainConfig>("MainConfig");
        mainConfig.soundVolume = soundSource.volume;
        SavesManager.SaveConfig<MainConfig>(mainConfig, "MainConfig");
    }
}
