using System.Runtime.CompilerServices;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRay : MonoBehaviour
{
	[SerializeField] private float rayDistance = 10f; // レイの飛ばす距離
	[SerializeField] private GameObject m_moneyBoxText;
	private static bool m_lookStall;
	private static bool m_lookMoneyBox;

	void Start()
    {
        m_lookStall = false;
		m_lookMoneyBox = false;
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
			m_lookStall = hit.transform.TryGetComponent(out Stall stall);

			m_lookMoneyBox = hit.transform.CompareTag("MoneyBox");
		}
		else
		{
			m_lookStall = false;
			m_lookMoneyBox = false;
		}
		m_moneyBoxText.SetActive(m_lookMoneyBox && Money.ClearMoney());
		// レイの見た目を表示
		Debug.DrawRay(transform.position, transform.forward, Color.green, rayDistance);
	}

	public static bool lookStall
	{
		get { return m_lookStall; }
	}

	public static bool lookMoneyBox
	{
		get { return m_lookMoneyBox;}
	}
}
