using UnityEngine;

public class FishHealth : MonoBehaviour
{
    public float health = 100;

    public virtual void Damage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            Die();
        }
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }
}
