using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    public Text scoreText;
    public Button pauseButton;

    public GameController gameController;

    private IDisposable _scoreSubscription;
    private IDisposable _pauseSubscription;

    void Start()
    {
        _scoreSubscription = gameController.score
            .Select(score => string.Format("Score: {0}", score))
            .SubscribeToText(scoreText);

        _pauseSubscription = pauseButton.onClick.AsObservable().Subscribe(_ =>
        {
            gameController.SwitchPause();
        });
    }

    private void OnDestroy()
    {
        if (_scoreSubscription != null)
        {
            _scoreSubscription.Dispose();
            _scoreSubscription = null;
        }

        if (_pauseSubscription != null)
        {
            _pauseSubscription.Dispose();
            _pauseSubscription = null;
        }
    }
}