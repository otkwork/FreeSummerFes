using UnityEngine;

public class Shooting : MonoBehaviour
{
	enum GunType
	{
		CorkGun,	// コルク銃
		AirGun,     // エアガン
		RichGun,    // リッチガン
		Pistol,		// ピストル

		Length,     // 銃の種類の数
	}
	[SerializeField] private GameObject m_bullet; // 弾丸のプレハブ
	
	// 弾丸の力の強さ
	private readonly int[] BulletPower =
	{
		1000,
		1500,
		2000,
		3000,
	}; 

	[SerializeField] private GunType m_gunType; // 銃の種類

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		// InputSystem変更予定検索キーワード　"ちんちん"
		// 後にInputSystemに変更
		if (PlayerController.isShooting && Input.GetMouseButtonDown(0))
		{
			GameObject bullet = Instantiate(m_bullet, transform.position, transform.rotation);
			bullet.GetComponent<Rigidbody>().AddForce(transform.forward * BulletPower[(int)m_gunType]); // 弾丸に力を加える
			Destroy(bullet, 2.0f); // 2秒後に弾丸を削除
		}
    }
}
