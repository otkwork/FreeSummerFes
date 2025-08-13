using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement;

public class FleaMarket : MonoBehaviour
{
    [SerializeField] GameObject m_listingPanel;
	private static List<(ShootingObjectEntity, int)> m_marketList = new List<(ShootingObjectEntity, int)>();
	private static List<GameObject> m_marketUiList = new List<GameObject>();
	private (int, int) m_prevBuyDay = (7, 31); 

	private readonly (int, int) BuyTime = (12, 0);

    public void OnListing()
    {
        m_listingPanel.SetActive(true);
    }

	void Update()
	{
		// 指定した時間に一日一回確率売却
		if (WorldTime.GetWorldDay() != m_prevBuyDay &&
			BuyTime.Item1 < WorldTime.GetWorldTime().Item1 &&
			BuyTime.Item2 < WorldTime.GetWorldTime().Item2)
		{
			m_prevBuyDay = WorldTime.GetWorldDay();

			int objectIndex = 0;
			List<int> removeList = new List<int>();
			foreach ((ShootingObjectEntity, int) buyData in m_marketList)
			{
				// 1～100までの値
				int buySeed = (int)Random.Range(1, 101);

				// 設定した値段がbuySeedの結果と比べる
				// 1%における値段に確率をかけて今回売れる最高額を出す
				int maxPrice = ((buyData.Item1.maxPrice - buyData.Item1.minPrice) / 100) * buySeed + buyData.Item1.minPrice;

				// 最高額よりも設定値段の方が小さいときは売れる
                if (maxPrice > buyData.Item2)
                {
					// 所持金にbuyDataを加算
					Money.AddMoney(buyData.Item2);
					// リスト二つから削除(実態も削除)
					removeList.Add(objectIndex);
                }
                objectIndex++;
			}

			foreach (int remove in removeList)
			{
                m_marketList.RemoveAt(remove);
                Destroy(m_marketUiList[remove]);
                m_marketUiList.RemoveAt(remove);
            }
		}
	}

	public static void AddData(ShootingObjectEntity data, int price, GameObject obj)
	{
		m_marketList.Add((data, price));
		m_marketUiList.Add(obj);
	}
}
