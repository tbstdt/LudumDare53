using UnityEngine;

namespace Sources.Editor {
	public class GizmoCircle : MonoBehaviour {
		void OnDrawGizmos() {
			UnityEditor.Handles.color = Color.green;
			UnityEditor.Handles.DrawWireDisc(transform.position, transform.forward, 0.05f, 3f);
		}
		
		void OnDrawGizmosSelected() {
			UnityEditor.Handles.color = Color.red;
			UnityEditor.Handles.DrawWireDisc(transform.position, transform.forward, 0.01f, 3f);
		}
	}
}