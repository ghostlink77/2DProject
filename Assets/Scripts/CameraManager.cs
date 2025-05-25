using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public CinemachineCamera virtualCamera;

    private static CameraManager instance;
    public static CameraManager Instance
    {
        get
        {
            if (instance == null)
            {
                SetupInstance();
            }
            return instance;
        }
    }
    private static void SetupInstance()
    {
        instance = FindAnyObjectByType<CameraManager>();
        if (instance == null)
        {
            GameObject cameraManager = new GameObject();
            instance = cameraManager.AddComponent<CameraManager>();
            cameraManager.name = "CameraManager";
            DontDestroyOnLoad(cameraManager);
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        GameObject vCam = GameObject.FindWithTag("VirtualCamera");
        virtualCamera = vCam.GetComponent<CinemachineCamera>();
    }
}
