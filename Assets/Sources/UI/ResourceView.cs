using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Sources.Editor.UI {
	public class ResourceView : MonoBehaviour {
		[SerializeField] private TMP_Text _value;
		[SerializeField] private List<ResourceViewData> _viewDatas;

		public void init(Resource resource) {
			foreach (var viewData in _viewDatas) {
				viewData.Image.gameObject.SetActive(false);
			}
			
			var data = _viewDatas.FirstOrDefault(x => x.Type == resource.Type);

			data?.Image.gameObject.SetActive(true);

			_value.text = resource.Amount.ToString();
		}
	}

	[Serializable]
	public class ResourceViewData {
		public Image Image;
		public ResourceType Type;
	}
}