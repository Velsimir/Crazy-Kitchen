using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPause;
    public event EventHandler OnBindingRebind;

    private const string PlayerPrefBindings = "InputBindings";
    public enum Binding
    {
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight,
        Interact,
        InteractAlternative,
        Pause,
        GamepadInteract,
        GamepadInteractAlternative,
        GamepadPause
    }

    private PlayerInput _playerInput;
    private Vector2 _inputVector;

    public static GameInput Instanse { get; private set; }

    private void Awake()
    {
        Instanse = this;

        _playerInput = new PlayerInput();

        if (PlayerPrefs.HasKey(PlayerPrefBindings))
        {
            _playerInput.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PlayerPrefBindings));
        }

        _playerInput.Enable();

        _playerInput.Player.Interaction.performed += Interaction_performed;

        _playerInput.Player.InteractionAlternate.performed += InteractionAlternate_performed;

        _playerInput.Player.Pause.performed += Pause_performed;
    }

    public string GetBindingText(Binding binding)
    {
        switch (binding)
        {
            default:
            case Binding.MoveUp:
                return _playerInput.Player.Move.bindings[1].ToDisplayString();
            case Binding.MoveDown:
                return _playerInput.Player.Move.bindings[2].ToDisplayString();
            case Binding.MoveLeft:
                return _playerInput.Player.Move.bindings[3].ToDisplayString();
            case Binding.MoveRight:
                return _playerInput.Player.Move.bindings[4].ToDisplayString();
            case Binding.Interact:
               return _playerInput.Player.Interaction.bindings[0].ToDisplayString();
            case Binding.InteractAlternative:
                return _playerInput.Player.InteractionAlternate.bindings[0].ToDisplayString();
            case Binding.Pause:
                return _playerInput.Player.Pause.bindings[0].ToDisplayString();
            case Binding.GamepadInteract:
                return _playerInput.Player.Interaction.bindings[1].ToDisplayString();
            case Binding.GamepadInteractAlternative:
                return _playerInput.Player.InteractionAlternate.bindings[1].ToDisplayString();
            case Binding.GamepadPause:
                return _playerInput.Player.Pause.bindings[1].ToDisplayString();
        }
    }

    public void RebindBinding(Binding binding, Action onActionRebound)
    {
        InputAction inputAction;
        int inputIndex;

        _playerInput.Player.Disable();

        switch (binding)
        {
            default:
            case Binding.MoveUp:
                inputAction = _playerInput.Player.Move;
                inputIndex = 1;
                break;
            case Binding.MoveDown:
                inputAction = _playerInput.Player.Move;
                inputIndex = 2;
                break;
            case Binding.MoveLeft:
                inputAction = _playerInput.Player.Move;
                inputIndex = 3;
                break;
            case Binding.MoveRight:
                inputAction = _playerInput.Player.Move;
                inputIndex = 4;
                break;
            case Binding.Interact:
                inputAction = _playerInput.Player.Interaction;
                inputIndex = 0;
                break;
            case Binding.InteractAlternative:
                inputAction = _playerInput.Player.Interaction;
                inputIndex = 0;
                break;
            case Binding.Pause:
                inputAction = _playerInput.Player.Interaction;
                inputIndex = 0;
                break;
            case Binding.GamepadInteract:
                inputAction = _playerInput.Player.Interaction;
                inputIndex = 1;
                break;
            case Binding.GamepadInteractAlternative:
                inputAction = _playerInput.Player.InteractionAlternate;
                inputIndex = 1;
                break;
            case Binding.GamepadPause:
                inputAction = _playerInput.Player.Pause;
                inputIndex = 1;
                break;
        }

        inputAction.PerformInteractiveRebinding(inputIndex).OnComplete((callback) =>
        {
            _playerInput.Player.Enable();
            onActionRebound();
            PlayerPrefs.SetString(PlayerPrefBindings, _playerInput.SaveBindingOverridesAsJson());
            PlayerPrefs.Save();

            OnBindingRebind?.Invoke(this, EventArgs.Empty);
        })
        .Start();
    }

    public Vector2 GetMovementVectorNormalized()
    {
        _inputVector = _playerInput.Player.Move.ReadValue<Vector2>();

        _inputVector = _inputVector.normalized;

        return _inputVector;
    }

    private void OnDestroy()
    {
        _playerInput.Player.Interaction.performed += Interaction_performed;

        _playerInput.Player.InteractionAlternate.performed += InteractionAlternate_performed;

        _playerInput.Player.Pause.performed += Pause_performed;

        _playerInput.Dispose();
    }

    private void Pause_performed(InputAction.CallbackContext obj)
    {
        OnPause?.Invoke(this,EventArgs.Empty);
    }

    private void InteractionAlternate_performed(InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interaction_performed(InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }
}