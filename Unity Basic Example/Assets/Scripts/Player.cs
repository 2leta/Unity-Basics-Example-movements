using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private LayerMask playerMask;

    public float jumpForce = 5f;
    public float sideForce = 3f;

    private bool jumpKeyWasPressed = false;
    private float horizontalInput;
    private Rigidbody rigidbodyComponent;
    private int doubleJumpsRemaining = 0;

    private void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpKeyWasPressed = true;
        }

        horizontalInput = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        if (jumpKeyWasPressed)
        {
            if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0)
            {
                if (doubleJumpsRemaining > 0)
                {
                    rigidbodyComponent.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
                    doubleJumpsRemaining--;
                    return;
                }
                else
                {
                    return;
                }
            } 

            rigidbodyComponent.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            jumpKeyWasPressed = false;
        }

        rigidbodyComponent.velocity = new Vector3(horizontalInput * sideForce, rigidbodyComponent.velocity.y, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            Destroy(other.gameObject);
            doubleJumpsRemaining++;
        }
    }
}
