using TMPro;
using UnityEngine;

public class ObjectSelect : MonoBehaviour
{
	[SerializeField] GameObject m_marketUi;
	[SerializeField] Transform m_marketParent;
	[SerializeField] private TMP_InputField m_setPrice;
    [SerializeField] private GameObject m_inventory;
	private static ShootingObjectEntity m_data;
    
    private int m_price = 0;
    private static bool m_selectObject = false;


    public void SetPrice()
    {
        m_price = int.Parse(m_setPrice.text);
	}

	public void OnClickObjectSelect()
    {
        m_selectObject = true;
        m_inventory.SetActive(true);
    }

    public void OnClickListing()
    {
		// èoïiï®ÇÃê›íËã‡äzÇÃê›íËÇ™Ç≈Ç´ÇƒÇ¢Ç»Ç¢èÍçáreturn
		if (m_data == null) return;
		if (m_price == 0) return;

		GameObject obj = Instantiate(m_marketUi, m_marketParent);
		obj.TryGetComponent(out FleaMarketUi market);
		market.objectPrice = m_price;
		market.SetData(m_data.objectName);
		FleaMarket.AddData(m_data, obj);
		gameObject.SetActive(false);
		m_data = null;
    }

    public static bool selectObject
    {
        get { return m_selectObject; } 
        set {  m_selectObject = value; }
    }

	public static void SetData(ShootingObjectEntity data)
	{
		m_data = data;
	}
}
