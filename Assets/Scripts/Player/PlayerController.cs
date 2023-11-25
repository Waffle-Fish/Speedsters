using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    [Tooltip("Buffer time between jumps to prevent spamming press")]
    [SerializeField]
    private Vector2 groundDetectSize;

    [SerializeField] Animator anim;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float dJForce = 9;
    public float fallJumpForce = 9f;
    public float fastFallSpeed = 10f;
    public bool finished = false;

    
    private BoxCollider2D characterCollider;
    private Rigidbody2D rb;
    private PlayerInput playerInput;
    private Controls playerControls;
    private int jumpCount = 2;
    private LayerMask GroundLayer => LayerMask.GetMask("Ground");
    private Vector2 boxOrigin;
    private InputAction jump;
    private int detectTimer = 3;
    private bool justJump = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        characterCollider = GetComponent<BoxCollider2D>();
        playerInput = GetComponent<PlayerInput>();

        playerControls = new();
        playerControls.Player1.Enable();
        jump = playerControls.Player1.Jump;
        playerControls.Player1.Jump.performed += ActivateJump;
    }

    private void FixedUpdate()
    {
        if (PauseGame.Instance.isGamePaused || finished) { return; } //If game is paused, don't register movement
        
        float moveInpVal = playerControls.Player1.Movement.ReadValue<float>();//moveInput.ReadValue<float>();
        rb.velocity = new Vector2(moveInpVal * moveSpeed, rb.velocity.y);
        FlipCharacter(moveInpVal);
        DetectGround();

        anim.SetFloat("Speed", rb.velocity.x);
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

    private void DetectGround()
    {
        if (justJump && detectTimer > 0)
        {
            detectTimer--;
            return;
        }
        justJump = false; 
        detectTimer = 3;
        boxOrigin = new(transform.position.x, transform.position.y - characterCollider.size.y / 2f);
        Collider2D groundDetect = Physics2D.OverlapBox(boxOrigin, groundDetectSize, 0f, GroundLayer);
        if (groundDetect && groundDetect.CompareTag("Ground")) { jumpCount = 2; }
    }

    private void FlipCharacter(float moveInpVal)
    {
        if (moveInpVal > 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (moveInpVal < 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    public void ActivateJump(InputAction.CallbackContext context)
    {
        if(!(context.performed && jumpCount > 0)) { return; }
        justJump = true;
        Vector2 f = (jumpCount == 2) ? Vector2.up *  jumpForce : Vector2.up *  dJForce;
        rb.AddForce(f, ForceMode2D.Impulse);
        jumpCount--;
        //Debug.Log("Remaining Jumps: " + jumpCount);
    }

    public void setVel(Vector2 vect)
    {
        rb.velocity = vect;
    }

    void OnDrawGizmos()
    {
       Gizmos.color = Color.red;
       Gizmos.DrawCube(boxOrigin, groundDetectSize);
    }
}