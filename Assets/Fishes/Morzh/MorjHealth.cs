using UnityEngine;

public class MorjHealth : FishHealth
{
    public GameObject explosionPrefab;
    public GameObject portalPrefab;
    public Transform roof;
    private float deathTime;

    private Rigidbody2D rb;
    private int roofLayer;
    private bool shouldExplode = false;

    protected void Start()
    {
        Level2Ocean level2Ocean = SavesManager.LoadConfig<Level2Ocean>("Level2Ocean");
        if (level2Ocean.isWalrusDefeated)
        {
            portalPrefab.transform.position = level2Ocean.doorPosition;
            portalPrefab.SetActive(true);
        }
        else
        {
            portalPrefab.SetActive(false);
        }

        rb = GetComponent<Rigidbody2D>();
        roofLayer = LayerMask.NameToLayer("Explosive roof");
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (shouldExplode && (deathTime + 1 < Time.time))
        {
            Instantiate(explosionPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else if (collision.gameObject.layer == roofLayer)
        {
            Instantiate(explosionPrefab, transform.position, transform.rotation);
            Level2Ocean level2Ocean = SavesManager.LoadConfig<Level2Ocean>("Level2Ocean");
            if (!level2Ocean.isWalrusDefeated)
            {
                Vector2 position = new Vector2(transform.position.x, roof.position.y);
                // Instantiate(portalPrefab, position, Quaternion.identity);
                portalPrefab.transform.position = position;
                level2Ocean.doorPosition = position;
                level2Ocean.isWalrusDefeated = true;
                SavesManager.SaveConfig<Level2Ocean>(level2Ocean, "Level2Ocean");
                portalPrefab.SetActive(true);
            }

            Destroy(gameObject);
        }
    }

    protected override void _Die()
    {
        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();
        foreach (var script in scripts)
        {
            if (script != this && script.enabled)
                script.enabled = false;
        }

        if (transform.localScale.y > 0)
        {
            transform.localScale = new Vector2(transform.localScale.x, -transform.localScale.y);
        }

        transform.rotation = Quaternion.Euler(0, 0, 0);

        rb.gravityScale = -1;
        shouldExplode = true;

        deathTime = Time.time;
    }
}