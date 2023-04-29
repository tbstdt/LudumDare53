using Sources.map;
using UnityEngine;

namespace Sources.core
{
	public class GameCore : MonoBehaviour {
		[SerializeField] private MapManager _mapManager;
		
		private static CoreManagerProvider _coreManagerProvider;

		public static CoreManagerProvider Instance => _coreManagerProvider ??= new CoreManagerProvider();

		private void Start() {
				///register
				Instance.Register<MapManager>(_mapManager);
				Instance.InitManagers();
		}
	}
	
	public class CoreManagerProvider : Core {
	}
}
