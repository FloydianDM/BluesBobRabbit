using Unity.Netcode;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [SerializeField] private AudioClip _tauntAudioClip;

    public void SendSound(SoundType soundType)
    {
        switch (soundType)
        {
            case SoundType.TauntSound:
                SoundManager.Instance.PlaySound(_tauntAudioClip);
                break;
        }
    }
}
