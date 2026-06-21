using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioBehavior_BreakableObject : AudioBehaviour
{
    [SerializeField] private List<AudioClip> hitAudioClips;
    [SerializeField] private List<AudioClip> breakAudioClips;

   

    public override void PlayAudio(bool objectBroken)
    {
        base.PlayAudio(objectBroken);
        _audioSource.PlayOneShot(objectBroken
            ? breakAudioClips[Random.Range(0, breakAudioClips.Count - 1)]
            : hitAudioClips[Random.Range(0, hitAudioClips.Count - 1)]);
    }
    
}
