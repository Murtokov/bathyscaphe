using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5;
    public float jumpForce = 10;
    public float ladderSpeed = 3;
    public float maxSpeedX = 10;
    public float maxSpeedY = 10;
    public float groundCheckDistance = 0.1f;
    public LayerMask groundLayers;

    public bool onLadder = false;
    private bool ladderMode = false;
    private bool isGrounded = true;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (ladderMode && Input.GetKeyDown(KeyCode.E))
        {
            ladderMode = false;
        }
        else if (onLadder && Input.GetKeyDown(KeyCode.E))
        {
            ladderMode = true;
        }
    }

    void LateUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayers);
        isGrounded = hit.collider != null;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    void FixedUpdate()
    {
        Vector2 velocity = rb.linearVelocity;

        if (ladderMode)
        {
            float verticalInput = Input.GetAxis("Vertical");
            if (!(isGrounded && verticalInput < 0))
            {
                rb.AddForce(new Vector2(0, verticalInput * ladderSpeed), ForceMode2D.Force);
            }

            velocity.x = 0;
            rb.gravityScale = 0f;
        }
        else
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            rb.AddForce(new Vector2(horizontalInput * walkSpeed, 0), ForceMode2D.Force);
            rb.gravityScale = 1f;
        }

        velocity = rb.linearVelocity;
        velocity.x = Mathf.Clamp(velocity.x, -maxSpeedX, maxSpeedX);
        velocity.y = Mathf.Clamp(velocity.y, -maxSpeedY, maxSpeedY);
        rb.linearVelocity = velocity;
    }
}