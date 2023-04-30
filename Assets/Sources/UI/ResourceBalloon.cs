using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Sources.Editor.UI {
	public class ResourceBalloon : MonoBehaviour {
		[SerializeField] private ResourceView _template;
		[SerializeField] private Transform _poolContainer;
		[SerializeField] private Transform _container;
		[SerializeField] private int _padding;
		[SerializeField] private int _height;
		[SerializeField] private float _duration;

		private List<ResourceView> _pool = new ();

		public void Show(List<Resource> resources) {
			if (resources == null) {
				return;
			}
			
			for (int index = 0; index < resources.Count; index++) {
				Resource resource = resources[index];
				var view = getResourceView();

				view.init(resource);

				view.transform.localPosition = new Vector3(0, _padding * index, 0);

				view.transform.DOMoveY(view.transform.position.y + _height, _duration).OnComplete(()=>hideView(view));
			}
		}

		private ResourceView getResourceView() {
			if (_pool.Count > 0) {
				var view = _pool[0];
				_pool.Remove(view);
				view.transform.SetParent(_container);
				view.transform.position = Vector3.zero;
				return view;
			}

			return Instantiate(_template, _container);
		}

		private void hideView(ResourceView view) {
			view.transform.SetParent(_poolContainer);
			_pool.Add(view);
		}

	}
}