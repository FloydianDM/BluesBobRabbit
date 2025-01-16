using Unity.Netcode;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _joinMenuUI;
    [SerializeField] private GameObject _deathUI;

    public static UIManager Instance;
    public DeathUI DeathUI { private set; get; }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        DeathUI = _deathUI.GetComponent<DeathUI>();
        _joinMenuUI.SetActive(true);
        _deathUI.SetActive(false);
    }

    public void DisableJoinMenu()
    {
        _joinMenuUI.SetActive(false);
        _deathUI.SetActive(true);
    }
}
