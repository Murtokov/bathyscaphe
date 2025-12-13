using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float lifeTime = 2f;
    public float explosionRadius = 5f;
    public float explosionDamage = 5f;
    public float explosionForce = 1000f;

    private LayerMask submarineLayerMask;

    void Start()
    {
        submarineLayerMask = 1 << LayerMask.NameToLayer("Submarine");

        DamageInRadius();

        Destroy(gameObject, lifeTime);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    private void DamageInRadius()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, submarineLayerMask);
        foreach (Collider2D collider in hitColliders)
        {
            SubmarineLife health = collider.GetComponent<SubmarineLife>();

            Debug.Log(explosionDamage);
            health.Damage(explosionDamage);

            Vector3 explosionDirection = (collider.transform.position - transform.position).normalized;

            Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
            rb.AddForce(explosionDirection * explosionForce, ForceMode2D.Impulse);
        }
    }
}