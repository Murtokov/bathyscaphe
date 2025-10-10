using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed;
    public float jumpSpeed;
    public float groundCheckDistance = 0.1f;
    public LayerMask groundLayers;

    public bool onLadder;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void LateUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayers);
        bool isGrounded = hit.collider != null;
        Debug.Log(isGrounded);

        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || onLadder))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpSpeed);
        }
    }

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(horizontalInput * walkSpeed, rb.linearVelocity.y);
    }
}
