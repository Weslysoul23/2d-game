using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    [Header("Ground Check Settings")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator anim;
    private bool isGrounded;
    private float moveInput;
    private bool facingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Horizontal input
        moveInput = Input.GetAxis("Horizontal");

        // WALK BOOL
        bool isWalking = Mathf.Abs(moveInput) > 0.1f;
        anim.SetBool("walk", isWalking);

        // JUMP BOOL
        anim.SetBool("jump", !isGrounded);

        // Jump action
if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
{
    rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    anim.SetBool("jump", true); // FORCE jump animation immediately
}
// Reset jump when landed
if (isGrounded && rb.linearVelocity.y <= 0)
{
    anim.SetBool("jump", false);
}
        // Flip sprite
        if (moveInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && facingRight)
        {
            Flip();
        }
    }

    void FixedUpdate()
    {
        // Move player
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Ground check
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}