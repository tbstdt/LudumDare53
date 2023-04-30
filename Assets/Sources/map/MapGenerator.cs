using System;
using System.Collections.Generic;
using Sources.core;
using UnityEngine;

namespace Sources.map {
	public class MapGenerator: MonoBehaviour, ICoreRegistrable, ICoreInit {
		[SerializeField] private List<MapPoint> _points;
		[SerializeField] private Transform _container;

		private ObjectsStorage _objectStorage;

		private void SetObject(out ObjectOnMap objectOnMap, ObjectType type, GameObject point)
		{
			objectOnMap = _objectStorage.GetObjectByType(type);
			objectOnMap.transform.SetParent(_container);
			objectOnMap.transform.position = point.transform.position;
			objectOnMap.AddMapPoint(point);
		}

		public void Init() {
			_objectStorage = GameCore.Instance.Get<ObjectsStorage>();
			foreach (var mapPoint in _points)
				SetObject(out var objectOnMap, mapPoint.Type, mapPoint.Point);
		}

		public ObjectOnMap SpawnRandomObject(GameObject point)
		{
			SetObject(out var objectOnMap, ObjectType.Resource | ObjectType.Random, point);
			return objectOnMap;
		}
	}

	[Serializable]
	public class MapPoint {
		public ObjectType Type;
		public GameObject Point;
	}
}