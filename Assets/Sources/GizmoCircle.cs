using UnityEngine;

namespace Sources.Editor {
	public class GizmoCircle : MonoBehaviour {
#if UNITY_EDITOR
		void OnDrawGizmos() {
			UnityEditor.Handles.color = Color.green;
			UnityEditor.Handles.DrawWireDisc(transform.position, transform.forward, 5f, 3f);
		}

		void OnDrawGizmosSelected() {
			UnityEditor.Handles.color = Color.red;
			UnityEditor.Handles.DrawWireDisc(transform.position, transform.forward, 1f, 3f);
		}
#endif
	}
}