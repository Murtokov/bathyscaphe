using UnityEngine;
using UnityEngine.Rendering;

public class SubmarineRam : MonoBehaviour
{
    public float dashForce = 30f;
    public float dashCooldown = 1f;
    public float resistanceDuration = 0.5f;
    public float resistanceMultiplier = 0.5f;

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
        submarineLife.SetTemporaryDamageMultiplier(resistanceMultiplier, resistanceDuration);
        submarineMoving.Dash(dashForce);
        dashable = false;
        previousDashTime = Time.time;
    }
}
