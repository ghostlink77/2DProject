using UnityEngine;

public class EnemySpawner : SpawnPoint
{
    GameObject spawned = null;
    Camera mainCamera;
    Vector2 scrPos;

    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        scrPos = mainCamera.WorldToViewportPoint(transform.position);
        bool isOutOfScreen = scrPos.x < 0 || scrPos.x > 1 || scrPos.y < 0 || scrPos.y > 1;
        if (spawned != null && isOutOfScreen)
        {
            return;
        }
        else if(spawned == null && isOutOfScreen)
        {
            Respawn();
        }
    }

    void Respawn()
    {
        if(spawned != null)
        {
            Destroy(spawned);
            spawned = null;
        }
        spawned = SpawnObject();
    }
}
