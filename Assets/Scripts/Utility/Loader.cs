using UnityEngine;
using UnityEngine.AddressableAssets;

public static class Loader
{
	// 指定したアドレスのSpriteをロードして表示する
	public static Sprite LoadSpriteAsync(string address)
	{
		Sprite sprite = null;
		Addressables.LoadAssetAsync<Sprite>(address).Completed += op =>
		{
			sprite = op.Result;
			Addressables.Release(op);
		};
		return sprite;
	}

	public static GameObject LoadGameObjectAsync(string address)
	{
		GameObject obj = null;
		Addressables.LoadAssetAsync<GameObject>(address).Completed += op =>
		{
			obj = op.Result;
			Addressables.Release(op);
		};
		return obj;
	}
}