using UnityEngine;

public class ShootingObject : MonoBehaviour
{
	[SerializeField] string m_objectName;
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
}
