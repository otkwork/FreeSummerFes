using UnityEngine;

public class InventoryButton : MonoBehaviour
{
	private ShootingObjectEntity m_objectData;

	public ShootingObjectEntity objectData
	{
		get { return m_objectData; }
		set { m_objectData = value; }
	}

    public void OnClick()
	{
		// o•i•¨‘I‘ðŽž
		if (ObjectSelect.selectObject)
		{
			ObjectSelect.SetData(m_objectData);
			ObjectSelect.selectObject = false;

			// content->viewport->DislayInventory
			transform.parent.parent.parent.gameObject.SetActive(false);
		}
	}
}
