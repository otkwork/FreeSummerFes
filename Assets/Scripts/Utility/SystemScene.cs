using UnityEngine;
using UnityEngine.SceneManagement;

public class SystemScene : MonoBehaviour
{
	private void Awake()
	{
		// SceneManager.LoadScene("Title", LoadSceneMode.Additive);
	}

	public static void Load(string sceneName)
	{
		if (SceneManager.GetSceneByName(sceneName).isLoaded)
		{
			return; // 既にシーンがロードされている場合は何もしない
		}
		SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
	}

	public static void Unload(string sceneName)
	{
		if (!SceneManager.GetSceneByName(sceneName).isLoaded)
		{
			return; // シーンがロードされていない場合は何もしない
		}
		SceneManager.UnloadSceneAsync(sceneName);
	}

	public static void ChangeScene(string prevSceneName, string nextSceneName)
	{
		Load(nextSceneName);
		Unload(prevSceneName);
	}

	public static void AllClearScene()
	{
		// システムシーンをのぞいたすべてのシーンを削除
		for (int i = 0; i < SceneManager.sceneCount; ++i)
		{
			if (SceneManager.GetSceneAt(i).name == "System") continue;

			Unload(SceneManager.GetSceneAt(i).name);
		}
	}
}
