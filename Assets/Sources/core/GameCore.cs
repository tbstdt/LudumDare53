using Sources.map;
using UnityEngine;

namespace Sources.core
{
	public class GameCore : MonoBehaviour {
		[SerializeField] private MapManager _mapManager;
		[SerializeField] private MapGenerator _mapGenerator;
		[SerializeField] private ObjectsStorage _objectsStorage;
		[SerializeField] private OrderGenerator _orderGenerator;

		private static CoreManagerProvider _coreManagerProvider;

		public static CoreManagerProvider Instance => _coreManagerProvider ??= new CoreManagerProvider();

		private void Start() {
				Instance.Register<MapManager>(_mapManager);
				Instance.Register<MapGenerator>(_mapGenerator);
				Instance.Register<ObjectsStorage>(_objectsStorage);
				Instance.Register<OrderGenerator>(_orderGenerator);

				Instance.InitManagers();
		}
	}

	public class CoreManagerProvider : Core {
	}
}