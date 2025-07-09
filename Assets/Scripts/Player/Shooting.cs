using UnityEngine;

public class Shooting : MonoBehaviour
{
	enum GunType
	{
		CorkGun,	// �R���N�e
		AirGun,     // �G�A�K��
		RichGun,    // ���b�`�K��
		Pistol,		// �s�X�g��

		Length,     // �e�̎�ނ̐�
	}
	[SerializeField] private GameObject m_bullet; // �e�ۂ̃v���n�u
	
	// �e�ۂ̗͂̋���
	private readonly int[] BulletPower =
	{
		1000,
		1500,
		2000,
		3000,
	}; 

	[SerializeField] private GunType m_gunType; // �e�̎��

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		// InputSystem�ύX�\�茟���L�[���[�h�@"���񂿂�"
		// ���InputSystem�ɕύX
		if (PlayerController.isShooting && Input.GetMouseButtonDown(0))
		{
			GameObject bullet = Instantiate(m_bullet, transform.position, transform.rotation);
			bullet.GetComponent<Rigidbody>().AddForce(transform.forward * BulletPower[(int)m_gunType]); // �e�ۂɗ͂�������
			Destroy(bullet, 2.0f); // 2�b��ɒe�ۂ��폜
		}
    }
}
