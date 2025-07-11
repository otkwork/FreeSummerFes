using UnityEngine;
using UnityEngine.AddressableAssets;

public class Shooting : MonoBehaviour
{
	[SerializeField] private GameObject m_bullet; // �e�ۂ̃v���n�u
	private static GunDataEntity m_gunData;
	private static GameObject m_this;
	private static GameObject m_model; // �e�̃��f��
	private static int m_bulletPower; // �e�ۂ̈З�
	private static bool m_isShooting; // �e�������Ă��邩�ǂ���

	// Start is called before the first frame update
	void Awake()
    {
        m_this = gameObject;
	}

    // Update is called once per frame
    void Update()
    {
		// �e������ԂłȂ��A�܂��͏e�̑I�𒆂͉������Ȃ�
		if (!m_isShooting || GunSelect.isSelectTime)
		{
			Destroy(m_model);
			return;
		}

		// InputSystem�ύX�\�茟���L�[���[�h�@"���񂿂�"
		// ���InputSystem�ɕύX
		if (PlayerController.isShooting && Input.GetMouseButtonDown(0))
		{
			GameObject bullet = Instantiate(m_bullet, m_model.transform.GetChild(0).transform.position, transform.rotation);
			bullet.GetComponent<Rigidbody>().AddForce(m_model.transform.forward * m_bulletPower); // �e�ۂɗ͂�������
			Destroy(bullet, 2.0f); // 2�b��ɒe�ۂ��폜
		}
    }

	public static void SetData(GunDataEntity data)
	{
		m_gunData = data;
		m_isShooting = true; // �e������Ԃɂ���

		Loader.LoadGameObjectAsync(m_gunData.gunName).Completed += op =>
		{
			m_model = Instantiate(op.Result, m_this.transform.position, m_this.transform.rotation, m_this.transform); // �e�̃��f�����C���X�^���X��
			Addressables.Release(op);
		};
		m_bulletPower = m_gunData.bulletPower; // �e�ۂ̈З͂�ݒ�
	}
}
