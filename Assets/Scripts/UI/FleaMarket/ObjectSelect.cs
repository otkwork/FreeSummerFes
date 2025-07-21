using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ObjectSelect : MonoBehaviour
{
    [SerializeField] private TMP_InputField m_setPrice;
    [SerializeField] private GameObject m_inventory;
    
    private int m_price = 0;
    private static bool m_selectObject = true;


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

    }

    public bool selectObject
    {
        get { return m_selectObject; } 
        set {  m_selectObject = value; }
    }
}
