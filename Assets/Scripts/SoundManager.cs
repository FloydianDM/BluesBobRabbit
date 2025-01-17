using Unity.Netcode;
using UnityEngine;
using UnityEngine.Serialization;

public class SoundManager : NetworkBehaviour
{
    [SerializeField] private AudioClip _tauntHostAudioClip;
    [SerializeField] private AudioClip _tauntClientAudioClip;
    
    public static SoundManager Instance;
    private AudioSource _audioSource;
    
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
        
        _audioSource = GetComponent<AudioSource>();
    }
    
    [Rpc(SendTo.Server)]
    public void PlaySoundServerRpc(SoundType soundType, PlayerType playerType)
    {
        switch (soundType)
        {
            case SoundType.TauntSound:
                PlayTauntSoundEveryoneRpc(playerType);
                break;
        }
    }

    [Rpc(SendTo.Everyone)]
    private void PlayTauntSoundEveryoneRpc(PlayerType playerType)
    {
        switch (playerType)
        {
            case PlayerType.Host:
                _audioSource.PlayOneShot(_tauntHostAudioClip);
                break;
            case PlayerType.Client:
                _audioSource.PlayOneShot(_tauntClientAudioClip);
                break;
        }
    }
}
