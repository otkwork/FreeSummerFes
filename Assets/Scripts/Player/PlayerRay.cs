using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class PlayerRay : MonoBehaviour
{
	[SerializeField] private float rayDistance = 10f; // ���C�̔�΂�����

	void Start()
    {
        
    }

	// Update is called once per frame
	void Update()
	{
		RaycastHit hit;
		// ���C���΂�
		if (Physics.Raycast(transform.position, transform.forward, out hit, rayDistance))
		{
			
		}
	}
}
