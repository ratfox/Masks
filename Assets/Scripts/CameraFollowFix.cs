using UnityEngine;
using Cinemachine;

public class CameraFollowFix : MonoBehaviour
{
    void Start()
    {
        // Find the persistent Player instance
        var vcam = GetComponent<CinemachineVirtualCamera>();
        
        if (PlayerController.Instance != null)
        {
            vcam.Follow = PlayerController.Instance.transform;
        }
    }
}