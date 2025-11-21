using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject[] collectiblePrefabs;  
    public float spawnInterval = 2f;         
    public float spawnDistance = 15f;        
    public float verticalRange = 3.5f;       

    [Header("References")]
    public Transform cameraTransform;        

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
        
        int index = Random.Range(0, collectiblePrefabs.Length);
        GameObject prefab = collectiblePrefabs[index];

        
        float randomY = Random.Range(-verticalRange, verticalRange);

        
        Vector3 spawnPos = new Vector3(cameraTransform.position.x + spawnDistance, randomY, 0f);

        
        Instantiate(prefab, spawnPos, Quaternion.identity);

        Debug.Log("<color=cyan>[Spawner]</color> Spawned " + prefab.name + " at Y=" + randomY);
    }
}
