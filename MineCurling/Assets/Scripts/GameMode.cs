using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    [SerializeField]
    private float _firstWaveStart = 5.0f;

    [SerializeField]
    private float _waveStartFrequency = 15.0f;

    [SerializeField]
    private float _waveEndFrequency = 7.0f;

    [SerializeField]
    private float _waveFrequencyIncrement = 0.5f;

    private float _currentFrequency = 0.0f;

    private void Awake()
    {
        _currentFrequency = _waveStartFrequency;

        Invoke(STARTNEWWAVE_METHOD, _firstWaveStart);
        MineSpawnManager.Instance.SpawnMine();
    }

    const string STARTNEWWAVE_METHOD = "StartNewWave";

    void StartNewWave()
    {
        SpawnManager.Instance.SpawnWave();

        _currentFrequency = Mathf.Clamp(_currentFrequency - _waveFrequencyIncrement, _waveEndFrequency, _currentFrequency);

        Invoke(STARTNEWWAVE_METHOD, _currentFrequency);
    }
}
