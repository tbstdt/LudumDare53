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
		[SerializeField] private Button _restartButton;
		[SerializeField] private Button _exitButton;

		[SerializeField] private GameObject _settingsPanel;

		[SerializeField] private AudioMixer _audioMixer;

		private void Start() {
			_settingsButton.onClick.AddListener(() => {
				Time.timeScale = 0;
				_settingsPanel.gameObject.SetActive(!_settingsPanel.activeSelf);
			});

			setVolume("Sounds", _soundSlider);
			setVolume("Music", _musicSlider);

			_soundSlider.onValueChanged.AddListener(_ => setVolume("Sounds", _soundSlider));
			_musicSlider.onValueChanged.AddListener(_ => setVolume("Music", _musicSlider));

			_resumeButton.onClick.AddListener(() => {
				Time.timeScale = 1;
				_settingsPanel.SetActive(false);
			});
			_restartButton.onClick.AddListener(AppUtils.Restart);
			_exitButton.onClick.AddListener(AppUtils.Exit);
		}

		private void setVolume(string id, Slider slider) {
			_audioMixer.SetFloat(id, Mathf.Log10(slider.value) * 20);
		}
	}
}