using UnityEngine;
using UnityEngine.Events;

public class WaterBehaviour : MonoBehaviour,IAudible
{
    public UnityEvent onWetAudio;
    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent(out IWashable washable);
        washable?.Wet();
        onWetAudio?.Invoke();
    }

    public void SetAudioBehaviour(AudioBehaviour audioBehaviour)
    {
        onWetAudio.AddListener(audioBehaviour.PlayAudio);
    }

    public void RemoveAudioBehaviour(AudioBehaviour audioBehaviour)
    {
        onWetAudio.RemoveListener(audioBehaviour.PlayAudio);
    }
}
