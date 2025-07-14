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
			return; // ���ɃV�[�������[�h����Ă���ꍇ�͉������Ȃ�
		}
		SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
	}

	public static void Unload(string sceneName)
	{
		if (!SceneManager.GetSceneByName(sceneName).isLoaded)
		{
			return; // �V�[�������[�h����Ă��Ȃ��ꍇ�͉������Ȃ�
		}
		SceneManager.UnloadSceneAsync(sceneName);
	}

	public static void ChangeScene(string prevSceneName, string nextSceneName)
	{
		Load(nextSceneName);
		Unload(prevSceneName);
	}
}
