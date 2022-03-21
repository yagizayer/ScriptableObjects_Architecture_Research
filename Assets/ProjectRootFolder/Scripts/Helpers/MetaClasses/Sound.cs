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
        public string name;
        public bool SelectFromList;

        [ConditionalField(nameof(SelectFromList), true)]
        public AudioClip clip;

        [ConditionalField(nameof(SelectFromList))]
        public AudioClips clips;

        [Range(0f, 1f)] public float volume;
        [Range(.1f, 3f)] public float pitch;
        public bool loop;

        public bool playOnAwake = false;

        [HideInInspector] public AudioSource Source;
        public bool ChangeWithPause=false;
        [ConditionalField(nameof(ChangeWithPause))]
        [Range(0f, 1f)] public float PauseVolume;
        [ConditionalField(nameof(ChangeWithPause))]
        [Range(.1f, 3f)] public float PausePitch;

    }

    [Serializable]
    public class AudioClips
    {
        public List<AudioClip> clips;

        public AudioClip GetRandom()
        {
            if (clips.Count == 0) return null;
            return clips[Random.Range(0, clips.Count)];
        }
    }
}
