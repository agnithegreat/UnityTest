using UnityEngine;

public class Score : MonoBehaviour
{
    public int score;

    private Health _health;
    private GameController _gameController;

    private void Start()
    {
        _health = GetComponent<Health>();
        _gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    private void OnDestroy()
    {
        if (_gameController != null && _health != null && !_health.alive)
        {
            _gameController.AddScore(score);
        }
    }
}