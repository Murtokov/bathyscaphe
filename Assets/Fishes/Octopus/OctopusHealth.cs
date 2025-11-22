using UnityEngine;

public class OctopusHealth : MonoBehaviour
{
    public float health = 100;

    public void Damage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            _Die();
        }
    }

    private void _Die()
    {
    }
}
