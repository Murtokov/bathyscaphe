using UnityEngine;

public class SubmarineMoving : MonoBehaviour
{
    public float walkSpeed;
    public float jumpSpeed;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private ParticleSystem ps;

    void Start()
    {
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
        rb.linearVelocity = new Vector2(
            horizontalInput * walkSpeed, 
            verticalInput * walkSpeed);

        var vel = ps.velocityOverLifetime;
        vel.enabled = true;
        vel.x = -rb.linearVelocity.x;
        vel.y = -rb.linearVelocity.y;


        if (horizontalInput > 0.01f)
            sr.flipX = false;
        else if (horizontalInput < -0.01f)
            sr.flipX = true;

        var shape = ps.shape;
        Vector3 shapePos = shape.position;
        shapePos.x = Mathf.Abs(shapePos.x) * (sr.flipX ? 1 : -1);
        shape.position = shapePos;
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
