using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public static class Loader
{
	// �w�肵���A�h���X��Sprite�����[�h���ĕ\������
	public static AsyncOperationHandle<Sprite> LoadSpriteAsync(string address)
	{
		return Addressables.LoadAssetAsync<Sprite>(address);
	}

	public static AsyncOperationHandle<GameObject> LoadGameObjectAsync(string address)
	{
		return Addressables.LoadAssetAsync<GameObject>(address);
	}
}