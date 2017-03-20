using System;
using App.Scripts.utils;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class MenuView : MonoBehaviour
{
    public Button startGameButton;
    public Button connectButton;
    public Button quitButton;

    public CanvasRenderer enterIPPanel;
    public Text enterIPText;
    public Button enterIPButton;

    private IDisposable _startGameSubscription;
    private IDisposable _connectSubscription;
    private IDisposable _quitSubscription;

    private IDisposable _connectToIPSubscription;

    void Start()
    {
        _startGameSubscription = startGameButton.onClick.AsObservable().Subscribe(_ =>
        {
            AppController.instance.CreateGame();
        });

        _connectSubscription = connectButton.onClick.AsObservable().Subscribe(_ =>
        {
            enterIPPanel.gameObject.SetActive(true);
        });

        _quitSubscription = quitButton.onClick.AsObservable().Subscribe(_ =>
        {
            Application.Quit();
        });

        _connectToIPSubscription = enterIPButton.onClick.AsObservable().Subscribe(_ =>
        {
            if (IPValidator.Validate(enterIPText.text))
            {
                enterIPPanel.gameObject.SetActive(false);
                AppController.instance.ConnectToGame(enterIPText.text);
            } else {
                Debug.LogError("wrong ip address format");
            }
        });
    }

    private void OnDestroy()
    {
        if (_startGameSubscription != null)
        {
            _startGameSubscription.Dispose();
            _startGameSubscription = null;
        }

        if (_connectSubscription != null)
        {
            _connectSubscription.Dispose();
            _connectSubscription = null;
        }

        if (_quitSubscription != null)
        {
            _quitSubscription.Dispose();
            _quitSubscription = null;
        }

        if (_connectToIPSubscription != null)
        {
            _connectToIPSubscription.Dispose();
            _connectToIPSubscription = null;
        }
    }
}