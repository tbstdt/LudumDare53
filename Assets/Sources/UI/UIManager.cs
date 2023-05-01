using System;
using System.Collections.Generic;
using DG.Tweening;
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
		[SerializeField] private Image m_imageOne;
		[SerializeField] private Image m_imageTwo;
		[SerializeField] private Image m_imageThree;
		[SerializeField] private Image m_imageMoney;
		[SerializeField] private Image m_imageMan;

		public int MaxOrders => _maxOrders;

		private void Start()
		{
			_orders.text = $"0/{_maxOrders}";
		}

		public void ShowRedMan()
		{
			RedAnim(m_imageMan);
		}

		public void ShowRedResource(ResourceType type) {
			RedAnim(GetImage(type));
		}

		private void RedAnim(Image image) {
			GameCore.Instance.Get<SoundManager>().PlaySound(SoundType.FX_NotEnough);
			image.DOKill();
			image.color = Color.white;
			image.DOColor(Color.red, 0.2f).OnComplete(()=>image.DOColor(Color.white, 0.2f).SetDelay(0.3f));
		}

		private Image GetImage(ResourceType type) {
			return type switch {
				ResourceType.One => m_imageOne,
				ResourceType.Two => m_imageTwo,
				ResourceType.Three => m_imageThree,
				ResourceType.Money => m_imageMoney,
				_ => null
			};
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