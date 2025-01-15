using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : NetworkBehaviour
{
    private float _speed = 5f;
    private Rigidbody2D _rigidbody;
    private BluesBobRabbitInput _inputActions;
    private InputAction _move;
    private InputAction _jump;
    private Vector2 _previousMovementInput;
    private bool _isGrounded;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _inputActions = new BluesBobRabbitInput();
        _jump = _inputActions.FindAction("Jump");
        _move = _inputActions.FindAction("Move");
    }

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            _inputActions.Enable();
            _move.performed += OnMovePerformed;
            _jump.performed += OnJumpPerformed;
            _move.canceled += OnMoveCanceled;
            _jump.canceled += OnJumpCanceled;
        }
    }
    
    public override void OnNetworkDespawn()
    {
        if (IsOwner)
        {
            _inputActions.Disable();
            _move.performed -= OnMovePerformed;
            _jump.performed -= OnJumpPerformed;
            _move.canceled -= OnMoveCanceled;
            _jump.canceled -= OnJumpCanceled;
        }
    }

    private void FixedUpdate()
    {
        if (IsOwner)
        {
            MovePlayer(_previousMovementInput);
        }
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        _previousMovementInput = context.ReadValue<Vector2>();
        _previousMovementInput.y = 0;
        _previousMovementInput = _previousMovementInput.normalized * _speed;
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        _previousMovementInput = Vector2.zero;
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        if (!_isGrounded)
        {
            return;
        }

        if (context.started)
        {
            _isGrounded = false;
            return;
        }

        PerformJump();
    }
    
    private void OnJumpCanceled(InputAction.CallbackContext obj)
    {
        PerformLand();
    }
    
    private void MovePlayer(Vector2 moveInput)
    {
        _rigidbody.linearVelocity = new Vector2(moveInput.x, _rigidbody.linearVelocity.y);
    }

    private void PerformJump()
    {
        _rigidbody.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
    }

    private void PerformLand()
    {
        _rigidbody.AddForce(Vector2.down * 10f, ForceMode2D.Impulse);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }
}

