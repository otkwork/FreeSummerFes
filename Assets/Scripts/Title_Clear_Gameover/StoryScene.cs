using UnityEngine;
using UnityEngine.AddressableAssets;

public class StoryScene : MonoBehaviour
{
	private bool isFade = false;
	private AudioSource m_bgm;

	private void Start()
	{
		Loader.LoadAudioClipAsync("BGM_Night").Completed += op =>
		{
			m_bgm = SoundEffect.Play2D(op.Result, true);
			Addressables.Release(op);
		};
	}

	private void Update()
    {
        if (Input.anyKey && !isFade)
        {
			isFade = true;
            SceneFade.FadeOut(1.0f, () =>
            {
				SystemScene.Load("WorldTime");
				SystemScene.Load("Shrine");
				SystemScene.Load("Player");
				SoundEffect.StopSe(m_bgm);
				SystemScene.Unload("Story");
                SceneFade.FadeIn(1.0f);
            });
        }
    }
}
