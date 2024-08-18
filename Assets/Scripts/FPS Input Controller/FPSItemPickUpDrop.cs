using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSItemPickUpDrop : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] private LayerMask pickUpLayerMask;
    [SerializeField] private float pickUpRange = 5f;

    private GrabbableObject currentGrabbableObject;

    public void OnInteractPressed()
    {
        if (currentGrabbableObject == null)
        {
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hit, pickUpRange, pickUpLayerMask))
            {
                if (hit.transform.TryGetComponent(out currentGrabbableObject))
                {
                    currentGrabbableObject.Grab(objectGrabPointTransform);
                }
            }
        }
        else
        {
            currentGrabbableObject.Drop();
            currentGrabbableObject = null;
        }

    }
}
