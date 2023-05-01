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

			if (_soundTypes.TryGetValue(SoundType.Music, out AudioClip clip)) {
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

	[Flags]
	public enum SoundType {
		Music = 0,
		FX_Enough = 1,
		FX_Positive = 1 << 1,
		FX_NotEnough = 1 << 2,
		FX_Fail = 1 << 3,
		FX_Neutral = 1 << 4,
		
		Alien = 1 << 5,
		Mutant = 1 << 6,
		Robot = 1 << 7,
		Biker = 1 << 8,
		Cowboy = 1 << 9,
		
		Neutral = 1 << 10,
		Negative = 1 << 11,
		Positive = 1 << 12,
	}
}