using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject[] collectiblePrefabs;  // your 3 collectible prefabs
    public float spawnInterval = 2f;         // seconds between spawns
    public float spawnDistance = 15f;        // distance ahead of the camera
    public float verticalRange = 3.5f;       // how high/low collectibles can appear

    [Header("References")]
    public Transform cameraTransform;        // reference to main camera

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnCollectible();
            timer = 0f;
        }
    }

    void SpawnCollectible()
    {
        // Choose a random collectible prefab
        int index = Random.Range(0, collectiblePrefabs.Length);
        GameObject prefab = collectiblePrefabs[index];

        // Random vertical position
        float randomY = Random.Range(-verticalRange, verticalRange);

        // Spawn position (ahead of camera)
        Vector3 spawnPos = new Vector3(cameraTransform.position.x + spawnDistance, randomY, 0f);

        // Instantiate collectible
        Instantiate(prefab, spawnPos, Quaternion.identity);

        Debug.Log("<color=cyan>[Spawner]</color> Spawned " + prefab.name + " at Y=" + randomY);
    }
}
