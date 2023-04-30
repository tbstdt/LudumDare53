using System;
using Sources.core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Editor.UI {
	public class EndGamePanel : MonoBehaviour, ICoreRegistrable {
		
		[SerializeField] private TMP_Text _endGameText;
		[SerializeField] private Button _exit;
		[SerializeField] private Button _restart;

		private void Start() {
			_exit.onClick.AddListener(AppUtils.Exit);
			_restart.onClick.AddListener(AppUtils.Restart);
			
			GameCore.Instance.Register<EndGamePanel>(this);
		}

		public void Show(bool gameResult) {
			gameObject.SetActive(true);
			_endGameText.text = gameResult ? "Congratulations! You won!" : "Unfortunately you lost";
		}
	}
}