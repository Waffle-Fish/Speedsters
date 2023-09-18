using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] InputAction movement;
    [SerializeField][Range(0,0.5f)] float speedFactor;


    void Start()
    {

    }

    private void OnEnable()
    {
        movement.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (movement != null)
        {
            transform.position = new Vector3(transform.position.x + movement.ReadValue<float>() * speedFactor, transform.position.y);
        }
    }
}
