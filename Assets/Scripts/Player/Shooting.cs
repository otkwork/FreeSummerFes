using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
	[SerializeField] private PlayerInput m_playerInput;
	private static GunDataEntity m_gunData;
	private static Transform m_transform;
	private static GameObject m_model; // 銃のモデル
	private static GameObject m_bullet; // 弾丸のモデル
	private static int m_bulletPower; // 弾丸の威力
	private static bool m_isShooting; // 銃を撃っているかどうか

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
        if (m_isShooting)
        {
			GameObject bullet = Instantiate(m_bullet, m_model.transform.GetChild(0).transform.position, Quaternion.Euler(m_model.transform.rotation.eulerAngles + new Vector3(-90, 0, 0)));
            bullet.GetComponent<Rigidbody>().AddForce(m_model.transform.forward * m_bulletPower); // 弾丸に力を加える
            Destroy(bullet, 2.0f); // 2秒後に弾丸を削除
        }
    }

    // Update is called once per frame
    void Update()
    {
		// 銃を撃つ状態でない、または銃の選択中は何もしない
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
			m_model = Instantiate(op.Result, m_transform.position, m_transform.rotation, m_transform); // 銃のモデルをインスタンス化
			Addressables.Release(op);
		};

		Loader.LoadGameObjectAsync(m_gunData.bulletName).Completed += op =>
		{
			m_bullet = op.Result;
			Addressables.Release(op);
			m_isShooting = true; // 銃を撃つ状態にする
		};
		m_bulletPower = m_gunData.bulletPower; // 弾丸の威力を設定
	}

	public static bool isShooting
	{
		get { return m_isShooting; }
		set { m_isShooting = value; }
	}
}
