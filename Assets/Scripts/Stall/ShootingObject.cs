using UnityEngine;

public class ShootingObject : MonoBehaviour
{
	ShootingObjectEntity m_data;

	public void OnCollisionEnter(Collision other)
	{
		// 地面についたら
		if (other.transform.CompareTag("Ground"))
		{
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
