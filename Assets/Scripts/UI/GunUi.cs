using UnityEngine;
using UnityEngine.UI;

public class GunUi : MonoBehaviour
{
    private Image m_image;
    private Image m_gunImage;
    private GunDataEntity m_gunData;

    private void Awake()
    {
        m_image = GetComponent<Image>();
        m_gunImage = transform.GetChild(0).GetComponent<Image>();
    }

    public void SetData(GunDataEntity data)
    {
        m_gunData = data;
        Loader.LoadSpriteAsync(m_gunData.gunName).Completed += op =>
        m_gunImage.sprite = op.Result;
    }

    public void SetStyle(Vector3 pos, Vector3 scale, Color color)
    {
        transform.position = pos;
        transform.localScale = scale;
        m_image.color = color;
        m_gunImage.color = color;
    }
}
