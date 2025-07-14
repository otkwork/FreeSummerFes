using UnityEngine;

public class ShootingObject : MonoBehaviour
{
	[SerializeField] string m_objectName;

	public void OnCollisionEnter(Collision other)
	{
		// 地面についたら
		if (other.transform.CompareTag("Ground"))
		{
            // そのオブジェクトをインベントリに追加する
            Inventory.AddObject(m_objectName);

			Destroy(gameObject);
		}
	}

	public void SetName(string name)
	{
		m_objectName = name;
	}
}
