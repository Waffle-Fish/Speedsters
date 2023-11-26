using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
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
    [SerializeField] private Vector2 groundDetectSize;
    [SerializeField] Animator anim;

    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float dJForce = 9;
    public float fastFallSpeed = 10f;
    public bool finished = false;

    private BoxCollider2D characterCollider;
    private Rigidbody2D rb;
    private PlayerInput playerInput;
    private Controls playerControls;
    private int jumpCount = 2;
    private Vector2 boxOrigin;
    private int detectTimer = 3;
    private bool onGround = true;
    private bool fastFalling = false;

    private LayerMask GroundLayer => LayerMask.GetMask("Ground");
    private bool IsJumping => !onGround;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        characterCollider = GetComponent<BoxCollider2D>();
        playerInput = GetComponent<PlayerInput>();

        playerControls = new();
        playerControls.General.Enable();
        if(gameObject.CompareTag("Player1"))
        {
            playerControls.Player1Controls.Enable();
            playerControls.Player1Controls.Jump.performed += ActivateJump;
            playerControls.Player1Controls.FastFall.performed += FastFall;
        }
        else
        {
            playerControls.Player2Controls.Enable();
            playerControls.Player2Controls.Jump.performed += ActivateJump;
            playerControls.Player2Controls.FastFall.performed += FastFall;
        }
        
        //playerControls.General.PauseMenu.performed += PauseGame.Instance.ToggleMenu;
    }

    private void FixedUpdate()
    {
        if (PauseGame.Instance.isGamePaused || finished) { return; }
        float moveInpVal = gameObject.CompareTag("Player1") ? playerControls.Player1Controls.Forward.ReadValue<float>() : playerControls.Player2Controls.Forward.ReadValue<float>();
        rb.velocity = new Vector2(moveInpVal * moveSpeed, rb.velocity.y);
        FlipCharacter(moveInpVal);
        DetectGround();
        anim.SetFloat("XVel", rb.velocity.x);
        anim.SetFloat("YVel", rb.velocity.y);
        anim.SetBool("Jumping", IsJumping);
        anim.SetBool("FastFalling", fastFalling);
        anim.SetBool("OnGround", onGround);
    }

    private void DetectGround()
    {
        if (IsJumping && detectTimer > 0)
        {
            detectTimer--;
            return;
        } 
        detectTimer = 3;
        boxOrigin = new(transform.position.x, transform.position.y - characterCollider.size.y / 2f);
        Collider2D groundDetect = Physics2D.OverlapBox(boxOrigin, groundDetectSize, 0f, GroundLayer);
        if (groundDetect && groundDetect.CompareTag("Ground")) { 
            jumpCount = 2;
            fastFalling = false;
            onGround = true;
        }
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

    private void ActivateJump(InputAction.CallbackContext context)
    {
        if(!(context.performed && jumpCount > 0)) { return; }
        onGround = false;
        fastFalling = false;
        Vector2 f = (jumpCount == 2) ? Vector2.up *  jumpForce : Vector2.up *  dJForce;
        rb.AddForce(f, ForceMode2D.Impulse);
        jumpCount--;
        //Debug.Log("Remaining Jumps: " + jumpCount);
    }

    private void FastFall(InputAction.CallbackContext context)
    {
        if(!context.performed) { return; }
        if(!onGround)
        {
           rb.velocity = new Vector2(rb.velocity.x,-fastFallSpeed);
           fastFalling = true;
        }
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