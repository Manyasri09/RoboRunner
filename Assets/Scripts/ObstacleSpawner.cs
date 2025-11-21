using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject[] obstaclePrefabs;  // list of obstacle prefabs
    public float spawnInterval = 2.5f;    // time between spawns
    public float spawnDistance = 18f;     // how far ahead obstacles appear
    public float verticalRange = 3.5f;    // how high/low they can appear
    public Transform cameraTransform;     // reference to camera

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnObstacle();
            timer = 0f;
        }
    }

    void SpawnObstacle()
    {
        // Pick a random obstacle prefab
        int index = Random.Range(0, obstaclePrefabs.Length);
        GameObject prefab = obstaclePrefabs[index];

        // Pick a random height (Y)
        float randomY = Random.Range(-verticalRange, verticalRange);

        // Spawn position ahead of camera
        Vector3 spawnPos = new Vector3(cameraTransform.position.x + spawnDistance, randomY, 0f);

        Instantiate(prefab, spawnPos, Quaternion.identity);
    }
}
