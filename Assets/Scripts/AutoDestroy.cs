using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float lifetime = 10f; // how long before collectible despawns

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
