using Unity.VisualScripting;
using UnityEngine;

public class Stall : MonoBehaviour
{
    [SerializeField] private GameObject m_lookUi;   // �v���C���[�����Ă���ۂɕ\������UI
	void Start()
    {
        
    }

    void Update()
    {
        m_lookUi.SetActive(PlayerRay.lookStall);
    }
}
