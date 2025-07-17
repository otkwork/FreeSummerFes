using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRay : MonoBehaviour
{
	[SerializeField] private float rayDistance = 10f; // レイの飛ばす距離
	private static bool m_lookStall;

	void Start()
    {
        m_lookStall = false;
	}

	void Update()
	{
		// 射的中ならreturn
		if (PlayerController.isShooting)
		{
			m_lookStall = false;
			return;
		}

		RaycastHit hit;
		// レイを飛ばす
		if (Physics.Raycast(transform.position, transform.forward, out hit, rayDistance))
		{
			if (hit.transform.TryGetComponent(out Stall stall))
			{
				m_lookStall = true;
			}
			else
			{
				m_lookStall = false;
			}
		}
		else
		{
			m_lookStall = false;
		}

		// レイの見た目を表示
		Debug.DrawRay(transform.position, transform.forward, Color.green, rayDistance);
	}

	public static bool lookStall
	{
		get { return m_lookStall; }
	}
}
