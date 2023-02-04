using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour, InputActions.IPlayerActions
{

    private Camera _mainCamera;
    private Vector3 _cameraForward;
    private Vector3 _cameraRight;
    
    public Action OnJumpPerformed;
    private Vector2 MouseDelta;

    private InputActions _inputAction;

    public Vector3 _movementDirection;
    public float _speed = 5f;

    private RaycastHit _raycastHit;
    private float hitDistance = 5f;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    void Update()
    {
        gameObject.transform.Translate(_movementDirection * (_speed * Time.deltaTime));
        MovePlayerRelativeToCamera();
        RayCaster();
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
        _movementDirection = new Vector3(moveVector.y, 0, moveVector.x);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

         OnJumpPerformed?.Invoke();
    }

    public void MovePlayerRelativeToCamera()
    {
        _cameraForward = _mainCamera.transform.forward;
        _cameraRight = _mainCamera.transform.right;

        _cameraForward = new Vector3(_cameraForward.x, 0, _cameraForward.z);
        _cameraRight = new Vector3(_cameraRight.x, 0, _cameraRight.z);

        _cameraForward = _cameraForward.normalized;
        _cameraRight = _cameraRight.normalized;

        Vector3 forwardRelativeInput = _movementDirection.x * _cameraForward;
        Vector3 rightRelativeInput = _movementDirection.z * _cameraRight;

        Vector3 cameraRelativeMovement = forwardRelativeInput + rightRelativeInput;
        gameObject.transform.Translate(cameraRelativeMovement * (_speed * Time.deltaTime), Space.World);
    }

    public void JumpHandler()
    {
        
    }

    private void RayCaster()
    {
        if (Physics.Raycast(gameObject.transform.position, transform.TransformDirection(Vector3.down), out _raycastHit,
                hitDistance))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * _raycastHit.distance, Color.yellow);
        } else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
    }
 } 
