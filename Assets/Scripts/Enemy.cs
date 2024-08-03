using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody2D;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float growthAmount = 1f;
    [SerializeField] private int fullAdaptationThreshold = 5;
    [SerializeField] private float edgeDistance = 2.5f;
    [SerializeField] private BallController ball;
    [SerializeField] private TMP_Text speedDisplay;

    private float _input;
    private const float Threshold = 0.5f;
    [SerializeField] private int _growth;
    [SerializeField] private float _currentSpeed;
    [SerializeField] private int _adaptationCount = -1;

    private void Start()
    {
        Adapt();
    }

    public void GetBetter()
    {
        _growth++;
        Adapt();
    }

    public void GetWorse()
    {
        _growth = Mathf.Max(_growth - 1, -5);
        Adapt();
    }

    private void Adapt()
    {
        _adaptationCount++;
        _currentSpeed = speed + _growth * growthAmount;

        if (_adaptationCount >= fullAdaptationThreshold)
        {
            _adaptationCount = 0;
            speed = _currentSpeed + _growth * growthAmount;
            _growth = (int)(_growth / (fullAdaptationThreshold / 2f));
            _currentSpeed = speed + _growth * growthAmount;
        }

        speedDisplay.text = _currentSpeed.ToString();
    }

    private void FixedUpdate()
    {
        float x = rigidBody2D.position.x;
        float y = rigidBody2D.position.y;

        float ballX = ball.transform.position.x;
        float ballY = ball.transform.position.y;
        float meetPointX = ballX + 0.1f * (y - ballY) * ball.VelocityX;

        Debug.DrawLine(new Vector3(ballX, ballY, 0), new Vector3(meetPointX, y, 0), Color.cyan);

        _input = (meetPointX - x) switch
        {
            < -Threshold => -1f,
            > Threshold => 1f,
            _ => 0f
        };

        if (x >= edgeDistance && (rigidBody2D.velocity.x > 0 || rigidBody2D.velocity.x == 0 && _input > 0))
        {
            rigidBody2D.MovePosition(new Vector2(edgeDistance, y));
            rigidBody2D.velocity = Vector2.zero;
        }
        else if (x <= -edgeDistance && (rigidBody2D.velocity.x < 0 || rigidBody2D.velocity.x == 0 && _input < 0))
        {
            rigidBody2D.MovePosition(new Vector2(-edgeDistance, y));
            rigidBody2D.velocity = Vector2.zero;
        }
        else
        {
            float decelerationFactor = 1;
            if (!Mathf.Approximately(Mathf.Sign(rigidBody2D.velocity.x), Mathf.Sign(_input)))
                decelerationFactor = 2.5f;
            rigidBody2D.velocity += decelerationFactor * _currentSpeed * Time.fixedDeltaTime * _input * Vector2.right;
        }
    }
}