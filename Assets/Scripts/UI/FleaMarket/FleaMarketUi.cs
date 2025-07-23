using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FleaMarketUi : MonoBehaviour
{
    [SerializeField] Image m_objectImage;
    [SerializeField] TextMeshProUGUI m_objectName;
    [SerializeField] TextMeshProUGUI m_objectPrice;

    private int m_price = 0;

    public void SetData(string objName)
    {
		Loader.LoadSpriteAsync(objName).Completed += op =>
		{
			m_objectImage.sprite = op.Result;
		};
        m_objectName.text = objName;
        m_objectPrice.text = m_price.ToString();
    }

    public int objectPrice
    {
		get { return m_price; }
		set { m_price = value; }
    }
}
