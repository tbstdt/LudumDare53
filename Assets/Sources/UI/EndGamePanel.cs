using System;
using Sources.core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Editor.UI {
	public class EndGamePanel : MonoBehaviour, ICoreRegistrable, ICoreInit {
		
		[SerializeField] private TMP_Text _endGameText;
		[SerializeField] private Button _exit;
		[SerializeField] private Button _restart;
		[SerializeField] private Image _winImage;
		[SerializeField] private Image _loseImage;
		

		public void Show(bool gameResult) {
			_winImage.gameObject.SetActive(gameResult);
			_loseImage.gameObject.SetActive(!gameResult);
			
			gameObject.SetActive(true);
			_endGameText.text = gameResult ? "Delivery master!" : "Delivery disaster!";
			
			GameCore.Instance.Get<SoundManager>().PlaySound(gameResult ? SoundType.FX_Win : SoundType.FX_Fail);
		}

		public void Init() {
			_exit.onClick.AddListener(AppUtils.Exit);
			_restart.onClick.AddListener(AppUtils.Restart);
		}
	}
}