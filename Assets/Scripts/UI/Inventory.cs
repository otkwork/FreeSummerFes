using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
	[SerializeField] Transform m_content;
	[SerializeField] GameObject m_button;
	private static List<ShootingObjectEntity> m_shootingObjects = new List<ShootingObjectEntity>();
	private static int m_addObject = 0;

    public static void AddObject(ShootingObjectEntity data)
	{
		m_shootingObjects.Add(data);
		m_addObject++;
	}

    private void Update()
    {
		// 同時に複数個オブジェクトが落下した時のためにint
		if (m_addObject == 0) return;	

		// 生成したオブジェクトに渡すデータはリストの最後尾から追加した数の分引く
		GameObject obj = Instantiate(m_button, m_content);
		obj.GetComponent<InventoryButton>().objectData = m_shootingObjects[m_shootingObjects.Count - m_addObject];
		obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = m_shootingObjects[m_shootingObjects.Count - 1].displayName;
		m_addObject--;
	}

	public void RemoveObject(ShootingObjectEntity data)
	{
		int objectIndex = m_shootingObjects.IndexOf(data);
		m_shootingObjects.RemoveAt(objectIndex);
		Destroy(m_content.GetChild(objectIndex).gameObject);
	}
}
