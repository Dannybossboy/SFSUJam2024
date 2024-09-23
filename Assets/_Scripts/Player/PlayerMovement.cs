using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float airGravity;
    private float defaultGravity;

    [Header("Jump")]
    public float jumpForce = 12f;
    public float jumpCutMultiplier;
    private bool isJumping;
    private bool isJumpCut;

    [Header("GroundCheck")]
    public Transform groundCheck;
    public LayerMask groundMask;

    private bool isGrounded;

    [Header("Timers")]
    private float lastOnGroundTime;

    [Header("Input")]
    private Vector2 input;

    [Header("Assists")]
    [Range(0.01f, 0.5f)] public float coyoteTime;
    [Range(0.01f, 0.5f)] public float jumpInputBufferTime;

    public Animator animator;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        defaultGravity = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        //Input
        isGrounded = Physics2D.OverlapBox(groundCheck.position, new Vector2(.35f, .1f), 0f, groundMask);

        input.x = Input.GetAxisRaw("Horizontal");

        //Grounded Behavior
        if (isGrounded)
        {
            lastOnGroundTime = coyoteTime;
            rb.gravityScale = defaultGravity;
        }
        else
        {
            lastOnGroundTime -= Time.deltaTime;
            rb.gravityScale = airGravity;
        }

        //Flip Player Direction
        if(rb.velocity.x > .1f)
        {
            flipPlayer(true);
        } else if(rb.velocity.x < -.1f)
        {
            flipPlayer(false);
        }


        //Jump
        if (Input.GetButtonDown("Jump") && lastOnGroundTime > 0)
        {
            Jump();

            isJumping = true;
            lastOnGroundTime = 0;
        }
        if (Input.GetButtonUp("Jump") && isJumping && rb.velocity.y > 0)
        {
            rb.AddForce(Vector2.down * rb.velocity.y * (1 - jumpCutMultiplier), ForceMode2D.Impulse);
            isJumping = false;
            animator.ResetTrigger("Jump");
        }

        //Animation
        animator.SetBool("IsGrounded", isGrounded);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(input.x * moveSpeed, rb.velocity.y);

        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);

        animator.SetTrigger("Jump");
    }

    private void flipPlayer(bool right)
    {
        if(right)
        {
            transform.localScale = new Vector3(1, 1, 1);
        } else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
