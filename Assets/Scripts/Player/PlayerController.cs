using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    [SerializeField] InputAction moveInput;
    [SerializeField] InputAction jump;
    [SerializeField] InputAction fastFall;

    //[SerializeField] Animator anim;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float dJForce = 9;
    public float fallJumpForce = 9f;
    public float fastFallSpeed = 10f;
    public bool finished = false;

    public bool onGround = true;
    public bool fallJump = false;
    public bool isFastFalling = false;

    private BoxCollider2D characterCollider;
    private Rigidbody2D rb;
    private float YForce => jump.ReadValue<float>() * jumpForce;
    private float XForce => moveInput.ReadValue<float>() * moveSpeed;
    private int jumpCount = 2;
    private RaycastHit2D hit;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        characterCollider = GetComponent<BoxCollider2D>();
        hit = Physics2D.Raycast(transform.position, Vector2.down, 5f);
    }
    private void OnEnable()
    {
        moveInput.Enable();
        jump.Enable();
        fastFall.Enable();
    }

    private void OnDisable()
    {
        moveInput.Disable();
        jump.Disable();
        fastFall.Disable();
    }

    private void Update()
    {
        if (PauseGame.Instance.isGamePaused || finished) { return; } //If game is paused, don't register movement
        float moveInpVal = moveInput.ReadValue<float>();

        rb.velocity = new Vector2(moveInpVal * moveSpeed, rb.velocity.y);
        hit = Physics2D.Raycast(transform.position, Vector2.down, 5f);
        if(hit.collider != null && !hit.collider.CompareTag("Player1"))
        {
            Debug.Log("I have hit " + hit.collider.gameObject.name);
        }
        // Flips character 
        if (moveInpVal > 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (moveInpVal < 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        if(jump.WasPerformedThisFrame())
        {
            activateJump(jumpForce);
            // switch (jumpCount)
            // {
            //     case 2:
            //         activateJump(jumpForce);
            //         break;
            //     case 1:
            //         activateJump(dJForce);
            //         break;
            //     default:
            //         break;
            // }
        }

        //Debug.Log("Player velocity: " + rb.velocity);

        //anim.SetFloat("Speed", rb.velocity.x);

        // if (jump.WasPressedThisFrame() && jumps > 0)
        // {
        //     if (!isJumping)
        //     {
        //         rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        //         isJumping = true;
        //         onGround = false;
        //         jumps--;
        //     }

        //     else if (VI < 0)
        //     {
        //         rb.velocity = new Vector2(rb.velocity.x, fallJumpForce);
        //         isDoubleJumping = true;
        //         fallJump = true;
        //         jumps--;
        //     }

        //     else if (isJumping)
        //     {
        //         rb.velocity = new Vector2(rb.velocity.x, dJForce);
        //         jumps--;
        //         isDoubleJumping = true;
        //         onGround = false;
        //     }
        // }

        // float fastFallInput = fastFall.ReadValue<float>();
        // if (fastFallInput > 0 && isJumping)
        // {
        //    rb.velocity = new Vector2(rb.velocity.x,-fastFallSpeed);
        //    isFastFalling = true;
        // }
        // else if (!isJumping)
        // {
        //     isFastFalling = false;
        // }
    }

    private void activateJump(float f)
    {
        jumpCount--;
        rb.AddForce(new(0,jump.ReadValue<float>() * f), ForceMode2D.Impulse);
    }

    public void setVel(Vector2 vect)
    {
        rb.velocity = vect;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector2.down * 5);
    }
}