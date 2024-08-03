using UnityEngine;
using UnityEngine.Events;

public class Goal : MonoBehaviour
{
    [SerializeField] private UnityEvent onGoal;
    [SerializeField] private AudioSource onGoalAudio;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            onGoal?.Invoke();
            onGoalAudio?.Play();
        }
    }
}