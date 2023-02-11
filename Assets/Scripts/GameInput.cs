using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;

    private PlayerInput _playerInput;
    private Vector2 _inputVector;

    private void Awake()
    {
        _playerInput = new PlayerInput();

        _playerInput.Enable();

        _playerInput.Player.Interaction.performed += Interaction_performed;

        _playerInput.Player.InteractionAlternate.performed += InteractionAlternate_performed;
    }

    private void InteractionAlternate_performed(InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interaction_performed(InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        _inputVector = _playerInput.Player.Move.ReadValue<Vector2>();

        _inputVector = _inputVector.normalized;

        return _inputVector;
    }
}