using UnityEngine;

namespace Sources.core
{
	public class GameCore : MonoBehaviour {
		private static CoreManagerProvider _coreManagerProvider;

		public static CoreManagerProvider Instance => _coreManagerProvider ??= new CoreManagerProvider();

		private void Start() {
				///register
			
			Instance.InitManagers();
		}
	}
	
	public class CoreManagerProvider : Core {
	}
}
