using System;
using System.Linq;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerInputManager))]
public class PlayerJoinHandler : MonoBehaviour
{
    private const int _maxPlayerCount = 4;

    [SerializeField]
    private Transform[] _spawnPositions = new Transform[_maxPlayerCount];

    private PlayerInputManager _manager = null;

    private void OnValidate()
    {
        if (_spawnPositions.Length != _maxPlayerCount)
        {
            Array.Resize(ref _spawnPositions, _maxPlayerCount);
        }
    }

    private void Awake()
    {
        _manager = GetComponent<PlayerInputManager>();
        _manager.onPlayerJoined += OnPlayerJoined;
    }

    public void OnPlayerJoined(PlayerInput input)
    {
        if (_manager == null) return;
        if (_manager.playerCount >= _maxPlayerCount) return;
        if (_spawnPositions[input.playerIndex] == null) return;

        var rb = input.GetComponent<Rigidbody>();

        if (rb == null) return;

        rb.position = _spawnPositions[input.playerIndex].position;
        rb.rotation = _spawnPositions[input.playerIndex].rotation;

    }
}
