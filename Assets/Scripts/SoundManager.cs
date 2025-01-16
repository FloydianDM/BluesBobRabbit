using Unity.Netcode;
using UnityEngine;

public class SoundManager : NetworkBehaviour
{
    [SerializeField] private AudioClip _tauntAudioClip;
    
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
    public void PlaySoundServerRpc(SoundType soundType)
    {
        Debug.Log("PlaySoundServerRpc");
        
        switch (soundType)
        {
            case SoundType.TauntSound:
                PlayTauntSoundEveryoneRpc();
                break;
        }
    }

    [Rpc(SendTo.Everyone)]
    private void PlayTauntSoundEveryoneRpc()
    {
        _audioSource.PlayOneShot(_tauntAudioClip);
        
        Debug.Log("PlaySoundEveryoneRpc");
    }
}
