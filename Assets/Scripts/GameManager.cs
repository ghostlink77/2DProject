using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    int playerStock = 3;
    int startScore = 0;
    int currScore = 0;

    SpawnPoint playerSpawnPoint;
    CameraManager cameraManager;
    GameObject player;
    TextMeshProUGUI stockText;
    TextMeshProUGUI scoreText;

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
    void Start()
    {
        cameraManager = CameraManager.Instance;
        playerSpawnPoint = GameObject.Find("PlayerSpawnPoint").GetComponent<SpawnPoint>();
        stockText = GameObject.Find("StockText").GetComponent<TextMeshProUGUI>();
        scoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        SpawnPlayer();
    }

    public void GetScore(int score)
    {
        currScore += score;
        scoreText.text = "Score : " + currScore.ToString("D6");
    }

    public void SpawnPlayer()
    {
        if (playerSpawnPoint != null)
        {
            player = playerSpawnPoint.SpawnObject();
            cameraManager.virtualCamera.Follow = player.transform;
            stockText.text = "x" + playerStock.ToString();
            currScore = startScore;
            scoreText.text = "Score : " + currScore.ToString("D6");
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
        Invoke("SpawnPlayer", 2f);

        playerStock--;
        if (playerStock <= 0)
        {
            GameOver();
        }
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
