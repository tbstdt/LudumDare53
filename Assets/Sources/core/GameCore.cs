using Sources.Editor;
using Sources.Editor.UI;
using Sources.map;
using UnityEngine;

namespace Sources.core
{
	public class GameCore : MonoBehaviour {
		[SerializeField] private MapManager _mapManager;
		[SerializeField] private MapGenerator _mapGenerator;
		[SerializeField] private ObjectsStorage _objectsStorage;
		[SerializeField] private OrderGenerator _orderGenerator;
		[SerializeField] private UIManager _uiManager;
		[SerializeField] private SoundManager _soundManager;
		[SerializeField] private EndGamePanel _endGamePanel;
		[SerializeField] private TutorialController _tutorialController;

		private static CoreManagerProvider _coreManagerProvider;

		public static CoreManagerProvider Instance => _coreManagerProvider ??= new CoreManagerProvider();

		private void Awake() {
				Instance.Register<TutorialController>(_tutorialController);
				Instance.Register<MapManager>(_mapManager);
				Instance.Register<MapGenerator>(_mapGenerator);
				Instance.Register<ObjectsStorage>(_objectsStorage);
				Instance.Register<OrderGenerator>(_orderGenerator);
				Instance.Register<UIManager>(_uiManager);
				Instance.Register<SoundManager>(_soundManager);
				Instance.Register<EndGamePanel>(_endGamePanel);

				Instance.InitManagers();
		}
	}

	public class CoreManagerProvider : Core {
	}
}