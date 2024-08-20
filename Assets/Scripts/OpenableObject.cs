using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenableObject : MonoBehaviour
{
    [SerializeField] private float maxOpenAngle = 90f; // Maximum angle the door will swing open
    [SerializeField] private float openLerpSpeed = 2f; // Speed at which the door swings open

    private bool isOpening = false;
    private Quaternion initialRotation;
    private Quaternion targetRotation;
    private Quaternion convertedAngle;

    private void Start()
    {
        // Store the initial rotation of the door/object
        initialRotation = transform.rotation;
        convertedAngle = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + maxOpenAngle, transform.eulerAngles.z);
        targetRotation = convertedAngle;
    }

    private void Update()
    {
        // If we are not open or closed, lerp the door to the target rotation
        if (targetRotation != initialRotation && targetRotation != convertedAngle)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * openLerpSpeed);
        }
    }

    // Public function to be called when the door should open
    public void Open()
    {
        isOpening = !isOpening;
        if (!isOpening)
        {
            targetRotation = initialRotation;
        }
        else
        {
            targetRotation = convertedAngle;
        }
    }
}
