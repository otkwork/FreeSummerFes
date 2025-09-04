using UnityEngine;

public class ShootingObject : MonoBehaviour
{
	ShootingObjectEntity m_data;
	bool m_onGround = false;

	public void OnCollisionEnter(Collision other)
	{
		if (m_onGround) return;

		// 地面についたら
		if (other.transform.CompareTag("Ground"))
		{
			m_onGround = true;
			// そのオブジェクトをインベントリに追加する
			Inventory.AddObject(m_data);
			Destroy(gameObject);
		}
	}

	public void SetData(ShootingObjectEntity data)
	{
		m_data = data;
	}
}
