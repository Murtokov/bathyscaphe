using UnityEngine;
using UnityEngine.Rendering;

public class SubmarineRam : MonoBehaviour
{
    public float dashForce = 30f;
    public float dashCooldown = 1f;
    public float ramDuration = 0.5f;
    public float resistanceMultiplier = 0.5f;
    public float damage = 10f;
    public float ramDamage = 20f;

    private SubmarineMoving submarineMoving;
    private SubmarineLife submarineLife;
    private bool dashable = true;
    private float previousDashTime;
    void Start()
    {
        UpdateRam();
        submarineMoving = GetComponent<SubmarineMoving>();
        submarineLife = GetComponent<SubmarineLife>();
    }

    public void UpdateRam()
    {
        SubmarineConfig submarineConfig = SavesManager.LoadConfig<SubmarineConfig>("SubmarineConfig");
        if (submarineConfig.ramEquipped)
        {
            damage = 40f;
            ramDamage = 110f;
        }
    }

    void Update()
    {
        if (submarineMoving.isActiveAndEnabled)
        {
            if (Input.GetKeyDown(KeyCode.Space) && dashable)
            {
<<<<<<< Updated upstream
=======
                Debug.Log("Ram");
>>>>>>> Stashed changes
                Dash();
            }

            if (previousDashTime + dashCooldown < Time.time)
            {
                dashable = true;
            }
        }
    }

    private void Dash()
    {
        if (submarineMoving.isActiveAndEnabled)
        {
            submarineLife.SetTemporaryDamageMultiplier(resistanceMultiplier, ramDuration);
            submarineMoving.Dash(dashForce);
            dashable = false;
            previousDashTime = Time.time;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        FishHealth fishHealth = collision.gameObject.GetComponent<FishHealth>();
        if (fishHealth != null)
        {
            if (previousDashTime + ramDuration > Time.time) 
            {
                fishHealth.Damage(ramDamage);
            }
            else
            {
                Debug.Log(Time.time);
                fishHealth.Damage(damage);
            }
        }
    }
}
