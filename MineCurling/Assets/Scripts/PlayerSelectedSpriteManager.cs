using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Composites;
using EasyTransition;

public class PlayerSelectedSpriteManager : MonoBehaviour
{

    [SerializeField]
    private List<GameObject> _sprites;
    [SerializeField]
    private List<GameObject> _joinedSprites;

    [SerializeField]
    private GameObject _endScreen;
    [SerializeField]
    private GameObject _startgameImage;
    [SerializeField]
    private GameObject _pressYToJoin;
    [SerializeField]
    private InputActionReference buttonApressed;

    [SerializeField]
    private AudioSource _menuTheme;
    [SerializeField]
    private AudioSource _gameTheme;

    [SerializeField]
    private AudioSource _pop;

    [SerializeField]
    private AudioSource _horn;

    private PlayerInputManager _inputManager;

    private List<Image> _spritesImages = new List<Image>();
    private List<Image> _joinedSpritesImages = new List<Image>();
    private int _playerCount = 0;
    private bool _hasFrozenTime = false;
    private void Awake()
    {
        _menuTheme.Play();
        _startgameImage.SetActive(false);
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
        _pop.pitch = Random.Range(0.8f, 1.2f);
        _pop.Play();
        _startgameImage.SetActive(true);
        buttonApressed.action.performed += ctx => StartGame();

        _playerCount++;
        _spritesImages[_playerCount - 1].enabled = false;
        _joinedSpritesImages[_playerCount - 1].enabled = true;

    }


    private void StartGame()
    {
        _horn.Play();
        var Images = GetComponentsInChildren<Transform>();
        foreach (var image in Images)
        {
            if (image.gameObject == this.gameObject)
                continue;
            
            image.gameObject.SetActive(false);
        }
        _menuTheme.Stop();
        _gameTheme.Play();
        Time.timeScale = 1;
    }

    public void ShowEndScreen()
    {
        _endScreen.SetActive(true);
    }

    private void Update()
    {
        if(!FindAnyObjectByType<Transition>() && !_hasFrozenTime)
        {
            Time.timeScale = 0;
            _hasFrozenTime = true;
        }
    }
}
