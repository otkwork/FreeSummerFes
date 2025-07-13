using UnityEngine;

public class WarpPoint : MonoBehaviour
{
    [SerializeField] Transform m_warpPos;
    
    public void PlayerWarp(GameObject player)
    {
        if (player.TryGetComponent(out CharacterController controller))
        {
            controller.enabled = false;
            player.transform.position = m_warpPos.position;
            controller.enabled = true;
        }
    }
}
