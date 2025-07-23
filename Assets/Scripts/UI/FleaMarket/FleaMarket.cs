using System.Collections.Generic;
using UnityEngine;

public class FleaMarket : MonoBehaviour
{
    [SerializeField] GameObject m_listingPanel;
	private static List<ShootingObjectEntity> m_marketList = new List<ShootingObjectEntity>();
	private static List<GameObject> m_marketUiList = new List<GameObject>();

    public void OnListing()
    {
        m_listingPanel.SetActive(true);
    }

	void Update()
	{
		// Žw’è‚µ‚½ŽžŠÔ‚ÉŠm—¦”„‹p
		//if (WorldTime.GetWorldDay())
	}

	public static void AddData(ShootingObjectEntity data, GameObject obj)
	{
		m_marketList.Add(data);
		m_marketUiList.Add(obj);
	}
}
