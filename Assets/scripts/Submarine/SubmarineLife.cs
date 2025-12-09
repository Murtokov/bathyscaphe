using UnityEngine;

public class SubmarineLife : MonoBehaviour
{
    public float health = 100;
    private float damageMultiplier = 1f;

    public void Start()
    {
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        SubmarineConfig submarineConfig = SavesManager.LoadConfig<SubmarineConfig>("SubmarineConfig");
        health = submarineConfig.health;
    }

    public void Damage(float damage)
    {
        Debug.Log(damageMultiplier);
        health -= damage * damageMultiplier;
        if (health <= 0)
        {
            health = 0;
            _Die();
        }
    }

    private void _Die()
    {
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