using System;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Editor.UI {
	public class ReputationView : MonoBehaviour{
		private static readonly int ANIMATOR_REPUTATION = Animator.StringToHash("reputation");
		
		[SerializeField] private Sprite[] _emojies;
		[SerializeField] private Image _reputationView;

		[SerializeField] private Animator _animator;

		public void UpdateReputation(int value) {
			var emojiIndex = Math.Clamp(value, 0, _emojies.Length - 1);
			_reputationView.sprite = _emojies[emojiIndex];

			_animator.SetInteger(ANIMATOR_REPUTATION, value);
		}
	}
}