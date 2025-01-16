using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public void SendSound(SoundType soundType)
    {
        switch (soundType)
        {
            case SoundType.TauntSound:
                SoundManager.Instance.PlaySoundServerRpc(soundType);
                break;
        }
    }
}
