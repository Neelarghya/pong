using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody2D;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float edgeDistance = 2.5f;

    private void Start()
    {
        Input.gyro.enabled = true;
    }

    private void Update()
    {
#if UNITY_EDITOR
        float input = Input.GetAxis("Horizontal");
#else
        float input = Input.gyro.rotationRateUnbiased.y;
#endif
        float x = rigidBody2D.position.x;
        float y = rigidBody2D.position.y;

        if (x >= edgeDistance && input > 0)
        {
            rigidBody2D.MovePosition(new Vector2(edgeDistance, y));
            rigidBody2D.velocity = Vector2.zero;
        }
        else if (x <= -edgeDistance && input < 0)
        {
            rigidBody2D.MovePosition(new Vector2(-edgeDistance, y));
            rigidBody2D.velocity = Vector2.zero;
        }
        else
        {
            rigidBody2D.velocity = speed * input * Vector2.right;
        }
    }
}