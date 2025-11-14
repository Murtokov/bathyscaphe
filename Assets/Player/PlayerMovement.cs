using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5;
    public float jumpForce = 10;
    public float ladderSpeed = 3;
    public float groundCheckDistance = 0.1f;
    public LayerMask groundLayers;

    public bool onLadder = false;
    private float horizontalInput;
    private bool isGrounded = true;
    private bool ladderMode = false;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (ladderMode && Input.GetKeyDown(KeyCode.E))
        {
            ladderMode = false;
            rb.gravityScale = 1f;
        }
        else if (onLadder && Input.GetKeyDown(KeyCode.E))
        {
            ladderMode = true;
            rb.gravityScale = 0f;
        }

        // �������� �����
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayers);
        isGrounded = hit.collider != null;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    void FixedUpdate()
    {
        if (ladderMode)
        {
            HandleLadderMovement();
        }
        else
        {
            HandleGroundMovement();
        }
    }

    private void HandleGroundMovement()
    {
        float targetSpeed = horizontalInput * walkSpeed;

        // ������ ���������� velocity (����� ������� �������)
        rb.linearVelocity = new Vector2(targetSpeed, rb.linearVelocity.y);
    }

    private void HandleLadderMovement()
    {
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 ladderVelocity = new Vector2(0, verticalInput * ladderSpeed);
        rb.linearVelocity = ladderVelocity;
    }
}