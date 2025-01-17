using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerControl : NetworkBehaviour
{
    [SerializeField] private float _originalSpeed = 15f;
    [SerializeField] private float _modifiedSpeed = 20f;

    private float _speed;
    private Rigidbody2D _rigidbody;
    private PlayerSound _playerSound;
    private SpeedHandler _speedHandler;
    private BluesBobRabbitInput _inputActions;
    private InputAction _move;
    private InputAction _jump;
    private InputAction _taunt;
    private Vector2 _recentMovementInput;
    private bool _isGrounded;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerSound = GetComponent<PlayerSound>();
        _speedHandler = GetComponent<SpeedHandler>();
        
        _inputActions = new BluesBobRabbitInput();
        _jump = _inputActions.FindAction("Jump");
        _move = _inputActions.FindAction("Move");
        _taunt = _inputActions.FindAction("Taunt");
    }

    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
        {
            return;
        }
        
        _inputActions.Enable();
        _move.performed += OnMovePerformed;
        _jump.performed += OnJumpPerformed;
        _taunt.performed += OnTauntPerformed;
        _move.canceled += OnMoveCanceled;
        _jump.canceled += OnJumpCanceled;
        
        StaticEventHandler.OnPickupPicked += StaticEventHandler_OnPickupPicked;
    }
    
    public override void OnNetworkDespawn()
    {
        if (!IsOwner)
        {
            return;
        }

        _inputActions.Disable();
        _move.performed -= OnMovePerformed;
        _jump.performed -= OnJumpPerformed;
        _taunt.performed -= OnTauntPerformed;
        _move.canceled -= OnMoveCanceled;
        _jump.canceled -= OnJumpCanceled;
        
        StaticEventHandler.OnPickupPicked -= StaticEventHandler_OnPickupPicked;
    }

    private void Update()
    {
        if (IsOwner)
        {
            if (_speedHandler.IsAllowSpeedIncrease)
            {
                _speed = _modifiedSpeed;
            }
            else
            {
                _speed = _originalSpeed;
            }
        }
    }
    
    private void FixedUpdate()
    {
        if (IsOwner)
        {
            MovePlayer(_recentMovementInput);
        }
    }
    
    private void StaticEventHandler_OnPickupPicked(PickupPickedEventArgs args)
    {
        switch (args.PickupType)
        {
            case PickupType.SpeedPickup:
                // add speed for player
                Debug.Log("Picked up Power");
                _speedHandler.AddSpeed();

                if (IsHost)
                {
                    GameManager.Instance.ChangeHostScoreServerRpc();
                }
                else
                {
                    GameManager.Instance.ChangeClientScoreServerRpc();
                }
                break;
        }
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        _recentMovementInput = context.ReadValue<Vector2>();
        _recentMovementInput.y = 0;
        _recentMovementInput = _recentMovementInput.normalized * _speed;
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        _recentMovementInput = Vector2.zero;
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
    
    private void OnTauntPerformed(InputAction.CallbackContext obj)
    {
        _playerSound.SendSound(SoundType.TauntSound);
    }
    
    private void MovePlayer(Vector2 moveInput)
    {
        _rigidbody.linearVelocity = new Vector2(moveInput.x, _rigidbody.linearVelocity.y);
    }

    private void PerformJump()
    {
        _rigidbody.AddForceY(20f, ForceMode2D.Impulse);
    }

    private void PerformLand()
    {
        _rigidbody.AddForceY(-30f, ForceMode2D.Impulse);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DeathPool"))
        {
            if (IsHost)
            {
                GameManager.Instance.ChangeClientScoreServerRpc();
            }
            else
            {
                GameManager.Instance.ChangeHostScoreServerRpc();
            }
            
            ulong playerId = NetworkManager.Singleton.LocalClientId;
                
            StaticEventHandler.CallPlayerDeathEvent(playerId);
        }
    }
}

