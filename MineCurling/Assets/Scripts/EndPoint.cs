using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class EndPoint : MonoBehaviour
{
    private int _givenPoints = 3;
    private int _playerCount = 0;

    public struct EndPointData
    {
        public int RedPoints;
        public int BluePoints;
        public int GreenPoints;
        public int YellowPoints;
    }

    private EndPointData _data;

    [SerializeField] TextMeshProUGUI _redText;
    [SerializeField] TextMeshProUGUI _blueText;
    [SerializeField] TextMeshProUGUI _greenText;
    [SerializeField] TextMeshProUGUI _yellowText;

    private GameMode _gameMode;

    private void Awake()
    {
        _playerCount = FindFirstObjectByType<PlayerInputManager>().playerCount;
        FindFirstObjectByType<PlayerInputManager>().onPlayerJoined += IncreaseCount;
        _gameMode = GameObject.Find("GameMode").GetComponent<GameMode>();
    }

    private void IncreaseCount(PlayerInput input)
    {
        _playerCount += 1;
    }
    public void AddPoint(LayerMask layer, int points)
    {
        switch(layer.value)
        {
            case 6:
                _data.RedPoints += points;
                break;
            case 7:
                _data.BluePoints += points;
                break;
            case 8:
                _data.GreenPoints += points;
                break;
            case 9:
                _data.YellowPoints += points;
                break;
        }
    }

    public void RemovePoint(LayerMask layer, int points)
    {
        switch (layer.value)
        {
            case 6:
                _data.RedPoints -= points;
                break;
            case 7:
                _data.BluePoints -= points;
                break;
            case 8:
                _data.GreenPoints -= points;
                break;
            case 9:
                _data.YellowPoints -= points;
                break;
        }
    }

    public void UpdateText()
    {
        _redText.text = _data.RedPoints.ToString();
        _blueText.text = _data.BluePoints.ToString();
        _greenText.text = _data.GreenPoints.ToString();
        _yellowText.text = _data.YellowPoints.ToString();
    }

    public void ResetPoints()
    {
        _givenPoints = 3;
        _playerCount = FindFirstObjectByType<PlayerInputManager>().playerCount;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ghost")) return;

        --_playerCount;

        if (_givenPoints > 0)
        {
            switch (other.gameObject.layer)
            {
                case 6:
                    _data.RedPoints += _givenPoints;
                    break;
                case 7:
                    _data.BluePoints += _givenPoints;
                    break;
                case 8:
                    _data.GreenPoints += _givenPoints;
                    break;
                case 9:
                    _data.YellowPoints += _givenPoints;
                    break;
            }

            --_givenPoints;
        }
        
        var curlingStone = other.gameObject.GetComponent<CurlingStoneController>();
        if (curlingStone != null)
        {
            curlingStone.IsStoppable = true;
            other.gameObject.GetComponent<PlayerInput>().DeactivateInput();
        }
        UpdateText();

        if(_playerCount == 0)
        {
            ResetPoints();
            _gameMode.ReloadScene();
        }
    }

    public EndPointData GetData()
    {
        return _data;
    }
}
