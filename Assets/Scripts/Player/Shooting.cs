using UnityEngine;
using UnityEngine.AddressableAssets;

public class Shooting : MonoBehaviour
{
	[SerializeField] private GameObject m_bullet; // 弾丸のプレハブ
	private static GunDataEntity m_gunData;
	private static GameObject m_this;
	private static GameObject m_model; // 銃のモデル
	private static int m_bulletPower; // 弾丸の威力
	private static bool m_isShooting; // 銃を撃っているかどうか

	// Start is called before the first frame update
	void Awake()
    {
        m_this = gameObject;
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

		// InputSystem変更予定検索キーワード　"ちんちん"
		// 後にInputSystemに変更
		if (PlayerController.isShooting && Input.GetMouseButtonDown(0))
		{
			GameObject bullet = Instantiate(m_bullet, m_model.transform.GetChild(0).transform.position, transform.rotation);
			bullet.GetComponent<Rigidbody>().AddForce(m_model.transform.forward * m_bulletPower); // 弾丸に力を加える
			Destroy(bullet, 2.0f); // 2秒後に弾丸を削除
		}
    }

	public static void SetData(GunDataEntity data)
	{
		m_gunData = data;
		m_isShooting = true; // 銃を撃つ状態にする

		Loader.LoadGameObjectAsync(m_gunData.gunName).Completed += op =>
		{
			m_model = Instantiate(op.Result, m_this.transform.position, m_this.transform.rotation, m_this.transform); // 銃のモデルをインスタンス化
			Addressables.Release(op);
		};
		m_bulletPower = m_gunData.bulletPower; // 弾丸の威力を設定
	}
}
