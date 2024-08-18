using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FPSMovement : MonoBehaviour
{
    private Transform player;
    [SerializeField] private CharacterController controller;
    private float speed;
    [SerializeField] private float walkSpeed = 7f;
    [SerializeField] private float crouchMoveSpeed = 4f;
    [SerializeField] private float sprintSpeed = 11f;
    private Vector2 horizontalInput;

    [SerializeField] float jumpHeight = 3.5f;
    bool jump;

    [SerializeField] float gravity = -30f;
    Vector3 verticalVelocity = Vector3.zero;

    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;

    [SerializeField] Transform cameraTransform;
    [SerializeField] float crouchHeight;
    [SerializeField] float normalHeight;
    [SerializeField] Transform crouchCameraPosition;
    [SerializeField] Transform standCameraPosition;
    [SerializeField] float crouchTransitionSpeed;
    bool isGrounded;
    bool isCrouching = false;
    bool isSprinting = false;

    private void Start()
    {
        player = this.transform;
    }
    private void Update()
    {
        GroundedCheck();
        HandleHorizontalMovement();
        HandleJump();
        HandleCrouch();
        HandleVerticalMovement();
    }

    private void GroundedCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, groundMask);
        if (isGrounded) {
            verticalVelocity.y = 0;
        }
    }

    private bool CanStandUp()
    {
        RaycastHit hit;
        if (Physics.SphereCast(crouchCameraPosition.position, controller.radius, Vector3.up, out hit, normalHeight - crouchHeight))
        {
            return false; // Something is in the way, can't stand up
        }
        return true; // Safe to stand up
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

    private void HandleCrouch()
    {
        if (isCrouching)
        {
            // Instantly change the controller's height
            float previousHeight = controller.height;
            controller.height = crouchHeight;

            // Adjust player's vertical position to avoid sinking into the ground
            float heightDifference = previousHeight - crouchHeight;
            controller.center = new Vector3(controller.center.x, controller.center.y - heightDifference / 2, controller.center.z);

            // Smoothly transition the camera position to the crouch position
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, crouchCameraPosition.position, crouchTransitionSpeed * Time.deltaTime);
        }
        else if (!isCrouching && CanStandUp())
        {
            // Instantly change the controller's height
            float previousHeight = controller.height;
            controller.height = normalHeight;

            // Adjust player's vertical position to avoid sinking into the ground
            float heightDifference = normalHeight - previousHeight;
            controller.center = new Vector3(controller.center.x, controller.center.y + heightDifference / 2, controller.center.z);

            // Smoothly transition the camera position to the stand position
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, standCameraPosition.position, crouchTransitionSpeed * Time.deltaTime);
        }
    }

    private void HandleHorizontalMovement()
    {
        // Horizontal Movement section
        speed = walkSpeed;
        if (isSprinting)
        {
            speed = sprintSpeed;
        }
        if (isCrouching)
        {
            speed = crouchMoveSpeed;
        }
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
        Debug.Log("Jump pressed");
        jump = true;
    }

    public void OnCrouchPressed(bool toggle)
    {
        if (toggle)
        {
            Debug.Log("Crouch pressed");
            isCrouching = true;
        }
        else if (!toggle)
        {
            Debug.Log("Crouch released");
            isCrouching = false;
        }
    }

    public void OnSprintPressed(bool toggle)
    {
        if (toggle)
        {
            Debug.Log("Sprint pressed");
            isSprinting = true;
        }
        else if (!toggle)
        {
            Debug.Log("Sprint released");
            isSprinting = false;
        }
    }

    private void OnDrawGizmos()
    {
        if (controller != null)
        {
            Gizmos.color = Color.green;

            // Draw the base of the character controller (cylinder shape)
            Gizmos.DrawWireSphere(transform.position + controller.center + Vector3.up * (controller.height / 2 - controller.radius), controller.radius);
            Gizmos.DrawWireSphere(transform.position + controller.center - Vector3.up * (controller.height / 2 - controller.radius), controller.radius);
            Gizmos.DrawLine(
                transform.position + controller.center + Vector3.up * (controller.height / 2 - controller.radius) + Vector3.forward * controller.radius,
                transform.position + controller.center - Vector3.up * (controller.height / 2 - controller.radius) + Vector3.forward * controller.radius
            );
            Gizmos.DrawLine(
                transform.position + controller.center + Vector3.up * (controller.height / 2 - controller.radius) - Vector3.forward * controller.radius,
                transform.position + controller.center - Vector3.up * (controller.height / 2 - controller.radius) - Vector3.forward * controller.radius
            );
            Gizmos.DrawLine(
                transform.position + controller.center + Vector3.up * (controller.height / 2 - controller.radius) + Vector3.right * controller.radius,
                transform.position + controller.center - Vector3.up * (controller.height / 2 - controller.radius) + Vector3.right * controller.radius
            );
            Gizmos.DrawLine(
                transform.position + controller.center + Vector3.up * (controller.height / 2 - controller.radius) - Vector3.right * controller.radius,
                transform.position + controller.center - Vector3.up * (controller.height / 2 - controller.radius) - Vector3.right * controller.radius
            );
        }
    }
}
