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

    public float moveSpeed = 4f;
    public float jumpForce = 6f;
    public float fastFallSpeed = 6f;

    public bool isJumping = false;
    public bool isFastFalling = false;
    private int jumps = 2;


    private Rigidbody2D rb;

    private void OnEnable()
    {
        moveInput.Enable();
        jump.Enable();
    }

    private void OnDisable()
    {
        moveInput.Disable();
        jump.Disable();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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


        if (jump.WasPressedThisFrame())
        {
            Debug.Log("Jump pressed");
            if (!isJumping || jumps > 0)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                isJumping = true;
                jumps--;
            }

            if (isJumping || jumps == 1)
            {
                rb.AddForce(Vector2.up * (jumpForce - 1), ForceMode2D.Impulse);
                jumps--;
            }
        }

        if (fastFall.WasPressedThisFrame())
        {
            Debug.Log("Fastfall pressed");
            if (isJumping)
            {
                rb.velocity = new Vector2(rb.velocity.x, -fastFallSpeed);
                isFastFalling = true;
            }
            else
            {
                isFastFalling = false;
        }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            isFastFalling = false;
            jumps = 2;
        }
    }
}