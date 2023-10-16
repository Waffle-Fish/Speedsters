using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    [SerializeField] InputAction moveInput;
    [SerializeField] InputAction jump;
    [SerializeField] InputAction fastFall;
    [SerializeField] InputAction crouch;

    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float fallJumpForce = 9f;
    public float fastFallSpeed = 10f;
    public float dJForce = 9;

    public bool onGround = true;
    public bool isJumping = false;
    public bool fallJump = false;
    public bool isFastFalling = false;
    public bool isDoubleJumping = false;
    public bool isCrouching = false;
    
    private int jumps = 2;

    private BoxCollider2D characterCollider;
    private Vector2 standingSize;
    private Vector2 crouchingSize = new Vector2(1f, 0.5f);

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        characterCollider = GetComponent<BoxCollider2D>();

    }
    private void OnEnable()
    {
        moveInput.Enable();
        jump.Enable();
        fastFall.Enable();
        crouch.Enable();
    }

    private void OnDisable()
    {
        moveInput.Disable();
        jump.Disable();
        fastFall.Disable();
        crouch.Enable();
    }

    private void Update()
    {
        float MI = moveInput.ReadValue<float>();
        Debug.Log("Move input value: " + MI);

        float VI = rb.velocity.y;

        rb.velocity = new Vector2(MI * moveSpeed, rb.velocity.y);

        if (MI > 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (MI < 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        if (jump.WasPressedThisFrame() && jumps > 0)
        {
            if (!isJumping)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                isJumping = true;
                onGround = false;
                jumps--;
            }

            else if (VI < 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, fallJumpForce);
                isDoubleJumping = true;
                fallJump = true;
                jumps--;
            }

            else if (isJumping)
            {
                rb.velocity = new Vector2(rb.velocity.x, dJForce);
                jumps--;
                isDoubleJumping = true;
                onGround = false;
            }
        }

        float fastFallInput = fastFall.ReadValue<float>();

        if (fastFallInput > 0 && isJumping)
        {
           rb.velocity = new Vector2(rb.velocity.x,-fastFallSpeed);
           isFastFalling = true;
        }
        else if (!isJumping)
        {
            isFastFalling = false;
        }

        if (crouch.WasPressedThisFrame())
        {
            characterCollider.size = crouchingSize;
            isCrouching = true;
        }

        if (crouch.WasReleasedThisFrame())
        {
            characterCollider.size = standingSize;
            isCrouching = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        {
            
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
            isJumping = false;
            fallJump = false;
            isFastFalling = false;
            isDoubleJumping = false; 
            jumps = 2;
        }
    }
}