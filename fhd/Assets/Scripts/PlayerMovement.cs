using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;


public class PlayerMovement : MonoBehaviour, InputActions.IPlayerActions
{

    private Camera _mainCamera;
    
    private Rigidbody _rigidbody;
    
    private Vector2 MouseDelta;

    private InputActions _inputAction;

    private Vector3 _jumpVector;
    private Vector2 _moveInput;

    public Vector3 movementDirection;
    public float speed = 5f;

    [SerializeField] private float jumpforce;
    [SerializeField] private Collider playerCollider;

    private void Start()
    {
        _mainCamera = Camera.main;
        _jumpVector = new Vector3();
        _moveInput = new Vector2();
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
    }

    private void OnEnable()
    {
        if (_inputAction!= null)
            return;

        _inputAction = new InputActions();
        _inputAction.Player.SetCallbacks(this);
        _inputAction.Player.Enable();
    }

    private void ApplyJump()
    {
        this._rigidbody.velocity += _jumpVector;
        _jumpVector = new Vector3();
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
        _moveInput = moveVector;
    }

    public void OnJump(InputAction.CallbackContext context)
    { 
        if(!IsGrounded()) return;
        _jumpVector = new Vector3(0, jumpforce, 0);
    }

    private void Move()
    {
        _rigidbody.velocity = new Vector3(0,_rigidbody.velocity.y, 0);
        ApplyMoveRelativeToCamera();
        ApplyJump();
    }

    private void ApplyMoveRelativeToCamera()
    {
        var cameraTransform = _mainCamera.transform;
        var cameraForward = cameraTransform.forward;
        var cameraRight = cameraTransform.right;

        cameraForward = new Vector3(cameraForward.x, 0, cameraForward.z);
        cameraRight = new Vector3(cameraRight.x, 0, cameraRight.z);
        
        cameraForward = cameraForward.normalized;
        cameraRight = cameraRight.normalized;

        var direction = cameraForward * _moveInput.y + cameraRight * _moveInput.x;
        this._rigidbody.velocity += direction.normalized * speed;
    }

    private bool IsGrounded()
    {
        var distanceToGround = playerCollider.bounds.extents.y;
        return Physics.Raycast(transform.position, -Vector3.up, distanceToGround + 0.1f);
    }
 }
