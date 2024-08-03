using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody2D;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private float initialSpeed = 3f;
    [SerializeField] private float acceleration = 0.1f;
    [SerializeField] private float accelerationInterval = 5f;

    private bool _resetting;
    private float _time;

    public float VelocityX => rigidBody2D.velocity.x;
    
    private void Start()
    {
        ResetBall();
    }

    public void ResetBall()
    {
        StartCoroutine(ResetBallAsync());
        // Invoke(nameof(EnableTrail), 0.1f);
    }

    private void Update()
    {
        _time += Time.deltaTime;

        if (_time < accelerationInterval) return;

        rigidBody2D.velocity *= 1 + acceleration;
        _time = 0;
    }

    private IEnumerator ResetBallAsync()
    {
        _resetting = true;
        while (rigidBody2D.velocity.magnitude > 0.01f)
        {
            yield return null;
            rigidBody2D.velocity *= 0.95f;
        }

        trailRenderer.emitting = false;
        transform.position = Vector3.zero;
        rigidBody2D.velocity = Vector2.zero;

        yield return new WaitForSeconds(1f);

        int y = Random.Range(0f, 1f) < 0.5f ? -1 : 1;
        rigidBody2D.velocity = initialSpeed * new Vector2(Random.Range(-1f, 1f), y).normalized;

        yield return null;

        trailRenderer.emitting = true;
        _resetting = false;
    }

    public void Reflect(Vector2 normal)
    {
        // if (_resetting) return;
        audioSource.Play();

        Vector2 reflect = Vector2.Reflect(rigidBody2D.velocity.normalized, normal.normalized).normalized;
        reflect = new Vector2(reflect.x, Mathf.Sign(normal.y) * Mathf.Abs(reflect.y));
        if (Mathf.Abs(reflect.y) < 0.5f)
        {
            reflect = new Vector2(reflect.x, Mathf.Sign(reflect.y) * 0.5f);
        }
        
        rigidBody2D.velocity = rigidBody2D.velocity.magnitude * reflect.normalized;
    }
}