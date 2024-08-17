using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMovement : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] float speed = 11f;
    Vector2 horizontalInput;

    [SerializeField] float jumpHeight = 3.5f;
    bool jump;

    [SerializeField] float gravity = -30f;
    Vector3 verticalVelocity = Vector3.zero;

    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    bool isGrounded;  
    
    private void Update()
    {
        GroundedCheck();
        HandleHorizontalMovement();
        HandleJump();
        HandleVerticalMovement();
    }

    private void GroundedCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, groundMask);
        if (isGrounded) {
            verticalVelocity.y = 0;
        }
    }       

    private void HandleJump()
    {
        // If jump is true and the player is grounded, then jump
        if (jump) {
            if (isGrounded) {
                verticalVelocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity); // Jump eq -> v = sqrt(jumpHeight * -2 * gravity)
            }
            jump = false;
        }
    }

    private void HandleHorizontalMovement()
    {
        // Horizontal Movement section
        Vector3 horizontalVelocity = (transform.right * horizontalInput.x + transform.forward * horizontalInput.y) * speed;
        controller.Move(horizontalVelocity * Time.deltaTime);
    }

    private void HandleVerticalMovement()
    {
        // Vertical Movement section      
        verticalVelocity.y += gravity * Time.deltaTime;
        controller.Move(verticalVelocity * Time.deltaTime);
    }

    // These methods are called from FPSInputManager.cs & handle input from the player
    public void RecieveInput(Vector2 _horizontalInput)
    {
        horizontalInput = _horizontalInput;
    }

    public void OnJumpPressed()
    {
        jump = true;
    }
}
