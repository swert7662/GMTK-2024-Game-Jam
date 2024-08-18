using UnityEngine;
using UnityEngine.InputSystem;
public class FPSLook : MonoBehaviour
{
    [SerializeField] private float sensitivityX = 8f; // 30 is good for controller, 60 is good for mouse
    [SerializeField] private float sensitivityY = 0.5f; //.1 is good for controller, .5 is good for mouse
    [SerializeField] float gamepadMultiplier = 0.5f;
    private float lookX, lookY;

    [SerializeField] private Transform playerCamera;
    [SerializeField] private float xClamp = 85f;
    private float xRotation = 0f;

    private PlayerInput playerInput;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        HandleYRotation();
        HandleXRotation();
    }

    private void HandleXRotation()
    {
        // Pitch the camera around the X axis
        xRotation -= lookY;
        //xRotation -= lookY * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -xClamp, xClamp);
        Vector3 targetRotation = transform.eulerAngles;
        targetRotation.x = xRotation;
        //playerCamera.eulerAngles = new Vector3(xRotation, playerCamera.transform.eulerAngles.y, playerCamera.transform.eulerAngles.z);
        playerCamera.eulerAngles = targetRotation;
    }


    private void HandleYRotation()
    {
        // Rotate the player around the Y axis
        transform.Rotate(Vector3.up, lookX * Time.deltaTime);
    }

    public void RecieveInput(Vector2 lookInput)
    {
        lookX = lookInput.x * sensitivityX;
        lookY = lookInput.y * sensitivityY;
    }

}