using UnityEngine;

public class HPManager : MonoBehaviour
{
    GameObject[] heartContainers;

    [SerializeField] Player player;
    [SerializeField] Transform heartsParent;
    public GameObject heartPrefab;


    private static HPManager instance;
    public static HPManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<HPManager>();
            }
            return instance;
        }
    }
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        heartContainers = new GameObject[player.Hp];

        InstantiateHeart();
        SetHeartContainers();
    }

    public void SetHeartContainers()
    {
        for (int i = 0; i < heartContainers.Length; i++)
        {
            if (i < player.Hp)
            {
                heartContainers[i].SetActive(true);
            }
            else
            {
                heartContainers[i].SetActive(false);
            }
        }
    }


    void InstantiateHeart()
    {
        for(int i=0; i < player.Hp; i++)
        {
            GameObject heart = Instantiate(heartPrefab);
            heart.transform.SetParent(heartsParent, false);
            heartContainers[i] = heart;
        }
    }

    public void UpdatePlayer(GameObject player)
    {
        this.player = player.GetComponent<Player>();
    }
}
