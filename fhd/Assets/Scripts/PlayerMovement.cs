using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour, InputActions.IPlayerActions
{

    public Action OnJumpPerformed;
    private Vector2 MouseDelta;

    private InputActions _inputAction;

    private Vector3 _movementDirection;
    public float _speed = 5f;


    void Update()
    {
        gameObject.transform.Translate(_movementDirection * (_speed * Time.deltaTime));
    }

    private void OnEnable()
    {
        if (_inputAction!= null)
            return;

        _inputAction = new InputActions();
        _inputAction.Player.SetCallbacks(this);
        _inputAction.Player.Enable();
    }

    public void OnDisable()
    {
        _inputAction.Player.Disable();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        MouseDelta = context.ReadValue<Vector2>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _movementDirection = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

         OnJumpPerformed?.Invoke();
    }
}
