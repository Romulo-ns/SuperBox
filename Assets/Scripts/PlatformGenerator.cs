using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    [Header("Platform Settings")]
    public GameObject[] platformPrefabs; // multiple prefabs
    public int initialPlatforms = 5;
    public float minDistance = 2f;
    public float maxDistance = 4f;
    public float minY = -1f;
    public float maxY = 2f;

    [Header("Player Reference")]
    public Transform player;

    private Transform lastPlatform;

    void Start()
    {
        // 1) First platform under player
        Vector3 startPosition = new Vector3(player.position.x, player.position.y - 1.5f, 0f);
        GameObject firstPlatform = Instantiate(GetRandomPlatform(), startPosition, Quaternion.identity);
        lastPlatform = firstPlatform.transform;

        AddLife(firstPlatform);

        // 2) Generate the rest
        Vector3 spawnPosition = lastPlatform.position;
        for (int i = 0; i < initialPlatforms - 1; i++)
        {
            spawnPosition.x += Random.Range(minDistance, maxDistance);
            spawnPosition.y = lastPlatform.position.y + Random.Range(minY, maxY);

            GameObject newPlatform = Instantiate(GetRandomPlatform(), spawnPosition, Quaternion.identity);
            lastPlatform = newPlatform.transform;

            AddLife(newPlatform);
        }
    }

    void Update()
    {
        if (Vector2.Distance(player.position, lastPlatform.position) < 8f)
        {
            SpawnPlatform();
        }
    }

    void SpawnPlatform()
    {
        Vector3 spawnPosition = lastPlatform.position;
        spawnPosition.x += Random.Range(minDistance, maxDistance);
        spawnPosition.y = lastPlatform.position.y + Random.Range(minY, maxY);

        GameObject newPlatform = Instantiate(GetRandomPlatform(), spawnPosition, Quaternion.identity);
        lastPlatform = newPlatform.transform;

        AddLife(newPlatform);
    }

    // Pick a random prefab from the array
    GameObject GetRandomPlatform()
    {
        int index = Random.Range(0, platformPrefabs.Length);
        return platformPrefabs[index];
    }

    void AddLife(GameObject platform)
    {
        PlatformLife life = platform.AddComponent<PlatformLife>();
        life.player = player;
    }
}
