using System;
using System.Collections.Generic;
using Sources.core;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Editor.UI {
	public class UIManager : MonoBehaviour, ICoreRegistrable{
		[SerializeField] private Text _resourceOne;
		[SerializeField] private Text _resourceTwo;
		[SerializeField] private Text _resourceThree;
		[SerializeField] private Text _resourceMoney;
		[SerializeField] private Text _mans;


		public void UpdateResource(Dictionary<ResourceType, int> resources) {
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
			}
		}
	}
}