using Cinemachine;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    private static CinemachineVirtualCamera m_camera;
	private static GameObject m_this;

    // ƒJƒƒ‰‚Ì—Dæ“x
    private static readonly int ShootingCameraNum = 20;
    private static readonly int MoveingCameraNum = 5;

    void Start()
    {
        m_camera = GetComponent<CinemachineVirtualCamera>();
		m_this = gameObject;
		m_this.SetActive(false);
	}

    public static void ShootingCamera()
    {
		m_this.SetActive(true);
		m_camera.Priority = ShootingCameraNum;
    }

    public static void MoveingCamera()
    {
		m_this.SetActive(false);
		m_camera.Priority  = MoveingCameraNum;
    }
}
