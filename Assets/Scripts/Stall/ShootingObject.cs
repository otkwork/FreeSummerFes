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
		// 地面についたら
		if (other.transform.CompareTag("Ground"))
		{
            // そのオブジェクトをインベントリに追加する
            Inventory.AddObject(m_objectName);

			Destroy(gameObject);
		}
	}
}
