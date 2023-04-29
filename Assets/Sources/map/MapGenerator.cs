using System;
using System.Collections.Generic;
using Sources.core;
using UnityEngine;

namespace Sources.map {
	public class MapGenerator: MonoBehaviour, ICoreRegistrable, ICoreInit {
		[SerializeField] private List<MapPoint> _points;

		public void Init() {
			var objectStorage = GameCore.Instance.Get<ObjectsStorage>();
			foreach (var mapPoint in _points) {
				var template = objectStorage.GetObjectByType(mapPoint.Type);
				var objectOnMap = Instantiate(template);
				objectOnMap.transform.position = mapPoint.Point.transform.position;
			}
		}
	}

	[Serializable]
	public class MapPoint {
		public ObjectType Type;
		public GameObject Point;
	}
}