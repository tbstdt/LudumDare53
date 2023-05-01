using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Sources.Editor.ObjectsOnMap {
	public class Man : ObjectOnMap {

		[SerializeField] private GameObject _resourceOne;
		[SerializeField] private GameObject _resourceTwo;
		[SerializeField] private GameObject _resourceThree;
		[SerializeField] private GameObject _resourceMoney;
		
		[SerializeField] private TMP_Text _resourceTextOne;
		[SerializeField] private TMP_Text _resourceTextTwo;
		[SerializeField] private TMP_Text _resourceTextThree;
		[SerializeField] private TMP_Text _resourceTextMoney;

		private List<Resource> _resources;


		public List<Resource> Resources {
			get => _resources;
			set {
				_resources = value;

				showGatherAnimation();
			}
		}
		
		private void showGatherAnimation() {
			_resourceOne.SetActive(false);
			_resourceTwo.SetActive(false);
			_resourceThree.SetActive(false);
			_resourceMoney.SetActive(false);

			if (_resources == null) {
				return;
			}

			foreach (var resource in _resources) {
				switch (resource.Type) {
					case ResourceType.One:
						showResource(_resourceOne);
						_resourceTextOne.text = resource.Amount.ToString();
						break;
					case ResourceType.Two:
						showResource(_resourceTwo);
						_resourceTextTwo.text = resource.Amount.ToString();
						break;
					case ResourceType.Three:
						showResource(_resourceThree);
						_resourceTextThree.text = resource.Amount.ToString();
						break;
					case ResourceType.Money:
						showResource(_resourceMoney);
						_resourceTextMoney.text = resource.Amount.ToString();
						break;
				}
			}
		}

		private void showResource(GameObject resource) {
			resource.SetActive(true);
		}
		
		protected override void OnObjectClicked() { }
	}
}