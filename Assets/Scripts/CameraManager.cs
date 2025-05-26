using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraManager : MonoBehaviour
{
    public CinemachineCamera virtualCamera;
    [SerializeField] GameObject vCam = null;

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
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        vCam = GameObject.Find("CinemachineCamera");
        if (vCam == null)
        {
            return;
        }
        virtualCamera = vCam.GetComponent<CinemachineCamera>();
    }
}
