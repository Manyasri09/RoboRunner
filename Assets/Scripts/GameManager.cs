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

        // Stop camera
        if (cameraController != null)
            cameraController.enabled = false;

        // Stop background scroll
        if (background != null)
            background.enabled = false;

        // Stop collectible spawner
        if (collectibleSpawner != null)
            collectibleSpawner.enabled = false;

        // Stop obstacle spawner
        if (obstacleSpawner != null)
            obstacleSpawner.enabled = false;
    }
}
