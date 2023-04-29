using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Sources.core;
using UnityEngine;

namespace Sources.map {
	public class MapManager : MonoBehaviour, ICoreRegistrable {
		[SerializeField] private List<GameObject> _mapPoints;
		[SerializeField] private Transform _manContainer;

		private List<GraphNode> _graphNodes = new();
		
		private void Start() {
			foreach (GameObject p in _mapPoints)
			{
				_graphNodes.Add(new GraphNode(p));
			}
			
			// связываем соседние вершины
			foreach (GraphNode node in _graphNodes)
			{
				foreach (GameObject neighbor in _mapPoints)
				{
					if (node.point != neighbor && Vector3.Distance(node.point.transform.position, neighbor.transform.position) < 1.5f)
					{
						GraphNode neighborNode = _graphNodes.Find(n => n.point == neighbor);
						node.neighbors.Add(neighborNode);
					}
				}
			}
		}

		public void LaunchMan(GameObject start, GameObject end) {
			var path = FindPath(start, end);
			var storage = GameCore.Instance.Get<ObjectsStorage>();

			var man = storage.GetObjectByType(ObjectType.Man);

			man.transform.SetParent(_manContainer);
			man.transform.position = start.transform.position;

			for (int index = 1; index < path.Count; index++) {
				var point = path[index].transform.position;
				man.transform.DOMove(point, 5f).SetEase(Ease.Linear).OnComplete(() => Destroy(man.gameObject));
			}
		}

		public List<GameObject> FindPath(GameObject start, GameObject end)
		{
			// инициализируем начальную и конечную вершины
			GraphNode startNode = _graphNodes.Find(n => n.point == start);
			GraphNode endNode = _graphNodes.Find(n => n.point == end);
			startNode.distanceToStart = 0;
			startNode.distanceToEnd = Vector3.Distance(start.transform.position, end.transform.position);
			// запускаем алгоритм A*
			List<GraphNode> openList = new List<GraphNode>();
			List<GraphNode> closedList = new List<GraphNode>();
			openList.Add(startNode);
			while (openList.Count > 0)
			{
				// выбираем вершину с наименьшей оценкой
				GraphNode current = openList.OrderBy(n => n.distanceToStart + n.distanceToEnd).First();
				// если мы достигли конечной точки, то возвращаем путь
				if (current == endNode)
				{
					List<GameObject> path = new List<GameObject>();
					while (current != null)
					{
						path.Add(current.point);
						current = current.previous;
					}
					path.Reverse();
					return path;
				}
				// переносим вершину из openList в closedList
				openList.Remove(current);
				closedList.Add(current);
		// обновляем расстояния до соседних вершин
		foreach (GraphNode neighbor in current.neighbors)
		{
			if (closedList.Contains(neighbor))
			{
				continue;
			}
			float tentativeDistance = current.distanceToStart + Vector3.Distance(current.point.transform.position, neighbor.point.transform.position);
			if (!openList.Contains(neighbor) || tentativeDistance < neighbor.distanceToStart)
			{
				neighbor.previous = current;
				neighbor.distanceToStart = tentativeDistance;
				neighbor.distanceToEnd = Vector3.Distance(neighbor.point.transform.position, end.transform.position);
				if (!openList.Contains(neighbor))
				{
					openList.Add(neighbor);
				}
			}
		}
			}
			// если не удалось найти путь, то возвращаем null
			return null;
		}
		
		public class GraphNode
		{
			public GameObject point; // точка на карте
			public List<GraphNode> neighbors; // список соседних вершин
			public float distanceToStart; // расстояние до начальной точки
			public float distanceToEnd; // оценка расстояния до конечной точки
			public GraphNode previous; // предыдущая вершина в пути

			public GraphNode(GameObject p)
			{
				point = p;
				neighbors = new List<GraphNode>();
				distanceToStart = Mathf.Infinity;
				distanceToEnd = Mathf.Infinity;
				previous = null;
			}
		}

	}
}