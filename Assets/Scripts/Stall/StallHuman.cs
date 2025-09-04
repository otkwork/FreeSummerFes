using UnityEngine;
using UnityEngine.AddressableAssets;

public class StallHuman : MonoBehaviour
{
	Animator m_animator;
	CapsuleCollider m_collider;

	const int BulletLayer = 7;

	private void Awake()
	{
		m_animator = GetComponent<Animator>();
		m_collider = GetComponent<CapsuleCollider>();
	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.transform.CompareTag("PistolBullet"))
		{
			m_collider.enabled = false;
			m_animator.SetTrigger("Die");
			
			Loader.LoadAudioClipAsync("DamageVoice").Completed += op =>
			{
				SoundEffect.Play2D(op.Result);
				Addressables.Release(op);
			};

			SceneFade.FadeOut(3.0f, () =>
			{
				SystemScene.AllClearScene();
				SystemScene.Load("GameOver");
				GameoverScene.SetGameOverType(GameoverScene.GameoverType.Murder);
				SceneFade.FadeIn(1.0f);
			});
		}
		if (other.gameObject.layer == BulletLayer) Destroy(other.gameObject);
	}
}
