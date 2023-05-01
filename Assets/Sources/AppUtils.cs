using Sources.core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sources.Editor
{
    public class AppUtils
    {
        public static void Exit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

		public static void Restart() {
			Time.timeScale = 1;
			GameCore.Instance.Dispode();
			SceneManager.LoadScene(0, LoadSceneMode.Single);
		}
	}
}