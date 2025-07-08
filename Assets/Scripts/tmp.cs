using Cinemachine;
using UnityEngine;

public class tmp : MonoBehaviour
{
    private static CinemachineVirtualCamera m_camera;

    // ƒJƒƒ‰‚Ì—Dæ“x
    private static readonly int ShootingCameraNum = 20;
    private static readonly int MoveingCameraNum = 5;

    void Start()
    {
        m_camera = GetComponent<CinemachineVirtualCamera>();
    }

    public static void ShootingCamera()
    {
        m_camera.Priority = ShootingCameraNum;
    }

    public static void MoveingCamera()
    {
        m_camera.Priority  = MoveingCameraNum;
    }
}
