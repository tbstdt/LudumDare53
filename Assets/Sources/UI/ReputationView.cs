using System;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Editor.UI {
	public class ReputationView : MonoBehaviour{
		[SerializeField] private Sprite[] _emojies;
		[SerializeField] private Image _reputationView;

		public void UpdateReputation(int value) {
			var emojiIndex = Math.Clamp(value, 0, _emojies.Length - 1);
			_reputationView.sprite = _emojies[emojiIndex];
		}
	}
}