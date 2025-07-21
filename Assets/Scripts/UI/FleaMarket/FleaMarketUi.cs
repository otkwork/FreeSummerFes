using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FleaMarketUi : MonoBehaviour
{
    [SerializeField] Image m_objectImage;
    [SerializeField] TextMeshProUGUI m_objectName;
    [SerializeField] TextMeshProUGUI m_objectPrice;

    private int m_price = 0;

    public void SetData(Sprite objSprite, string objName)
    {
        m_objectImage.sprite = objSprite;
        m_objectName.text = objName;
        m_objectPrice.text = m_price.ToString();
    }

    public int objectPrice
    {
        get { return m_price; }
    }
}
