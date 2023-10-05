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

    public float moveSpeed = 4f;
    public float jumpForce = 9f;
    public float fastFallSpeed = 6f;

    private bool isJumping = false;
    private bool isFastFalling = false;
    private int jumps = 2;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float MI = moveInput.ReadValue<float>();

        rb.velocity = new Vector2(MI * moveSpeed, rb.velocity.y);

        if (MI > 0 )
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        if (MI < 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        if (jump.WasPressedThisFrame() && (!isJumping || jumps > 0))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = true;
            jumps--;
        }


        if (fastFall.WasPressedThisFrame())
        {
            if (isJumping)
            {
                rb.velocity = new Vector2(rb.velocity.x, -fastFallSpeed);
                isFastFalling = true;
            }
        }
        else
        {
            isFastFalling = false;
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