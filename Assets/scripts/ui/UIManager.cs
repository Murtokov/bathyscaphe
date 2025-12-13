using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject stasisGunIcon, balloonIcon, ramShieldIcon, speedText, speedVertText, hpText;
    public TMP_Text textSpeed, textVertSpeed, textHP;

    private void Awake()
    {
        textSpeed = speedText.GetComponent<TMP_Text>();
        textVertSpeed = speedVertText.GetComponent<TMP_Text>();
        textHP = hpText.GetComponent<TMP_Text>();
    }

    private void Start()
    {
        SubmarineConfig submarineConfig = SavesManager.LoadConfig<SubmarineConfig>("SubmarineConfig");
        balloonIcon.SetActive(submarineConfig.balloonEquipped);
        stasisGunIcon.SetActive(submarineConfig.stasisGunEquipped);
        ramShieldIcon.SetActive(submarineConfig.ramEquipped);
    }

    public void UpdateHealthUI(int health, int maxHealth)
    {
        textHP.text = health + "/" + maxHealth;
    }
    
    public void UpdateSpeedUI(float speedX, float speedY)
    {
        textSpeed.text = Math.Round(speedX, 1).ToString();
        textVertSpeed.text = Math.Round(speedY, 1).ToString();
    }

    public void UpdateRamUI(bool flag)
    {
        ramShieldIcon.SetActive(flag);
    }

    public void UpdateStasisGunUI(bool flag)
    {
        stasisGunIcon.SetActive(flag);
    }

    public void UpdateBalloonUI(bool flag)
    {
        balloonIcon.SetActive(flag);
    }
}
