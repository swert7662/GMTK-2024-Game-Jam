using UnityEngine;

public class BobAndRotateWithEuler : MonoBehaviour
{
    [SerializeField] private float bobSpeed = 2f; // Speed of the bobbing motion
    [SerializeField] private float bobHeight = 0.05f; // Height of the bobbing motion
    [SerializeField] private Vector3 rotationSpeed = new Vector3(0f, 0f, 50f); // Rotation speed around each axis (x, y, z)

    private Vector3 initialPosition;
    private Vector3 initialRotation;

    private void Start()
    {
        // Store the initial position and rotation of the object
        initialPosition = transform.position;
        initialRotation = transform.eulerAngles; // Using Euler angles to store the initial rotation
    }

    private void Update()
    {
        // Bobbing motion
        float newY = initialPosition.y + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        // Rotating motion using Euler angles
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
