using UnityEngine;

public class Reflect : MonoBehaviour
{
    [SerializeField] private float reflectDirectionY = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            // Vector2 closestPoint = other.ClosestPoint(transform.position);
            Vector2 normal = other.transform.position - transform.position;
            normal = new Vector2(normal.x, reflectDirectionY);
            other.GetComponent<BallController>().Reflect(normal);
        }
    }
}