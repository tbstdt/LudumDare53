using System;
using System.Collections.Generic;
using Sources.core;
using UnityEngine;

namespace Sources.Editor {
	public class SoundManager : MonoBehaviour, ICoreRegistrable{
		[SerializeField] private AudioSource _sound;
		[SerializeField] private AudioSource _music;

		[SerializeField] private List<Sound> _sounds;

		private Dictionary<SoundType, AudioClip> _soundTypes = new();


		private void Start() {
			foreach (var sound in _sounds) {
				_soundTypes.Add(sound.Type, sound.clip);
			}

			if (_soundTypes.TryGetValue(SoundType.music, out AudioClip clip)) {
				_music.clip = clip;
				_music.Play();
			}
		}

		public void PlaySound(SoundType type) {
			if (_soundTypes.TryGetValue(type, out var clip)) {
				_sound.PlayOneShot(clip);
			}
		}
	}

	[Serializable]
	public class Sound {
		public SoundType Type;
		public AudioClip clip;
	}
	
	public enum SoundType {
		one,
		music,
	}
}