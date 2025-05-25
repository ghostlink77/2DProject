using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] GameObject objectToSpawn;


    public GameObject SpawnObject()
    {
        if (objectToSpawn != null)
        {
            return Instantiate(objectToSpawn, transform.position, transform.rotation);
        }
        else
        {
            Debug.LogError("Object to spawn is not set.");
            return null;
        }
    }
}
