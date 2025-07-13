using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
	[SerializeField] private PlayerInput m_playerInput;
	[SerializeField] private GameObject m_bullet; // ’eŠÛ‚ÌƒvƒŒƒnƒu
	private static GunDataEntity m_gunData;
	private static GameObject m_this;
	private static GameObject m_model; // e‚Ìƒ‚ƒfƒ‹
	private static int m_bulletPower; // ’eŠÛ‚ÌˆĞ—Í
	private static bool m_isShooting; // e‚ğŒ‚‚Á‚Ä‚¢‚é‚©‚Ç‚¤‚©

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
            bullet.GetComponent<Rigidbody>().AddForce(m_model.transform.forward * m_bulletPower); // ’eŠÛ‚É—Í‚ğ‰Á‚¦‚é
            Destroy(bullet, 2.0f); // 2•bŒã‚É’eŠÛ‚ğíœ
        }
    }

    // Update is called once per frame
    void Update()
    {
		// e‚ğŒ‚‚Âó‘Ô‚Å‚È‚¢A‚Ü‚½‚Íe‚Ì‘I‘ğ’†‚Í‰½‚à‚µ‚È‚¢
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
			m_model = Instantiate(op.Result, m_this.transform.position, m_this.transform.rotation, m_this.transform); // e‚Ìƒ‚ƒfƒ‹‚ğƒCƒ“ƒXƒ^ƒ“ƒX‰»
			Addressables.Release(op);
			m_isShooting = true; // e‚ğŒ‚‚Âó‘Ô‚É‚·‚é
		};
		m_bulletPower = m_gunData.bulletPower; // ’eŠÛ‚ÌˆĞ—Í‚ğİ’è
	}

	public static bool isShooting
	{
		get { return m_isShooting; }
		set { m_isShooting = value; }
	}
}
