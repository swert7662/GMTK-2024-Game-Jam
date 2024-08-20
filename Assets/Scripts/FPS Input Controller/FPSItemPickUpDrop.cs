using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSItemPickUpDrop : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] private LayerMask interactableLayers;
    [SerializeField] private float pickUpRange = 5f;
    [SerializeField] private Image reticleImage;
    [SerializeField] private Color highlightReticleColor = Color.red;

    private GrabbableObject currentGrabbableObject;
    private Vector2 defaultReticleSize = new Vector2(10f, 10f); 
    private Vector2 highlightReticleSize = new Vector2(15f, 15f); 
    private Color defaultReticleColor;
    private float sizeLerpSpeed = 10f;

    private void Start()
    {
        defaultReticleColor = reticleImage.color; // Store the default reticle color
    }
    private void Update()
    {
        HandleRaycast();
    }

    public void OnInteractPressed()
    {
        if (currentGrabbableObject == null)
        {
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hit, pickUpRange, interactableLayers))
            {
                if (hit.transform.TryGetComponent(out currentGrabbableObject))
                {
                    currentGrabbableObject.Grab(objectGrabPointTransform);
                }
                else if (hit.transform.TryGetComponent(out OpenableObject openableObject))
                {
                    openableObject.Open();
                }
            }
        }
        else
        {
            currentGrabbableObject.Drop();
            currentGrabbableObject = null;
        }

    }

    private void HandleRaycast()
    {
        if (currentGrabbableObject) { return;}

        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hit, pickUpRange, interactableLayers))
        {
            if (hit.transform.TryGetComponent(out GrabbableObject grabbableObject) || hit.transform.TryGetComponent(out OpenableObject openableObject))
            {
                reticleImage.color = highlightReticleColor; // Change reticle color when hovering over a grabbable or openable object
                reticleImage.rectTransform.sizeDelta = Vector2.Lerp(reticleImage.rectTransform.sizeDelta, highlightReticleSize, Time.deltaTime * sizeLerpSpeed);

            }
            else
            {
                reticleImage.color = defaultReticleColor; // Reset to default color when not hovering over a grabbable object
                reticleImage.rectTransform.sizeDelta = Vector2.Lerp(reticleImage.rectTransform.sizeDelta, defaultReticleSize, Time.deltaTime * sizeLerpSpeed);
            }
        }
        else
        {
            reticleImage.color = defaultReticleColor; // Reset to default color when not hitting anything
            reticleImage.rectTransform.sizeDelta = Vector2.Lerp(reticleImage.rectTransform.sizeDelta, defaultReticleSize, Time.deltaTime * sizeLerpSpeed);

        }
    }


}
