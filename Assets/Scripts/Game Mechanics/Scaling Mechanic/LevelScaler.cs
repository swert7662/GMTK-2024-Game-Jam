using UnityEngine;

public class LevelScaler : MonoBehaviour
{
    [SerializeField] private float scaleAmount = 2f; 

    private GameObject player;
    private FPSMovement playerMovement;
    private Transform playerTransform;
    private CharacterController playerController;
    private Transform crouchCameraPosition;
    private Transform standCameraPosition;
    private Transform groundCheckPosition;
    private Camera playerCamera;
    private FPSLook playerLook;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
        playerMovement = player.GetComponent<FPSMovement>();
        playerController = player.GetComponent<CharacterController>();        
        playerCamera = playerTransform.GetComponentInChildren<Camera>();
        playerLook = playerTransform.GetComponentInChildren<FPSLook>();     


        // Get references from the player’s FPSMovement
        crouchCameraPosition = playerMovement.GetCrouchCameraPosition();
        standCameraPosition = playerMovement.GetStandCameraPosition();
        groundCheckPosition = playerMovement.GetGroundCheckPosition();

        if (playerMovement == null || playerTransform == null || playerController == null)
        {
            Debug.LogError("Player Movement, Player Transform, or Player Controller not found");
        }
    }

    public void ScalePlayer()
    {
        // Adjust the player's movement parameters to account for the new scale
        UpdateMovementParameters(scaleAmount);

        // Adjust the camera's field of view (FOV) to enhance the shrinking effect
        if (playerCamera != null)
        {
            playerCamera.fieldOfView -= 15;
            playerLook.SetSensitivity(playerLook.GetSensitivity() * .75f);
        }
    }

    public void UpdateMovementParameters(float scaleAmount)
    {
        playerController.height /= scaleAmount;
        playerController.radius /= scaleAmount;

        // Optionally adjust crouch and stand heights to match the new scale
        playerMovement.SetCrouchHeight(playerMovement.GetCrouchHeight() / scaleAmount);
        playerMovement.SetNormalHeight(playerMovement.GetNormalHeight() / scaleAmount);

        crouchCameraPosition.localPosition /= scaleAmount;
        standCameraPosition.localPosition /= scaleAmount;
        groundCheckPosition.localPosition = new Vector3(0, playerMovement.GetNormalHeight() / -2, 0);
    }
}
