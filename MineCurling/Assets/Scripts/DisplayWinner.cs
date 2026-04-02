using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class DisplayWinner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _playerIcons;

    private EndPoint _endPoint;
    private void Awake()
    {
        _endPoint = FindFirstObjectByType<EndPoint>();
    }
    private void DisplayWinnerIcon()
    {
        foreach (var icon in _playerIcons)
        {
            icon.SetActive(false);
        }
        _playerIcons[GetWinningIndex()].SetActive(true);
    }

    private int GetWinningIndex()
    {
        var data = _endPoint.GetData();
        int highestPoints = Mathf.Max(data.RedPoints, data.BluePoints, data.GreenPoints, data.YellowPoints);
        int winningIndex;
        switch (highestPoints)
        {
            case int points when points == data.RedPoints:
                winningIndex = 0;
                break;
            case int points when points == data.BluePoints:
                winningIndex = 1;
                break;
            case int points when points == data.GreenPoints:
                winningIndex = 2;
                break;
            case int points when points == data.YellowPoints:
                winningIndex = 3;
                break;
            default:
                return 0;
        }
        return winningIndex;
    }

    private void OnEnable()
    {
        DisplayWinnerIcon();
    }
}
