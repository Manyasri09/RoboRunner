using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float lifetime = 10f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("<color=red>[Obstacle]</color> Player hit obstacle!");
            other.GetComponent<Player>().Die();
            Destroy(gameObject);
        }
    }
}
