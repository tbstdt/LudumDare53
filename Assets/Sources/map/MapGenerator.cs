using System;
using System.Collections.Generic;
using Sources.core;
using UnityEngine;

namespace Sources.map {
	public class MapGenerator: MonoBehaviour, ICoreRegistrable, ICoreInit {
		[SerializeField] private List<MapPoint> _points;
		[SerializeField] private Transform _container;

		public void Init() {
			var objectStorage = GameCore.Instance.Get<ObjectsStorage>();
			foreach (var mapPoint in _points) {
				var objectOnMap = objectStorage.GetObjectByType(mapPoint.Type);
				objectOnMap.transform.SetParent(_container);
				objectOnMap.transform.position = mapPoint.Point.transform.position;
				objectOnMap.AddMapPoint(mapPoint.Point);
			}
		}
	}

	[Serializable]
	public class MapPoint {
		public ObjectType Type;
		public GameObject Point;
	}
}