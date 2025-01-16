using System;
using Unity.Netcode;
using UnityEngine;

public class PlayerHealth : NetworkBehaviour
{
    private NetworkVariable<int> _health = new NetworkVariable<int>(100, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public Action<int> OnHealthChanged; 

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        _health.OnValueChanged += Health_OnValueChanged;
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();

        _health.OnValueChanged -= Health_OnValueChanged;
    }

    private void Health_OnValueChanged(int previousValue, int newValue)
    {
        OnHealthChanged?.Invoke(newValue);

        if (newValue <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // TODO: player died
    }
}
