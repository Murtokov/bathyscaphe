using UnityEngine;

public class MorjHealth : FishHealth
{
    public GameObject explosionPrefab;
    public GameObject portalPrefab;
    public Transform roof;

    private Rigidbody2D rb;
    private bool shouldExplode = false;
    private int roofLayer;

    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        roofLayer = LayerMask.NameToLayer("Explosive roof");
    }

    protected override void _Die()
    {
        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();
        foreach (var script in scripts)
        {
            if (script != this && script.enabled)
                script.enabled = false;
        }

        rb.linearVelocity = Vector2.zero;   

        if (transform.localScale.y > 0)
        {
            transform.localScale = new Vector2(transform.localScale.x, -transform.localScale.y);
        }
        transform.rotation = Quaternion.Euler(0, 0, 0);

        rb.gravityScale = -1;
        shouldExplode = true;
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (shouldExplode)
        {
            Instantiate(explosionPrefab, transform.position, transform.rotation);
            if (collision.gameObject.layer == roofLayer)
            {
                Vector2 position = new Vector2(transform.position.x, roof.position.y);
                Instantiate(portalPrefab, position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
