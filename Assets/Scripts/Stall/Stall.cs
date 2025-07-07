using Unity.VisualScripting;
using UnityEngine;

public class Stall : MonoBehaviour
{
    [SerializeField] private GameObject m_lookUi;   // プレイヤーが見ている際に表示するUI
	void Start()
    {
        
    }

    void Update()
    {
        m_lookUi.SetActive(PlayerRay.lookStall);
    }
}
