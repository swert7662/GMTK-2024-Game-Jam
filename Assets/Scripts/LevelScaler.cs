using UnityEngine;

public class LevelScaler : MonoBehaviour
{
    [SerializeField] private float scaleAmount = 2f;
    [SerializeField] GameObject player;
    [SerializeField] Transform centerPointTransform;

    private FPSMovement playerMovement;
    private Transform playerTransform;

    private void Awake()
    {
        playerMovement = player.GetComponent<FPSMovement>();
        playerTransform = player.transform;
        if (playerMovement == null || playerTransform == null)
        {
            Debug.LogError("Player Movement or Player Transform not found");
        }
    }

    public void ScaleLevel()
    {
        // Calculate the player's position relative to the center of the level
        Vector3 relativePosition = playerTransform.transform.position - centerPointTransform.position;

        transform.localScale *= scaleAmount; // Doubles the size of the level

        // Move the player to the new position relative to the center of the level
        Vector3 newPosition = centerPointTransform.position + relativePosition * scaleAmount;

        playerMovement.UpdatePlayerPosition(newPosition);
    }
}
