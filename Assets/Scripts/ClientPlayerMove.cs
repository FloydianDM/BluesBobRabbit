using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClientPlayerMove : NetworkBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerInput _playerInput;
    
    private void Awake()
    {
        _playerMovement.enabled = false;
        _playerInput.enabled = false;
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        
        enabled = IsClient;

        if (!IsOwner)
        {
            enabled = false;
            _playerMovement.enabled = false;
            _playerInput.enabled = false;

            return;
        }
        
        _playerMovement.enabled = true;
        _playerInput.enabled = true;
    }
}
