using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour, InputActions.IPlayerActions
{

    private Vector3 cameraForward;
    private Vector3 cameraRight;
    
    public Action OnJumpPerformed;
    private Vector2 MouseDelta;

    private InputActions _inputAction;

    public Vector3 _movementDirection;
    public float _speed = 5f;


    void Update()
    {
        // gameObject.transform.Translate(_movementDirection * (_speed * Time.deltaTime));
        MovePlayerRelativeToCamera();
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
        Vector2 moveVector = context.ReadValue<Vector2>();
        _movementDirection = new Vector3(moveVector.x, 0, moveVector.y);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

         OnJumpPerformed?.Invoke();
    }

    public void MovePlayerRelativeToCamera()
    {
        cameraForward = Camera.main.transform.forward;
        cameraRight = Camera.main.transform.right;

        cameraForward = new Vector3(cameraForward.x, 0, cameraForward.z);
        cameraRight = new Vector3(cameraRight.x, 0, cameraRight.z);

        cameraForward = cameraForward.normalized;
        cameraRight = cameraRight.normalized;

        Vector3 forwardRelativeInput = _movementDirection.x * cameraForward;
        Vector3 rightRelativeInput = _movementDirection.z * cameraRight;

        Vector3 cameraRelativeMovement = forwardRelativeInput + rightRelativeInput;
        gameObject.transform.Translate(cameraRelativeMovement * (_speed * Time.deltaTime), Space.World);
    }
 }
