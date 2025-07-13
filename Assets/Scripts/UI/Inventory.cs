using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
	[SerializeField] Transform m_content;
	[SerializeField] GameObject m_button;
	private static List<string> m_shootingObjects = new List<string>();
	private static bool m_addObject = false;

    public static void AddObject(string objectName)
	{
		m_shootingObjects.Add(objectName);
		m_addObject = true;
	}

    private void Update()
    {
        if (m_addObject)
		{
			m_addObject = false;
			GameObject obj = Instantiate(m_button, m_content);
			obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = m_shootingObjects[m_shootingObjects.Count - 1];
		}
    }
}
