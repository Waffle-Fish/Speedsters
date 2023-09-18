using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] InputAction movement;
    [SerializeField] InputAction jump;
    [SerializeField][Range(0,0.5f)] float speedFactor;
    [SerializeField][Min(0)] float jumpFactor;
    Rigidbody rb;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        if (movement != null)
        {
            transform.position = new Vector3(transform.position.x + movement.ReadValue<float>() * speedFactor, transform.position.y);
        }
        if (jump != null)
        {
            rb.AddForce(new Vector3(0, jumpFactor, 0), ForceMode.Impulse);
        }
    }
}
