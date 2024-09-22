using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Jump")]
    public float jumpForce = 12f;
    private bool isJumping;
    private bool isJumpCut;

    [Header("GroundCheck")]
    public Transform groundCheck;
    public LayerMask groundMask;

    private bool isGrounded;

    [Header("Timers")]
    private float lastOnGroundTime;

    [Header("Assists")]
    [Range(0.01f, 0.5f)] public float coyoteTime;
    [Range(0.01f, 0.5f)] public float jumpInputBufferTime;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, .1f, groundMask);

        float horizontal = Input.GetAxisRaw("Horizontal");

        lastOnGroundTime -= Time.deltaTime;


        //Jump
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        //Move
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
    }
}
