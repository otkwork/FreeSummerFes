using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ObjectSelect : MonoBehaviour
{
	[SerializeField] GameObject m_marketUi;
	[SerializeField] Transform m_marketParent;
	[SerializeField] private TMP_InputField m_setPrice;
    [SerializeField] private GameObject m_diplayInventory;
    [SerializeField] private Inventory m_inventory;
	[SerializeField] private TextMeshProUGUI m_selectObjectText;
	private static ShootingObjectEntity m_data;
    
    private int m_price = 0;
    private static bool m_selectObject = false;


    public void SetPrice()
    {
        // 設定させていないまた0が入れられたとき
        if (string.IsNullOrEmpty(m_setPrice.text) || int.Parse(m_setPrice.text) == 0)
        {
            m_setPrice.text = null;
            return;
        }
        m_price = int.Parse(m_setPrice.text);
	}

	public void OnClickObjectSelect()
    {
        m_selectObject = true;
        m_diplayInventory.SetActive(true);
    }

    public void OnClickListing()
    {
		// 出品物の設定金額の設定ができていない場合return
		if (m_data == null) return;
		if (m_price == 0) return;

		GameObject obj = Instantiate(m_marketUi, m_marketParent);
		obj.TryGetComponent(out FleaMarketUi market);
		market.objectPrice = m_price;
		market.SetData(m_data);
		FleaMarket.AddData(m_data, m_price, obj);
        m_inventory.RemoveObject(m_data);
		gameObject.SetActive(false);
		m_data = null;
        m_price = 0;
        m_setPrice.text = null;

		Loader.LoadAudioClipAsync("Sell").Completed += op =>
		{
			SoundEffect.Play2D(op.Result);
			Addressables.Release(op);
		};

	}

    private void Update()
    {
        m_selectObjectText.text = m_data != null ? 
            m_data.displayName : "出品物の選択";
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
