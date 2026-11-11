
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using InputActions = UnityEngine.InputSystem.DefaultInputActions;

public class InputManager : MonoBehaviour, InputActions.IPlayerActions
{
    public UnityEvent<Vector2> OnMoveInput;

    private InputActions _inputAction;

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Interact");
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        OnMoveInput?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnLook(InputAction.CallbackContext context)
    {
    }

    public void OnFire(InputAction.CallbackContext context)
    {
    }

    private void Awake()
    {
        _inputAction = new InputActions();
        _inputAction.Enable();
        _inputAction.Player.Enable();
        _inputAction.Player.SetCallbacks(this);
    }

    private void OnDestroy()
    {
        if (_inputAction != null)
        {
            _inputAction.Player.SetCallbacks(null);
            _inputAction.Dispose();
        }
    }
}
