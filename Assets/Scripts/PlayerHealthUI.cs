using System;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _healthText;
    private PlayerHealth _playerHealth;

    private void Awake()
    {
        _playerHealth = GetComponent<PlayerHealth>();
    }

    private void OnEnable()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnectedCallback;
    }
    
    private void OnDisable()
    {        
        NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnectedCallback;
    }

    private void OnClientConnectedCallback(ulong _)
    {
        _playerHealth.OnHealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged(int newHealth)
    {
        _healthText.text = newHealth.ToString();
    }
}
