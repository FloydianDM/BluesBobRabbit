using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class JoinMenuUI : NetworkBehaviour
{
    [SerializeField] private Button _hostButton;
    [SerializeField] private Button _clientButton;
    
    public void HostGame()
    {
        // create host
        NetworkManager.Singleton.StartHost();
        
        _hostButton.interactable = false;
        _clientButton.interactable = false;
        UIManager.Instance.DisableJoinMenu();   
    }

    public void JoinGame()
    {
        // create client
        NetworkManager.Singleton.StartClient();
        
        _hostButton.interactable = false;
        _clientButton.interactable = false;
        UIManager.Instance.DisableJoinMenu();
    }
}
    

