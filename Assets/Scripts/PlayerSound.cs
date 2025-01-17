using Unity.Netcode;

public class PlayerSound : NetworkBehaviour
{
    public void SendSound(SoundType soundType)
    {
        if (IsHost)
        {
            SoundManager.Instance.PlaySoundServerRpc(soundType, PlayerType.Host);
        }
        else
        {
            SoundManager.Instance.PlaySoundServerRpc(soundType, PlayerType.Client);
        }
    }
}
