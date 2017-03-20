using System;
using System.Collections;
using UniRx;
using UnityEngine;
using UnityEngine.Networking;

public class GameController : NetworkBehaviour
{
    public GameObject ship;
    public Vector2 spawnValues;

    public float difficultyIncreaseRate;
    public float baseHazardsSpeed;
    public int hazardsCount;

    public float startWait;
    public float baseSpawnWait;
    public float waveWait;

    private Pool _hazardsPool;

    private float _hazardsSpeed;
    private float _spawnWait;

    [SyncVar(hook="OnUpdatePaused")]
    private bool _paused;
    [SyncVar(hook="OnUpdateScore")]
    private int _score;

    [NonSerialized]
    public IntReactiveProperty score = new IntReactiveProperty();

    void Start()
    {
        _hazardsPool = GetComponent<Pool>();

        _hazardsSpeed = baseHazardsSpeed;
        _spawnWait = baseSpawnWait;
    }

    public override void OnStartServer()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardsCount; i++)
            {
                CmdSpawnHazard();
                yield return new WaitForSeconds(_spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
            ImproveDifficulty();
        }
    }

    [Command]
    void CmdSpawnHazard()
    {
        GameObject hazard = _hazardsPool.Create();
        hazard.transform.position = spawnValues;
        hazard.transform.rotation = Quaternion.identity;
        NetworkServer.Spawn(hazard);

        Move move = hazard.GetComponent<Move>();
        if (move != null)
        {
            move.speed = _hazardsSpeed;
        }
    }

    private void ImproveDifficulty()
    {
        _hazardsSpeed *= difficultyIncreaseRate;
        _spawnWait /= difficultyIncreaseRate;
    }

    public void AddScore(int delta)
    {
        if (isServer)
        {
            _score += delta;
            OnUpdateScore(_score);
        }
    }

    void OnUpdateScore(int value)
    {
        _score = value;
        score.Value = _score;
    }

    public void SwitchPause()
    {
        OnUpdatePaused(!_paused);
    }

    void OnUpdatePaused(bool value)
    {
        _paused = value;
        Time.timeScale = _paused ? 0.0f : 1.0f;
    }
}