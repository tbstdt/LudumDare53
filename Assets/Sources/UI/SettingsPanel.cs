using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Sources.Editor.UI {
	public class SettingsPanel : MonoBehaviour {
		[SerializeField] private Button _settingsButton;

		[SerializeField] private Slider _soundSlider;
		[SerializeField] private Slider _musicSlider;

		[SerializeField] private Button _resumeButton;
		[SerializeField] private Button _exitButton;

		[SerializeField] private GameObject _settingsPanel;

		[SerializeField] private AudioMixer _audioMixer;

		private void Start() {
			_settingsButton.onClick.AddListener(() => _settingsPanel.gameObject.SetActive(!_settingsPanel.activeSelf));

			setVolume("Sounds", _soundSlider);
			setVolume("Music", _musicSlider);

			_soundSlider.onValueChanged.AddListener(_ => setVolume("Sounds", _soundSlider));
			_musicSlider.onValueChanged.AddListener(_ => setVolume("Music", _musicSlider));

			_resumeButton.onClick.AddListener(() => _settingsPanel.SetActive(false));
			_exitButton.onClick.AddListener(() => {
#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
#else
				Application.Quit();

#endif
			});
		}

		private void setVolume(string id, Slider slider) {
			_audioMixer.SetFloat(id, Mathf.Log10(slider.value) * 20);
		}
	}
}