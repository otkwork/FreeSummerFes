using UnityEngine;

public class ShootingObject : MonoBehaviour
{
	[SerializeField] string m_objectName;

	public void OnCollisionEnter(Collision other)
	{
		// �n�ʂɂ�����
		if (other.transform.CompareTag("Ground"))
		{
            // ���̃I�u�W�F�N�g���C���x���g���ɒǉ�����
            Inventory.AddObject(m_objectName);

			Destroy(gameObject);
		}
	}

	public void SetName(string name)
	{
		m_objectName = name;
	}
}
