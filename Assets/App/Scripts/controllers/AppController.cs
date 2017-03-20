using UnityEngine;
using UnityEngine.Networking;

public class AppController : MonoBehaviour
{
    private static AppController _instance;

    public static AppController instance
    {
        get
        {
            if (_instance == null)
            {
                AppController app = FindObjectOfType<AppController>();
                if (app != null)
                {
                    initialize(app);
                } else {
                    initialize(new GameObject("AppController").AddComponent<AppController>());
                }
            }
            return _instance;
        }
    }

    private static void initialize(AppController app)
    {
        if (_instance == null)
        {
            _instance = app;
            DontDestroyOnLoad(app.gameObject);
            _instance.Init();
        } else {
            if (app != _instance)
            {
                Destroy(app.gameObject);
            }
        }
    }

    private NetworkManager _networkManager;

    public void Init()
    {
        _networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
    }

    public void CreateGame()
    {
        _networkManager.StartHost();
    }

    public void ConnectToGame(string address)
    {
        _networkManager.networkAddress = address;
        _networkManager.StartClient();
    }
}