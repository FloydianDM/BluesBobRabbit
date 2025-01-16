using Unity.Netcode;
using UnityEngine;

public class SoundManager : NetworkBehaviour
{
    public static SoundManager Instance;
    private AudioSource _audioSource;
    private AudioClip _audioClip;
    
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

    public void PlaySound(AudioClip clip)
    {
        _audioClip = clip;
        
        PlaySoundServerRpc();
    }

    [Rpc(SendTo.Server)]
    private void PlaySoundServerRpc()
    {
        Debug.Log("PlaySoundServerRpc");
            
        PlaySoundEveryoneRpc();
    }
    
    [Rpc(SendTo.Everyone)]
    private void PlaySoundEveryoneRpc()
    {
        _audioSource.PlayOneShot(_audioClip);
        Debug.Log("PlaySoundEveryoneRpc");
    }
}
