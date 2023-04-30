using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
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
		
		[SerializeField] private Transform  _startPosition;
		[SerializeField] private Transform  _endPosition;

		[SerializeField] private float _timeToLiveResource = 1f;
		private List<Resource> _resources;


		public override ObjectType Type => ObjectType.Man;

		public List<Resource> Resources {
			get => _resources;
			set {
				_resources = value;

				StartCoroutine(ShowGatherAnimation());
			}
		}
		
		private IEnumerator ShowGatherAnimation() {

			if (_resources == null) {
				yield break;
			}

			yield return new WaitForSeconds(0.5f);
			
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
				
				yield return new WaitForSeconds(0.5f);
			}
		}

		private void showResource(GameObject resource) {
			resource.SetActive(true);
			resource.transform.position = _startPosition.transform.position;
			resource.transform.DOMoveY(_endPosition.transform.position.y, _timeToLiveResource).OnComplete(()=>resource.SetActive(false));
		}
		
		protected override void OnObjectClicked() { }
	}
}