using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

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
        var curlingStone = other.gameObject.GetComponent<CurlingStoneController>();
        if (curlingStone != null)
        {
            curlingStone.IsStoppable = true;
            other.gameObject.GetComponent<PlayerInput>().DeactivateInput();
        }
        UpdateText();
    }
}
