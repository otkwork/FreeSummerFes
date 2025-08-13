using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
	[SerializeField] private PlayerInput m_playerInput;
	[SerializeField] private TextMeshPro m_bulletText;
	private static GunDataEntity m_gunData;
	private static Transform m_transform;
	private static GameObject m_model; // e‚Ìƒ‚ƒfƒ‹
	private static GameObject m_bullet; // ’eŠÛ‚Ìƒ‚ƒfƒ‹
	private static int m_bulletPower; // ’eŠÛ‚ÌˆĞ—Í
	private static bool m_isShooting; // e‚ğŒ‚‚Á‚Ä‚¢‚é‚©‚Ç‚¤‚©
	private static int m_bulletAmount;
	private static bool m_endShooting = true;

	// Start is called before the first frame update
	void Awake()
    {
		m_transform = transform;
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
		if (PlayerController.isOpenPhone) return;
        if (m_isShooting && !m_endShooting)
        {
			GameObject bullet = Instantiate(m_bullet, m_model.transform.GetChild(0).transform.position, Quaternion.Euler(m_model.transform.rotation.eulerAngles + new Vector3(-90, 0, 0)));
            bullet.GetComponent<Rigidbody>().AddForce(m_model.transform.forward * m_bulletPower); // ’eŠÛ‚É—Í‚ğ‰Á‚¦‚é
            Destroy(bullet, 2.0f); // 2•bŒã‚É’eŠÛ‚ğíœ
			
			// ’e‚ğÁ”ï
			m_bulletAmount--;
			if (m_bulletAmount <= 0)
			{
                m_endShooting = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
		// e‚ğŒ‚‚Âó‘Ô‚Å‚È‚¢A‚Ü‚½‚Íe‚Ì‘I‘ğ’†‚Í‰½‚à‚µ‚È‚¢
		if (!m_isShooting || GunSelect.isSelectTime)
		{
			Destroy(m_model);
		}

		m_bulletText.gameObject.SetActive(m_isShooting);
		if (m_isShooting)
		{
			m_bulletText.text = m_bulletAmount.ToString();
		}
    }

	public static void SetData(GunDataEntity data)
	{
		m_gunData = data;

		Loader.LoadGameObjectAsync(m_gunData.gunName).Completed += op =>
		{
			m_model = Instantiate(op.Result, m_transform.position, m_transform.rotation, m_transform); // e‚Ìƒ‚ƒfƒ‹‚ğƒCƒ“ƒXƒ^ƒ“ƒX‰»
			Addressables.Release(op);
		};

		Loader.LoadGameObjectAsync(m_gunData.bulletName).Completed += op =>
		{
			m_bullet = op.Result;
			Addressables.Release(op);
			m_isShooting = true; // e‚ğŒ‚‚Âó‘Ô‚É‚·‚é
		};
		m_bulletPower = m_gunData.bulletPower; // ’eŠÛ‚ÌˆĞ—Í‚ğİ’è
		m_bulletAmount = m_gunData.bulletAmount;
		m_endShooting = false;
	}

	public static bool isShooting
	{
		get { return m_isShooting; }
		set { m_isShooting = value; }
	}

	public static bool endShooting
	{
		get { return m_endShooting; }
	}
}
