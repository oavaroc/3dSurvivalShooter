using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Camera _mainCamera;
    private PlayerInputActions _playerInputActions;
    private Vector3 _playerVelocity = Vector3.zero;
    private CharacterController _controller;

    [SerializeField]
    private bool _groundedPlayer;
    [SerializeField]
    private float _playerSpeed = 2.0f;
    [SerializeField]
    private float _jumpHeight = 10.0f;
    [SerializeField]
    private float _gravityValue = -9.81f;
    [SerializeField]
    private float _maxRotationAngle=15f;
    [SerializeField]
    private float _mouseSensitivity = 1f;


    private void Start()
    {
        _mainCamera = Camera.main;
        _controller = GetComponent<CharacterController>();
        _playerInputActions=new PlayerInputActions();
        _playerInputActions.Enable();
        _playerInputActions.Player.MouseMove.performed += OnMouseDelta;
        _playerInputActions.Player.CursorEscape.performed += CursorEscape_performed;
        _playerInputActions.Player.LeftClick.performed += LeftClick_performed;
        Cursor.lockState = CursorLockMode.Locked;
    }


    private void LeftClick_performed(InputAction.CallbackContext obj)
    {
        if(Cursor.lockState != CursorLockMode.Locked)
            Cursor.lockState = CursorLockMode.Locked;
    }

    private void CursorEscape_performed(InputAction.CallbackContext obj)
    {
        Cursor.lockState = CursorLockMode.None;
    }

    private void OnMouseDelta(InputAction.CallbackContext context)
    {
        Vector2 delta = context.ReadValue<Vector2>();
        transform.localRotation = Quaternion.AngleAxis(
            transform.localEulerAngles.y + (delta.x * _mouseSensitivity), Vector3.up);
        _mainCamera.transform.localRotation = 
            Quaternion.AngleAxis(
                Mathf.Clamp(
                    _mainCamera.transform.localEulerAngles.x - (delta.y * _mouseSensitivity), 
                    0, 
                    30), 
                Vector3.right);
    }

    void Update()
    {
        CalculateMovement();
    }

    private void CalculateMovement()
    {
        _groundedPlayer = _controller.isGrounded;
        if(_groundedPlayer)
        {
            Vector2 movement = _playerInputActions.Player.WASDMovement.ReadValue<Vector2>();
            _playerVelocity = new Vector3(movement.x, 0, movement.y) *_playerSpeed;
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                _playerVelocity.y = _jumpHeight;
            }
        }
        _playerVelocity.y += _gravityValue * Time.deltaTime;
        _controller.Move(transform.TransformDirection(_playerVelocity) * Time.deltaTime);
    }
}
