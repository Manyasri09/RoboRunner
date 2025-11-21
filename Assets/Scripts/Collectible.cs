using UnityEngine;

public class Collectible : MonoBehaviour
{
    [Header("Collectible Settings")]
    [Tooltip("How many points this collectible gives.")]
    public int value = 5; // ✅ each collectible = +5 points

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Detect player collision
        if (other.CompareTag("Player"))
        {
            Debug.Log("<color=yellow>[Collectible]</color> Collected! +5 Points");

            // Add +5 score through ScoreManager
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddScore(value);
            }
            else
            {
                Debug.LogWarning("[Collectible] No ScoreManager found in scene!");
            }

            // Destroy collectible once collected
            Destroy(gameObject);
        }
    }
}
