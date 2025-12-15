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

<<<<<<< Updated upstream
<<<<<<<< Updated upstream:Assets/Fishes/FishHealth.cs
    protected virtual void _Die()
========
<<<<<<< Updated upstream:Assets/Submarine/SubmarineLife.cs
    private void _Die()
=======
    public virtual void Die()
>>>>>>> Stashed changes:Assets/Fishes/FishHealth.cs
>>>>>>>> Stashed changes:Assets/Submarine/SubmarineLife.cs
=======
    public virtual void Die()
>>>>>>> Stashed changes
    {
        Destroy(gameObject);
    }
}
