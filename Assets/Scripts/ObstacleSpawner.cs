using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject[] obstaclePrefabs;  
    public float spawnInterval = 2.5f;    
    public float spawnDistance = 18f;     
    public float verticalRange = 3.5f;    
    public Transform cameraTransform;     

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
        
        int index = Random.Range(0, obstaclePrefabs.Length);
        GameObject prefab = obstaclePrefabs[index];

       
        float randomY = Random.Range(-verticalRange, verticalRange);

       
        Vector3 spawnPos = new Vector3(cameraTransform.position.x + spawnDistance, randomY, 0f);

        Instantiate(prefab, spawnPos, Quaternion.identity);
    }
}
