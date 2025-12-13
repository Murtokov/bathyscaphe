using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SubmarineLife : MonoBehaviour
{
    public float health = 100;
    public int maxHealth = 100;
    private float damageMultiplier = 1f;

    public void Start()
    {
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        SubmarineConfig submarineConfig = SavesManager.LoadConfig<SubmarineConfig>("SubmarineConfig");
        health = submarineConfig.health;
        maxHealth = submarineConfig.maxHealth;
        UIManager uIManager = GameObject.FindAnyObjectByType<UIManager>();
        uIManager.UpdateHealthUI((int)Math.Ceiling(health), maxHealth);
    }

    private void UpdateConfig()
    {
        SubmarineConfig submarineConfig = SavesManager.LoadConfig<SubmarineConfig>("SubmarineConfig");
        submarineConfig.health = (int)Math.Ceiling(health);
        submarineConfig.maxHealth = maxHealth;
        SavesManager.SaveConfig<SubmarineConfig>(submarineConfig, "SubmarineConfig");
    }

    public void Damage(float damage)
    {
        Debug.Log(damageMultiplier);
        health -= damage * damageMultiplier;
        UpdateConfig();
        UIManager uIManager = GameObject.FindAnyObjectByType<UIManager>();
        uIManager.UpdateHealthUI((int)Math.Ceiling(health), maxHealth);
        if (health <= 0)
        {
            health = 0;
            _Die();
        }
    }

    private void _Die()
    {
        Level1Ocean level1OceanInit = SavesManager.LoadConfig<Level1Ocean>("Level1OceanInitial");
        Level1Ocean level1Ocean = SavesManager.LoadConfig<Level1Ocean>("Level1Ocean");
        level1Ocean.lastPosition = level1OceanInit.lastPosition;
        Level2Ocean level2OceanInit = SavesManager.LoadConfig<Level2Ocean>("Level2OceanInitial");
        Level2Ocean level2Ocean = SavesManager.LoadConfig<Level2Ocean>("Level2Ocean");
        level2Ocean.lastPosition = level2OceanInit.lastPosition;
        SubmarineConfig submarineConfig = SavesManager.LoadConfig<SubmarineConfig>("SubmarineConfig");
        submarineConfig.health = submarineConfig.maxHealth;
        SavesManager.SaveConfig<Level1Ocean>(level1Ocean, "Level1Ocean");
        SavesManager.SaveConfig<Level2Ocean>(level2Ocean, "Level2Ocean");
        SavesManager.SaveConfig<SubmarineConfig>(submarineConfig, "SubmarineConfig");
        SceneManager.LoadScene("Level1MainBase");
    }

    public void SetDamageMultiplier(float multiplier)
    {
        damageMultiplier = multiplier;
    }

    public void ResetDamageMultiplier()
    {
        damageMultiplier = 1f;
    }

    public void SetTemporaryDamageMultiplier(float multiplier, float duration)
    {
        SetDamageMultiplier(multiplier);
        Invoke(nameof(ResetDamageMultiplier), duration);
    }
}