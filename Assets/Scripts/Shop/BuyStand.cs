using JetBrains.Annotations;
using UnityEngine;

public class BuyStand : MonoBehaviour
{
	[SerializeField] private GameObject m_priceTag;

	private const int NewObjectSetTime = 3;

	private (int, int) m_prevBuyDay = (0, 0);
	private int m_maxPrice;
	private int m_minPrice;
	private int m_price;
	private bool m_isSetObject;

	private void Start()
	{
		m_maxPrice = 0;
		m_minPrice = 0;
		m_isSetObject = false;
	}

	void Update()
    {
		if (!m_isSetObject) return;

		// オブジェクトの売却
		if (WorldTime.GetWorldTime().Item1 >= NewObjectSetTime && WorldTime.GetWorldDay() != m_prevBuyDay)
		{
			m_prevBuyDay = WorldTime.GetWorldDay();


			// 売却成功した場合
			BuyManager.AddSales(m_price);
		}
	}

	public void SetData()
	{
		m_isSetObject = true;
	}

	public void SetPrice(int value)
	{
		m_price = value;
	}
}
