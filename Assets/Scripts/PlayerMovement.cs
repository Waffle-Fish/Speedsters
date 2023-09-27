using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] InputAction movement;
    [SerializeField] InputAction jump;
    [SerializeField] InputAction crouch;                 // make this down button while on ground
    [SerializeField] InputAction fastFall;               // make this down button while in air
    [SerializeField] [Range(0, 0.5f)] float speedFactor;
    [SerializeField] [Min(0)] float jumpFactor;
    [SerializeField] [Min(0)] float doublejumpFactor;
    [SerializeField] [Min(0)] float fastFallFactor;
    Rigidbody2D rb;

    // checks
    public bool isOnGround;
    public bool canDoubleJump;
    public bool canFastFall;
    //------------------------------------------------------

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        movement.Enable();
        jump.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
        jump.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        //left right movement
        if (movement != null)
        {
            transform.position = new Vector3(transform.position.x + movement.ReadValue<float>() * speedFactor, transform.position.y);
        }

        //ground jump
        if (jump.WasPressedThisFrame() && isOnGround)
        {
            rb.AddForce(new Vector2(0, jump.ReadValue<float>() * jumpFactor), ForceMode2D.Impulse);
        }

        //double jump
        if (jump.WasPressedThisFrame() && !isOnGround && canDoubleJump)
        {
            rb.AddForce(new Vector2(0, jump.ReadValue<float>() * doublejumpFactor), ForceMode2D.Impulse);
            canDoubleJump = false;
            canFastFall = true;

        }

//crouching button check
        if (crouch.WasPressedThisFrame() && isOnGround)
        {
            //crouching code
        }
//Fast fall button check
        if (crouch.WasPressedThisFrame() && !isOnGround && canFastFall) // fastfall should be possible once per leaving ground not "WasPressedThisFrame()" 
        {
            //fast falling code
            //rb.AddForce(new Vector2(0, fastFall.ReadValue<float>() * fastFallFactor), ForceMode2D.Impulse);
            //canFastFall = false;

        }
    }

    //after landing on ground
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            canDoubleJump = true;
            canFastFall = false;
        }
    }
    //after exiting ground 
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isOnGround = false;
            canFastFall = true;
        }
    }
}
