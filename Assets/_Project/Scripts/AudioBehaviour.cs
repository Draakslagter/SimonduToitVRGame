using System.Collections.Generic;
using UnityEngine;

public class AudioBehaviour : MonoBehaviour
{
   private IAudible _parentObject;
   protected AudioSource _audioSource;
   [SerializeField] protected float pitchMin = 0.9f;
   [SerializeField] protected float pitchMax = 1.1f;
   [SerializeField] protected List<AudioClip> generalAudioClips;

   protected virtual void Awake()
   {
      _parentObject ??= GetComponent<IAudible>();
      if (_audioSource == null)
      {
         _audioSource = GetComponent<AudioSource>();
      }
   }
   private void Start()
   {
      _parentObject?.SetAudioBehaviour(this);
   }

   private void OnDisable()
   {
      _parentObject?.RemoveAudioBehaviour(this);
   }
   
   public virtual void PlayAudio()
   {
      _audioSource.pitch = Random.Range(pitchMin, pitchMax);
      _audioSource.PlayOneShot(generalAudioClips[Random.Range(0, generalAudioClips.Count - 1)]);
   }
   public virtual void PlayAudio(bool objectBroken)
   {
      _audioSource.pitch = Random.Range(pitchMin, pitchMax);
   }
}
