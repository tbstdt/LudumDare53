using System;
using System.Collections.Generic;
using Sources.core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Editor.UI {
	public class UIManager : MonoBehaviour, ICoreRegistrable{
		[SerializeField] private TextMeshProUGUI _resourceOne;
		[SerializeField] private TextMeshProUGUI _resourceTwo;
		[SerializeField] private TextMeshProUGUI _resourceThree;
		[SerializeField] private TextMeshProUGUI _resourceMoney;
		[SerializeField] private TextMeshProUGUI _mans;
		[Space]
		[SerializeField] private int _maxOrders = 50;
		[SerializeField] private TextMeshProUGUI _orders;
		[Space]
		[SerializeField] private Animator m_animatorOne;
		[SerializeField] private Animator m_animatorTwo;
		[SerializeField] private Animator m_animatorThree;
		[SerializeField] private Animator m_animatorMoney;
		[SerializeField] private Animator m_animatorMan;

		private readonly string m_triggerName = "showRed";

		private void Start()
		{
			_orders.text = $"0/{_maxOrders}";
		}

		public void ShowRedMan()
		{
			m_animatorMan.SetTrigger(m_triggerName);
		}

		public void ShowRedResource(ResourceType type)
		{
			switch (type) {
				case ResourceType.One:
					m_animatorOne.SetTrigger(m_triggerName);
					break;
				case ResourceType.Two:
					m_animatorTwo.SetTrigger(m_triggerName);
					break;
				case ResourceType.Three:
					m_animatorThree.SetTrigger(m_triggerName);
					break;
				case ResourceType.Money:
					m_animatorMoney.SetTrigger(m_triggerName);
					break;
			}
		}

		public void UpdateResource(Dictionary<ResourceType, int> resources, int workersCount, int workersMax) {
			foreach (var resource in resources) {
				switch (resource.Key) {
					case ResourceType.One:
						_resourceOne.text = resource.Value.ToString();
						break;
					case ResourceType.Two:
						_resourceTwo.text = resource.Value.ToString();
						break;
					case ResourceType.Three:
						_resourceThree.text = resource.Value.ToString();
						break;
					case ResourceType.Money:
						_resourceMoney.text = resource.Value.ToString();
						break;
				}

				_mans.text = $"{workersCount}/{workersMax}";
			}
		}

		public void UpdateOrders(int currOrders)
		{
			_orders.text = $"{currOrders}/{_maxOrders}";

			if (currOrders == _maxOrders) {
				GameCore.Instance.Get<EndGamePanel>().Show(true);
			}
		}
	}
}