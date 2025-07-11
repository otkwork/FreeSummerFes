using Unity.VisualScripting;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class PlayerRay : MonoBehaviour
{
	[SerializeField] private float rayDistance = 10f; // ���C�̔�΂�����
	private static bool m_lookStall;

	void Start()
    {
        m_lookStall = false;
    }

	// Update is called once per frame
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

	public static bool lookStall
	{
		get { return m_lookStall; }
	}
}
