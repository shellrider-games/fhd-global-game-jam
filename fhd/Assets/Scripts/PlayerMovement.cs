using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour, InputActions.IPlayerActions
{

    private Camera _mainCamera;
    private Vector3 _cameraForward;
    private Vector3 _cameraRight;

    private Rigidbody _rigidbody;
    
    private Vector2 MouseDelta;

    private InputActions _inputAction;

    private Vector3 _jumpVector;
    
    public Vector3 movementDirection;
    public float speed = 5f;

    [SerializeField] private float jumpforce;

    private void Start()
    {
        _mainCamera = Camera.main;
        _jumpVector = new Vector3();
        this._rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        MovePlayerRelativeToCamera();
        ApplyJumpVector();
    }

    private void OnEnable()
    {
        if (_inputAction!= null)
            return;

        _inputAction = new InputActions();
        _inputAction.Player.SetCallbacks(this);
        _inputAction.Player.Enable();
    }

    private void ApplyJumpVector()
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
        movementDirection = new Vector3(moveVector.y, 0, moveVector.x);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        this._jumpVector = new Vector3(0, jumpforce, 0);
    }
    
    

    public void MovePlayerRelativeToCamera()
    {
        _cameraForward = _mainCamera.transform.forward;
        _cameraRight = _mainCamera.transform.right;

        _cameraForward = new Vector3(_cameraForward.x, 0, _cameraForward.z);
        _cameraRight = new Vector3(_cameraRight.x, 0, _cameraRight.z);

        _cameraForward = _cameraForward.normalized;
        _cameraRight = _cameraRight.normalized;

        Vector3 forwardRelativeInput = movementDirection.x * _cameraForward;
        Vector3 rightRelativeInput = movementDirection.z * _cameraRight;
        Vector3 cameraRelativeMovement = forwardRelativeInput + rightRelativeInput;
        
        
        gameObject.transform.Translate(cameraRelativeMovement * (speed * Time.deltaTime), Space.World);
    }
 }
