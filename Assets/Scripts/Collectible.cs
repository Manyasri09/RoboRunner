using UnityEngine;

public class Collectible : MonoBehaviour
{
    [Header("Collectible Settings")]
    [Tooltip("How many points this collectible gives.")]
    public int value = 5; 

    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.CompareTag("Player"))
        {
            Debug.Log("<color=yellow>[Collectible]</color> Collected! +5 Points");

            
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddScore(value);
            }
            else
            {
                Debug.LogWarning("[Collectible] No ScoreManager found in scene!");
            }

            
            Destroy(gameObject);
        }
    }
}
