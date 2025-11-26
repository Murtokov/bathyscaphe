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
        submarineMoving = GetComponent<SubmarineMoving>();
        submarineLife = GetComponent<SubmarineLife>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && dashable)
        {
            Dash();
        }

        if (previousDashTime + dashCooldown < Time.time)
        {
            dashable = true;
        }
    }

    private void Dash()
    {
        Debug.Log(1);
        submarineLife.SetTemporaryDamageMultiplier(resistanceMultiplier, ramDuration);
        submarineMoving.Dash(dashForce);
        dashable = false;
        previousDashTime = Time.time;
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
                fishHealth.Damage(damage);
            }
        }
    }
}
