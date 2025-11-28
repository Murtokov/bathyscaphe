using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SubmarineMoving : MonoBehaviour
{
    public float walkSpeed;
    public float jumpSpeed;

    public float maxSpeedX;
    public float maxSpeedY;
    private ParticleSystem ps;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Level1Ocean")
        {
            SubmarineConfig submarineConfig = SavesManager.LoadConfig<SubmarineConfig>("SubmarineConfig");
            transform.position = submarineConfig.lastPosition;
        }
        if (SceneManager.GetActiveScene().name == "Level2Ocean")
        {
            SubmarineConfig submarineConfig = SavesManager.LoadConfig<SubmarineConfig>("SubmarineConfig");
            transform.position = submarineConfig.lastPosition;
        }
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        ps = GetComponent<ParticleSystem>();
    }

    void Update()
    {
    }

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        rb.AddForce(new Vector2(
            horizontalInput * walkSpeed,
            verticalInput * jumpSpeed));

        var vel = ps.velocityOverLifetime;
        vel.enabled = true;
        vel.x = -rb.linearVelocity.x / 5;
        vel.y = -rb.linearVelocity.y / 5;

        Vector2 velocity = rb.linearVelocity;
        velocity.x = Mathf.Clamp(velocity.x, -maxSpeedX, maxSpeedX);
        velocity.y = Mathf.Clamp(velocity.y, -maxSpeedY, maxSpeedY);
        rb.linearVelocity = velocity;


        if (horizontalInput > 0.01f)
            sr.flipX = false;
        else if (horizontalInput < -0.01f)
            sr.flipX = true;

        var shape = ps.shape;
        Vector3 shapePos = shape.position;
        shapePos.x = Mathf.Abs(shapePos.x) * (sr.flipX ? 1 : -1);
        shape.position = shapePos;
    }

    public void Dash(float force)
    {
        float directionX = Input.GetAxis("Horizontal");
        float directionY = Input.GetAxis("Vertical");

        if (Mathf.Abs(directionX) < 0.01f && Mathf.Abs(directionY) < 0.01f)
        {
            Vector2 velocity = rb.linearVelocity;

            if (velocity.magnitude > 0.1)
            {
                directionX = velocity.x;
                directionY = velocity.y;
            } 
            else
            {
                directionX = sr.flipX ? -1f : 1f;
            }
        }

        Vector2 direction = new Vector2 (directionX, directionY).normalized;
        rb.AddForce(direction * force, ForceMode2D.Impulse);
    }

    public void StopSubmarine()
    {
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }

        if (ps != null)
        {
            var vel = ps.velocityOverLifetime;
            vel.enabled = false;
        }
    }
}