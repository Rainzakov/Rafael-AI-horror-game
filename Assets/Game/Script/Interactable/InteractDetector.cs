using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;

public class InteractDetector : MonoBehaviour
{
    [SerializeField]
    
    private PlayerCharacter _owner;

    [SerializeField]
    private float _detectorDistance;

    [SerializeField]
    private Vector3 _detectorBoxSize = Vector3.one;

    [SerializeField]
    private LayerMask _interactablePlayer;

    private Iinteractable _detectedInterctable;
    private bool _isInteracting;

    public bool Enabled {get; private set;} = true;

    public void SetEnabled(bool isEnabled)
    {
        Enabled = isEnabled;
    }

    private void Update()
    {
        UpdateDetection();
    }

    private void UpdateDetection()
    {
        if(_isInteracting)
        {
            _isInteracting = false;
            return;
        }
        if (Enabled == true)
        {
            Transform cameraTransform = Camera.main.transform;
            bool isDetectingInteractable = Physics.BoxCast(cameraTransform.position, 
                                                        _detectorBoxSize * 0.5f, 
                                                        cameraTransform.forward,out RaycastHit hit,
                                                        Quaternion.identity,
                                                        _detectorDistance,
                                                        _interactablePlayer);
            if (isDetectingInteractable)
        {
            // munculkan tampilan objek yang terdekteksi
            Iinteractable interactable = hit.collider.gameObject.GetComponent<Iinteractable>();
            if (interactable != null)
            {
                _detectedInterctable = interactable;
            }
        }
        }
    }

    public void interact()
    {
        if (_detectedInterctable != null && Enabled == true)
        {
            _detectedInterctable.Interact(_owner);
            _detectedInterctable = null;
            _isInteracting = true;
        }
    }


    void OnDrawGizmosSelected()
    {
        Transform cameraTransform = Camera.main.transform;
        Gizmos.color = Color.red;
        if (Enabled == true)
        {
             bool isDetectingInteractable = Physics.BoxCast(cameraTransform.position,
                                                        _detectorBoxSize * 0.5f, 
                                                        cameraTransform.forward,
                                                        out RaycastHit hit,Quaternion.identity,
                                                        _detectorDistance,_interactablePlayer);
            if (isDetectingInteractable)
            {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(cameraTransform.position,cameraTransform.position + cameraTransform.forward * hit.distance);
            Gizmos.DrawWireCube(cameraTransform.position + cameraTransform.forward * hit.distance, _detectorBoxSize);
            }
            else
            {
            Gizmos.DrawLine(cameraTransform.position,cameraTransform.position + cameraTransform.forward * _detectorDistance);
            Gizmos.DrawWireCube(cameraTransform.position + cameraTransform.forward * _detectorDistance, _detectorBoxSize);
            }
        }
        
    }
}
