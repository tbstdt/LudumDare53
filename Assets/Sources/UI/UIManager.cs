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

		private void Start()
		{
			_orders.text = $"0/{_maxOrders}";
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