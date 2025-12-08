using UnityEngine;
using UnityEngine.UIElements;

public class StasisGun : MonoBehaviour
{
    public float shootForce = 1000f;
    public GameObject projectilePrefab;
    public Transform shootPosition;
    public float fullChargeTime = 5f;
    public float partialChargeTime = 2f;
    public float maxProjectileSize = 3f;

    private GameObject projectile;
    private Rigidbody2D rbProjectile;
    private bool isPrepairing = false;
    private float startChargeTime;
    private SpriteRenderer submarineSprite;
    private void Start()
    {
        submarineSprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PrepareCharge();
        }
        else if (Input.GetMouseButtonUp(0) && isPrepairing)
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        Vector3 position = shootPosition.localPosition;
        position.x = Mathf.Abs(position.x) * (submarineSprite.flipX ? -1 : 1);
        shootPosition.localPosition = position;

        if (isPrepairing)
        {
            float percents = (Time.time - startChargeTime) / fullChargeTime;
            float scale = Mathf.Lerp(1f, maxProjectileSize, percents);
            projectile.transform.localScale = Vector2.one * scale;
        }
    }

    private void PrepareCharge()
    {
        isPrepairing = true;
        startChargeTime = Time.time;
        projectile = Instantiate(projectilePrefab, shootPosition.position, Quaternion.identity);
        rbProjectile = projectile.GetComponent<Rigidbody2D>();
        rbProjectile.simulated = false;
        projectile.transform.SetParent(shootPosition);
    }

    private void Shoot()
    {
        isPrepairing = false;
        if (Time.time < startChargeTime + partialChargeTime)
        {
            Destroy(projectile);
            return;
        }

        Camera cam = GameObject.FindWithTag("SubmarineCamera").GetComponent<Camera>();
        Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = ((Vector3)mousePosition - shootPosition.position).normalized;
        rbProjectile.simulated = true;

        projectile.transform.SetParent(null);

        rbProjectile.AddForce(shootForce * direction, ForceMode2D.Impulse);
    }
}
