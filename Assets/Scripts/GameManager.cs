using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    int playerStock = 3;
    int startScore = 0;
    int currScore = 0;

    [SerializeField] SpawnPoint playerSpawnPoint;
    [SerializeField] CameraManager cameraManager;
    [SerializeField] GameObject player;
    [SerializeField] TextMeshProUGUI stockText;
    [SerializeField] TextMeshProUGUI scoreText;

    private static GameManager instance;
    public static GameManager Instance
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
        instance = FindAnyObjectByType<GameManager>();
        if (instance == null)
        {
            GameObject gameManager = new GameObject();
            instance = gameManager.AddComponent<GameManager>();
            gameManager.name = "GameManager";
            DontDestroyOnLoad(gameManager);
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
        if (scene.name == "MainStage")
        {
            SceneSetup();
        }
    }

    void SceneSetup()
    {
        cameraManager = CameraManager.Instance;
        playerSpawnPoint = GameObject.Find("PlayerSpawnPoint").GetComponent<SpawnPoint>();
        stockText = GameObject.Find("StockText").GetComponent<TextMeshProUGUI>();
        scoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        playerStock = 3;
        startScore = 0;
        SpawnPlayer();
    }

    public void GetScore(int score)
    {
        currScore += score;
        scoreText.text = currScore.ToString("D7");
    }

    public void SpawnPlayer()
    {
        if (playerSpawnPoint != null)
        {
            player = playerSpawnPoint.SpawnObject();
            stockText.text = "x" + playerStock.ToString();
            currScore = startScore;
            scoreText.text = currScore.ToString("D7");

            cameraManager.virtualCamera.Follow = player.transform;
        }
        else
        {
            Debug.LogError("PlayerSpawnPoint is not set in GameManager.");
            return;
        }
    }

    public void SetCheckPoint(Vector2 pos)
    {
        playerSpawnPoint.transform.position = pos;
        startScore = currScore;
    }

    public void PlayerDie()
    {
        currScore = startScore;
        Destroy(player, 1f);
        player = null;
        cameraManager.virtualCamera.Follow = null;

        playerStock--;
        if (playerStock <= 0)
        {
            GameOver();
            return;
        }
        Invoke("SpawnPlayer", 2f);
    }
    public void GameOver()
    {
        Debug.Log("Game Over");
        SceneManager.LoadScene("GameOver");
    }
    public void GameClear()
    {
        SceneManager.LoadScene("GameClear");
    }
    public void LoadStage1()
    {
        SceneManager.LoadScene("MainStage");
    }

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("No more scenes to load.");
        }
    }

    public void GameExit()
    {
        Application.Quit();

    }
}
