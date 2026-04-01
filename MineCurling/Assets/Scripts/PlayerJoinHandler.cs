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

    [SerializeField]
    private Material[] _playerMaterials = new Material[_maxPlayerCount];

    [SerializeField]
    private Material[] _broomMaterials = new Material[_maxPlayerCount];

    private PlayerInputManager _manager = null;

    private void OnValidate()
    {
        // limit the amount of spawn positions the same as the player count
        if(_spawnPositions.Length != _maxPlayerCount)
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

        if (_playerMaterials[input.playerIndex] == null) return;
        var meshRenderStone = input.transform.Find("Visuals").Find("CurlingRockEdited").GetComponent<MeshRenderer>();
        meshRenderStone.sharedMaterial = _playerMaterials[input.playerIndex];

        if (_broomMaterials[input.playerIndex] == null) return;
        var meshRenderBroom = input.transform.Find("Visuals").Find("Broom").GetComponent<MeshRenderer>();
        meshRenderBroom.sharedMaterial = _broomMaterials[input.playerIndex];
    }
}
 