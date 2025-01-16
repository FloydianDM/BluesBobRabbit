using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public void SendSound(SoundType soundType)
    {
        SoundManager.Instance.PlaySoundServerRpc(soundType);
    }
}
