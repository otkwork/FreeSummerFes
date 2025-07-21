using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class FleaMarket : MonoBehaviour
{
    [SerializeField] GameObject m_marketUi;
    [SerializeField] Transform m_marketParent;
    [SerializeField] GameObject m_listingPanel;
    private List<GameObject> m_marketList; 

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void OnListing()
    {
        m_listingPanel.SetActive(true);
    }
}
