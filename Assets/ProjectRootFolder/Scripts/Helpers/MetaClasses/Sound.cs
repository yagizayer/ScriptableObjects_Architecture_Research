using System;
using System.Collections.Generic;
using MyBox;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Helpers
{
    [Serializable]
    public class Sound
    {
        [SerializeField] private bool selectFromList;

        [ConditionalField(nameof(selectFromList), true), SerializeField]
        private AudioClip clip;

        [ConditionalField(nameof(selectFromList)), SerializeField]
        private AudioClips clips;

        [HideInInspector] public AudioSource audioSource;

        [Range(0f, 1f)] public float volume = .5f;
        [Range(.1f, 3f)] public float pitch = 1;

        public bool loop = false;
        public bool playOnAwake = false;

        //--------------------------------------------------------------------------------------------------------------

        public Sound()
        {
            loop = false;
            playOnAwake = false;
            volume = .5f;
            pitch = 1f;
        }
        
        //--------------------------------------------------------------------------------------------------------------
        
        public AudioClip Clip => GetClip();
        public bool IsPlaying => audioSource.isPlaying;
        public bool SelectFromList => selectFromList;

        //--------------------------------------------------------------------------------------------------------------

        private AudioClip GetClip() => SelectFromList ? clips.GetRandomClip() : clip;
    }

    [Serializable]
    public class AudioClips
    {
        public List<AudioClip> clips;

        public AudioClip GetRandomClip() => clips.Count == 0 ? null : clips[Random.Range(0, clips.Count)];
    }
}