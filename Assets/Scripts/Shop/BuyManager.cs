using JetBrains.Annotations;
using UnityEngine;

public class BuyManager : MonoBehaviour
{
	private static int m_salesProceeds = 0;	// îÑÇËè„Ç∞ã‡

	public static void AddSales(int sales)
	{
		m_salesProceeds += sales;
	}

	public int GetSales()
	{
		int sales = m_salesProceeds;
		m_salesProceeds = 0;
		return sales;
	}
}
