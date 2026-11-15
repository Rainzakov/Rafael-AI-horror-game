
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using InputActions = InputSystem_Actions;

public class InputManager : MonoBehaviour, InputActions.IPlayerActions
{
    public UnityEvent<Vector2> OnMoveInput;

    private InputActions _inputAction;

    public UnityEvent<bool> OnsprintInput;

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

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnsprintInput?.Invoke(true);
        }

        if (context.canceled)
        {
            OnsprintInput?.Invoke(false);
        }
    }



    private void Awake()
    {
        _inputAction = new InputActions();

        _inputAction.Player.SetCallbacks(this);
        _inputAction.Player.Enable();
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
