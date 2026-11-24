using System;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField]
    private PlayerCharacterMovement _movement;

    [SerializeField]
    private PlayerCharacherStamina _stamina;

    [SerializeField]
    private InventoryManager _inventory;

     [SerializeField]
     private CameraManager _camera;

     [SerializeField]
     private InteractDetector _interactDetector;

     [SerializeField]
     private InputManager _input;

     [SerializeField]
     private FlashLight _flashlight;

    public PlayerCharacterMovement Movement => _movement;
    public PlayerCharacherStamina Stamina => _stamina;
    public InventoryManager Inventory => _inventory;

    public CameraManager Camera => _camera;
    public InteractDetector InteractDetector => _interactDetector;

    public InputManager Input => _input;

    public FlashLight FlashLight => _flashlight;



    public bool IsHiding {get; private set;}

    public void SethisHiding(bool isHiding)
    {
        IsHiding = isHiding;
    }
}
