using Helpers;
using ProjectRootFolder.Scripts.Consts;
using UnityEngine;

namespace ProjectRootFolder.Scripts.Managers
{
	public sealed class SoundManager : MonoBehaviour
	{
		[SerializeField] private SoundsDictionary sounds = new SoundsDictionary();

		private void Awake() => Initialize();

		private void Initialize()
		{
			foreach (var kvPair in sounds)
			{
				var child = new GameObject(kvPair.Key);
				child.transform.SetParent(transform);
				child.transform.Reset();
				
				var childAs = child.AddComponent<AudioSource>();
				childAs.volume = kvPair.Value.volume;
				childAs.pitch = kvPair.Value.pitch;
				childAs.clip = kvPair.Value.Clip;
				childAs.loop = kvPair.Value.loop;
				childAs.playOnAwake = kvPair.Value.playOnAwake;
				
				kvPair.Value.audioSource = childAs;
			}
		}

		public void Play(string soundName)
		{
			var sound = sounds[soundName];
			
			if(sound.IsPlaying) return;
			sound.audioSource.Play();
		}
		
		public void PlayOneShot(string soundName)
		{
			var sound = sounds[soundName];
			
			if(sound.IsPlaying) return;
			sound.audioSource.PlayOneShot(sound.Clip);
		}
		
		public void Stop(string soundName)
		{
			var sound = sounds[soundName];
			sound.audioSource.Stop();
		}
		
		public void Pause(string soundName)
		{
			var sound = sounds[soundName];
			sound.audioSource.Pause();
		}
		
		public void UnPause(string soundName)
		{
			var sound = sounds[soundName];
			sound.audioSource.UnPause();
		}
		
		public void ChangeVolume(string soundName, float value)
		{
			var sound = sounds[soundName];
			sound.audioSource.volume = value;
		}
		
		public void ChangePitch(string soundName, float value)
		{
			var sound = sounds[soundName];
			sound.audioSource.pitch = value;
		}
		
		public bool IsPlaying(string soundName)
		{
			var sound = sounds[soundName];
			return sound.IsPlaying;
		}

		public void Replay(string soundName)
		{
			var sound = sounds[soundName];
			sound.audioSource.Stop();
			sound.audioSource.Play();
		}
		
		public void NewClip(string soundName)
		{
			var sound = sounds[soundName];
			if (!sound.SelectFromList)
			{
				Debug.LogError("SelectFromList is false");
				return;
			}
			sound.audioSource.clip = sound.Clip;
		}
		public void NewClip(string soundName, AudioClip clip)
		{
			var sound = sounds[soundName];
			sound.audioSource.clip = clip;
		}
	}
}
