using TMPro;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
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
}
