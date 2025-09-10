using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    [Header("Platform Settings")]
    public GameObject[] platformPrefabs;
    public int initialPlatforms = 5;
    public float minDistance = 2f;
    public float maxDistance = 4f;
    public float minY = -1f;
    public float maxY = 2f;

    [Header("Spike Settings")]
    public GameObject spikePrefab;
    [Range(0f, 1f)] public float spikeChance = 0.3f;

    [Header("Player Reference")]
    public Transform player;

    private Transform lastPlatform;

    void Start()
    {
        // First platform under the player (no spikes here)
        Vector3 startPosition = new Vector3(player.position.x, player.position.y - 1.5f, 0f);
        GameObject firstPlatform = Instantiate(GetRandomPlatform(), startPosition, Quaternion.identity);
        lastPlatform = firstPlatform.transform;
        AddLife(firstPlatform);

        // Generate the rest
        Vector3 spawnPosition = lastPlatform.position;
        for (int i = 0; i < initialPlatforms - 1; i++)
        {
            spawnPosition.x += Random.Range(minDistance, maxDistance);
            spawnPosition.y = lastPlatform.position.y + Random.Range(minY, maxY);

            GameObject newPlatform = Instantiate(GetRandomPlatform(), spawnPosition, Quaternion.identity);
            lastPlatform = newPlatform.transform;

            AddLife(newPlatform);
            SpawnSpike(newPlatform);
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
        SpawnSpike(newPlatform);
    }

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

    void SpawnSpike(GameObject platform)
    {
        if (spikePrefab == null || Random.value >= spikeChance) return;

        Collider2D platformCol = platform.GetComponent<Collider2D>();
        if (platformCol == null) return;

        Bounds pB = platformCol.bounds;

        // Instantiate spike first
        GameObject spike = Instantiate(spikePrefab);
        spike.transform.SetParent(platform.transform);

        Collider2D spikeCol = spike.GetComponentInChildren<Collider2D>();
        Bounds sB;
        if (spikeCol != null) sB = spikeCol.bounds;
        else
        {
            SpriteRenderer sr = spike.GetComponentInChildren<SpriteRenderer>();
            if (sr == null) { Destroy(spike); return; }
            sB = sr.bounds;
        }

        // Horizontal clamp so spike stays fully on top
        float halfW = sB.extents.x;
        float minX = pB.min.x + halfW;
        float maxX = pB.max.x - halfW;
        if (minX > maxX) { minX = maxX = pB.center.x; }
        float spawnX = Random.Range(minX, maxX);

        // Align the bottom (minY) of the spike with the top (maxY) of the platform
        float offsetY = pB.max.y - sB.min.y;
        Vector3 spikePos = new Vector3(spawnX, spike.transform.position.y + offsetY, 0f);

        spike.transform.position = spikePos;
    }
}
