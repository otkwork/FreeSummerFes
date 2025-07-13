using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
	[SerializeField] private PlayerInput m_playerInput;
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

    private void OnEnable()
    {
        m_playerInput.actions["Decision"].performed += OnDecision;
    }

    private void OnDisable()
    {
        m_playerInput.actions["Decision"].performed -= OnDecision;
    }

	private void OnDecision(InputAction.CallbackContext callback)
	{
        if (m_isShooting)
        {
            GameObject bullet = Instantiate(m_bullet, m_model.transform.GetChild(0).transform.position, transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(m_model.transform.forward * m_bulletPower); // �e�ۂɗ͂�������
            Destroy(bullet, 2.0f); // 2�b��ɒe�ۂ��폜
        }
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
    }

	public static void SetData(GunDataEntity data)
	{
		m_gunData = data;

		Loader.LoadGameObjectAsync(m_gunData.gunName).Completed += op =>
		{
			m_model = Instantiate(op.Result, m_this.transform.position, m_this.transform.rotation, m_this.transform); // �e�̃��f�����C���X�^���X��
			Addressables.Release(op);
			m_isShooting = true; // �e������Ԃɂ���
		};
		m_bulletPower = m_gunData.bulletPower; // �e�ۂ̈З͂�ݒ�
	}

	public static bool isShooting
	{
		get { return m_isShooting; }
		set { m_isShooting = value; }
	}
}
