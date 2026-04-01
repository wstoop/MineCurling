using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    private PlayerInputManager _inputManager;
    [SerializeField]
    private List<GameObject> _startScreenElements;
    [SerializeField]
    private List<GameObject> _registerScreenElements;
    [SerializeField]
    private List<GameObject> _gameScreenElements;
    [SerializeField]
    private List<GameObject> _endScreenElements;

    public enum ScreenType
    {
        StartScreen,
        RegisterScreen,
        GameScreen,
        EndScreen
    }

    private void Awake()
    {
        _inputManager.enabled = false;
        SetActiveElements(_registerScreenElements, false);
        SetActiveElements(_gameScreenElements, false);
        SetActiveElements(_endScreenElements, false);

        SetActiveElements(_startScreenElements, true);
        Time.timeScale = 0;
    }

    private void SetActiveElements(List<GameObject> elements, bool state)
    {
        foreach (var element in elements)
        {
            element.SetActive(state);
        }
    }
    public void SetCurrentScreen(string screenName)
    {
        ScreenType screen = (ScreenType)System.Enum.Parse(typeof(ScreenType), screenName);

        //_inputManager.enabled = false;
        SetActiveElements(_startScreenElements, false);
        SetActiveElements(_registerScreenElements, false);
        SetActiveElements(_gameScreenElements, false);
        SetActiveElements(_endScreenElements, false);
        switch (screen)
        {
            case ScreenType.StartScreen:
                SetActiveElements(_startScreenElements, true);
                break;
            case ScreenType.RegisterScreen:
                SetActiveElements(_registerScreenElements, true);
                _inputManager.enabled = true;
                break;
            case ScreenType.GameScreen:
                SetActiveElements(_gameScreenElements, true);
                Time.timeScale = 1f;
                break;
            case ScreenType.EndScreen:
                SetActiveElements(_endScreenElements, true);
                break;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}