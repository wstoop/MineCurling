using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Composites;

public class PlayerSelectedSpriteManager : MonoBehaviour
{

    [SerializeField]
    private List<GameObject> _sprites;
    [SerializeField]
    private List<GameObject> _joinedSprites;

    [SerializeField]
    private GameObject _startgameImage;
    [SerializeField]
    private InputActionReference buttonApressed;

    private PlayerInputManager _inputManager;

    private List<Image> _spritesImages = new List<Image>();
    private List<Image> _joinedSpritesImages = new List<Image>();
    private int _playerCount = 0;
    private void Awake()
    {
        Time.timeScale = 0;
        _inputManager = FindFirstObjectByType<PlayerInputManager>();
        _inputManager.onPlayerJoined += AddPlayer;
        foreach (var sprite in _sprites)
        {
            _spritesImages.Add(sprite.GetComponent<Image>());
        }
        foreach (var joinedSprite in _joinedSprites)
        {
            _joinedSpritesImages.Add(joinedSprite.GetComponent<Image>());
        }
         
    }

    public void AddPlayer(PlayerInput input)
    {
        _startgameImage.SetActive(true);
        buttonApressed.action.performed += ctx => StartGame();
        _playerCount++;
        _spritesImages[_playerCount - 1].enabled = false;
        _joinedSpritesImages[_playerCount - 1].enabled = true;

    }


    private void StartGame()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
