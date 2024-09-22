using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float accelAmount;
    public float deccelAmount;

    [Header("Jump")]
    public float jumpForce = 12f;
    public float accelInAir;
    public float deccelInAir;
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
    }

    // Update is called once per frame
    void Update()
    {
        //Input
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, .1f, groundMask);

        input.x = Input.GetAxisRaw("Horizontal");

        //Timers
        lastOnGroundTime -= Time.deltaTime;

        //Flip Player Direction
        if(rb.velocity.x > 0)
        {
            flipPlayer(true);
        } else if(rb.velocity.x < 0)
        {
            flipPlayer(false);
        }


        //Jump
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        //Animation
        animator.SetBool("IsGrounded", isGrounded);
    }

    private void FixedUpdate()
    {
        run(1);

        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
    }

    private void Jump()
    {
        lastOnGroundTime = 0;
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

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

    private void run(float lerpAmount)
    {
        float targetSpeed = input.x * moveSpeed;
        targetSpeed = Mathf.Lerp(rb.velocity.x, targetSpeed, lerpAmount);

        float accelRate;

        //Changes acceleration if in air
        if(lastOnGroundTime > 0)
        {
            accelRate = (Mathf.Abs(targetSpeed) > 0.001f) ? accelAmount : deccelAmount;
        } else
        {
            accelRate = (Mathf.Abs(targetSpeed) > 0.001f) ? accelAmount * accelInAir : deccelAmount * deccelInAir;
        }


        float speedDif = targetSpeed - rb.velocity.x;

        float movement = speedDif * accelRate;


        rb.AddForce(movement * Vector2.right, ForceMode2D.Force);

    }
}
