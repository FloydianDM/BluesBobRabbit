using Unity.Netcode;

public class GameManager : NetworkBehaviour
{
    private NetworkVariable<int> _hostScore = new NetworkVariable<int>(0);
    private NetworkVariable<int> _clientScore = new NetworkVariable<int>(0);
    
    public static GameManager Instance;
    
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
        
        StaticEventHandler.OnPlayerDeath += StaticEventHandler_OnPlayerDeath;
    }
    
    public override void OnNetworkSpawn()
    {
        UIManager.Instance.ScoreUI.UpdatePlayer1ScoreCountText(0);
        UIManager.Instance.ScoreUI.UpdatePlayer2ScoreCountText(0);

        _hostScore.OnValueChanged += HostScore_ValueChanged;
        _clientScore.OnValueChanged += ClientScore_ValueChanged;
    }

    public override void OnNetworkDespawn()
    {
        StaticEventHandler.OnPlayerDeath -= StaticEventHandler_OnPlayerDeath;
        
        _hostScore.OnValueChanged -= HostScore_ValueChanged;
        _clientScore.OnValueChanged -= ClientScore_ValueChanged;
    }

    private void HostScore_ValueChanged(int previousValue, int newValue)
    {
        UIManager.Instance.ScoreUI.UpdatePlayer1ScoreCountText(newValue);
    }

    private void ClientScore_ValueChanged(int previousValue, int newValue)
    {
        UIManager.Instance.ScoreUI.UpdatePlayer2ScoreCountText(newValue);
    }
    
    private void StaticEventHandler_OnPlayerDeath(PlayerDeathEventArgs args)
    {
        NetworkObject playerNetworkObject = NetworkManager.Singleton.SpawnManager.GetPlayerNetworkObject(args.PlayerId);
        
        playerNetworkObject.transform.position = transform.position;
    }
    
    [Rpc(SendTo.Server)]
    public void ChangeClientScoreServerRpc()
    {
        _clientScore.Value++;
    }
    
    [Rpc(SendTo.Server)]
    public void ChangeHostScoreServerRpc()
    {
        _hostScore.Value++;
    }
}
