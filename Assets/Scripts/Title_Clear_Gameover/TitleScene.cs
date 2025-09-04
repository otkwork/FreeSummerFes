using UnityEngine;
using UnityEngine.AddressableAssets;

public class TitleScene : MonoBehaviour
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
				SoundEffect.StopSe(m_bgm);
				SystemScene.ChangeScene("Title", "Story");
                SceneFade.FadeIn(1.0f);
            });
        }
    }
}
