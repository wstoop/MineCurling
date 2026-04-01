using TMPro;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    private int _givenPoints = 3;

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

    public void AddPoint(LayerMask layer)
    {
        switch(layer.value)
        {
            case 6:
                _data.RedPoints++;
                break;
            case 7:
                _data.BluePoints++;
                break;
            case 8:
                _data.GreenPoints++;
                break;
            case 9:
                _data.YellowPoints++;
                break;
        }
    }

    public void RemovePoint(LayerMask layer)
    {
        switch (layer.value)
        {
            case 6:
                _data.RedPoints--;
                break;
            case 7:
                _data.BluePoints--;
                break;
            case 8:
                _data.GreenPoints--;
                break;
            case 9:
                _data.YellowPoints--;
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_givenPoints == 0) return;

        switch (other.gameObject.layer)
        {
            case 6:
                _data.RedPoints += _givenPoints;
                --_givenPoints;
                UpdateText();
                break;
            case 7:
                _data.BluePoints += _givenPoints;
                --_givenPoints;
                UpdateText();
                break;
            case 8:
                _data.GreenPoints += _givenPoints;
                --_givenPoints;
                UpdateText();
                break;
            case 9:
                _data.YellowPoints += _givenPoints;
                --_givenPoints;
                UpdateText();
                break;
        }
    }
}
