using JetBrains.Annotations;
using UnityEngine;

public class BuyStand : MonoBehaviour
{
	[SerializeField] private GameObject m_priceTag;
	private static ShootingObjectEntity m_data;

	private const int NewObjectSetTime = 3;
	private readonly (float, float) Probability = (1f, 101f);	// 確率を出すときの1～100までの値

	private (int, int) m_prevBuyDay = (0, 0);
	private int m_price;
	private static bool m_isSetObject;

	private void Start()
	{
		m_isSetObject = false;
	}

	void Update()
    {
		if (!m_isSetObject) return;

		// オブジェクトの売却
		if (WorldTime.GetWorldTime().Item1 >= NewObjectSetTime && WorldTime.GetWorldDay() != m_prevBuyDay)
		{
			m_prevBuyDay = WorldTime.GetWorldDay();

			// m_priceの商品が売れる確率
			float probability = (m_price - m_data.minPrice) / (m_data.maxPrice - m_data.minPrice);
			if (Random.Range(Probability.Item1, Probability.Item2) <= 100 * probability)
			{
				// 売却成功した場合
				BuyManager.AddSales(m_price);
			}
		}
	}

	public static void SetData(ShootingObjectEntity data)
	{
		if (data == null) return;
		m_isSetObject = true;
		m_data = data;
	}

	public void SetPrice(int value)
	{
		m_price = value;
	}
}
