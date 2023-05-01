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

		public void Show(bool gameResult) {
			gameObject.SetActive(true);
			_endGameText.text = gameResult ? "Congratulations! You won!" : "Unfortunately you lost";
			
			GameCore.Instance.Get<SoundManager>().PlaySound(gameResult ? SoundType.FX_Win : SoundType.FX_Fail);
		}

		public void Init() {
			_exit.onClick.AddListener(AppUtils.Exit);
			_restart.onClick.AddListener(AppUtils.Restart);
		}
	}
}