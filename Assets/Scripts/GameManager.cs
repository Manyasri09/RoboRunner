using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Scene References")]
    public CameraController cameraController;
    public LoopingBackground background;
    public CollectibleSpawner collectibleSpawner;
    public ObstacleSpawner obstacleSpawner;

    private bool isGameOver = false;

    void Awake()
    {
        Instance = this;
    }

    public void StopAllSystems()
    {
        if (isGameOver) return;
        isGameOver = true;

        Debug.Log("<color=cyan>[GameManager]</color> Stopping all systems...");

        
        if (cameraController != null)
            cameraController.enabled = false;

        
        if (background != null)
            background.enabled = false;

        
        if (collectibleSpawner != null)
            collectibleSpawner.enabled = false;

        if (obstacleSpawner != null)
            obstacleSpawner.enabled = false;
    }
}
