using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerController1 : MonoBehaviour
{
    [SerializeField] InputAction moveInput;
    [SerializeField] InputAction jump;
    [SerializeField] InputAction fastFall;
    [SerializeField] InputAction crouch;

    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float fastFallSpeed = 10f;
    public float dJForce;

    public bool isJumping = false;
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

        rb.velocity = new Vector2(MI * moveSpeed, rb.velocity.y);

        if (MI > 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (MI < 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        dJForce = jumpForce - 1;

        if (jump.WasPressedThisFrame() && jumps > 0)
        {
            if (!isJumping)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                isJumping = true;
                jumps--;
            }
            else if (isJumping)
            {
                rb.AddForce(Vector2.up * dJForce, ForceMode2D.Impulse);
                jumps--;
                isDoubleJumping = true;
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
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            isFastFalling = false;
            isDoubleJumping = false; 
            jumps = 2;
        }
    }
}