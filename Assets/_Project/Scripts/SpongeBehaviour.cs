using System;
using UnityEngine;
using UnityEngine.Events;

public class SpongeBehaviour : MonoBehaviour, IAudible
{
    public UnityEvent onCleanAudio;
    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent(out IWashable washable);
        washable?.Wash();
        onCleanAudio.Invoke();
    }

    public void SetAudioBehaviour(AudioBehaviour audioBehaviour)
    {
        onCleanAudio.AddListener(audioBehaviour.PlayAudio);
    }

    public void RemoveAudioBehaviour(AudioBehaviour audioBehaviour)
    {
        onCleanAudio.RemoveListener(audioBehaviour.PlayAudio);
    }
}
