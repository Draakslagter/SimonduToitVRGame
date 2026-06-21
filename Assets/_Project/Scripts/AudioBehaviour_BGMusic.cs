using UnityEngine;

public class AudioBehaviour_BGMusic : AudioBehaviour
{
    public static AudioBehaviour_BGMusic Instance;
    [SerializeField] private AudioSource secretAudioSource;

    protected override void Awake()
    {
        base.Awake();
        if (Instance != null &&  Instance != this)
        {
            Destroy(this);

        }
        else
        {
            Instance = this;
        }
    }

    public void ChangeMusic(float amount)
    {
        
        _audioSource.volume = 1 - (amount * 0.0001f);
        _audioSource.pitch = 1 - (amount * 0.0001f);
        if (_audioSource.volume <= 0)
        {
            _audioSource.Stop();
        }
        secretAudioSource.volume = amount * 0.0001f;;
    }
}
