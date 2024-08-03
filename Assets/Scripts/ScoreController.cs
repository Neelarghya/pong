using System;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private TMP_Text playerScore;
    [SerializeField] private TMP_Text enemyScore;
    [SerializeField] private TMP_Text timer;

    private int _playerScore;
    private int _enemyScore;

    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Application.targetFrameRate = -1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    public void PlayerScores()
    {
        _playerScore++;
        playerScore.text = _playerScore.ToString();
    }

    public void EnemyScores()
    {
        _enemyScore++;
        enemyScore.text = _enemyScore.ToString();
    }
}