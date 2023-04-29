using Sources.core;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Editor.UI {
	public class UIManager : MonoBehaviour, ICoreRegistrable, ICoreInit{
		[SerializeField] private Text _resourceOne;
		[SerializeField] private Text _resourceTwo;
		[SerializeField] private Text _resourceThree;
		[SerializeField] private Text _resourceMoney;
		[SerializeField] private Text _mans;


		public void Init() {
		}
	}
}