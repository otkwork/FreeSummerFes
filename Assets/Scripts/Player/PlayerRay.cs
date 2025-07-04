using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class PlayerRay : MonoBehaviour
{
	[SerializeField] private float rayDistance = 10f; // ƒŒƒC‚Ì”ò‚Î‚·‹——£

	void Start()
    {
        
    }

	// Update is called once per frame
	void Update()
	{
		RaycastHit hit;
		// ƒŒƒC‚ð”ò‚Î‚·
		if (Physics.Raycast(transform.position, transform.forward, out hit, rayDistance))
		{
			
		}
	}
}
