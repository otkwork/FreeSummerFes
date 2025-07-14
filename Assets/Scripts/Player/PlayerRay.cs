using UnityEditorInternal;
using UnityEngine;

public class PlayerRay : MonoBehaviour
{
	[SerializeField] private float rayDistance = 10f; // ���C�̔�΂�����
	private static bool m_lookStall;
	private static bool m_hitShrine;
	private static bool m_hitShop;

	void Start()
    {
        m_lookStall = false;
		m_hitShrine = false;
		m_hitShop = false;
	}

	void Update()
	{
		// �˓I���Ȃ�return
		if (PlayerController.isShooting)
		{
			m_lookStall = false;
			return;
		}

		RaycastHit hit;
		// ���C���΂�
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

		// ���C�̌����ڂ�\��
		Debug.DrawRay(transform.position, transform.forward, Color.green, rayDistance);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Shrine"))
		{
			m_hitShrine = true;
		}

		if (other.CompareTag("Shop"))
		{
			m_hitShop = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Shrine"))
		{
			m_hitShrine = false;
		}

		if (other.CompareTag("Shop"))
		{
			m_hitShop = false;
		}
	}

	public static bool lookStall
	{
		get { return m_lookStall; }
	}

	public static bool hitShrine
	{
		get { return m_hitShrine; }
		set { m_hitShrine = value; }
	}

	public static bool hitShop
	{
		get { return m_hitShop; }
		set { m_hitShop = value; }
	}
}
