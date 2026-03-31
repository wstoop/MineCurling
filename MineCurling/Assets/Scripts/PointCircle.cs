using UnityEngine;

public class PointCircle : MonoBehaviour
{
    private int _redPoints = 0;
    private int _bluePoints = 0;
    private int _greenPoints = 0;
    private int _yellowPoints = 0;

    public int RedPoints => _redPoints;
    public int BluePoints => _bluePoints;
    public int GreenPoints => _greenPoints;
    public int YellowPoints => _yellowPoints;

    private void OnTriggerEnter(Collider other)
    {
        switch(other.gameObject.layer)
        {
            case 6:
                _redPoints++;
                break;
            case 7:
                _bluePoints++;
                break;
            case 8:
                _greenPoints++;
                break;
            case 9:
                _yellowPoints++;
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch(other.gameObject.layer)
        {
            case 6:
                _redPoints--;
                break;
            case 7:
                _bluePoints--;
                break;
            case 8:
                _greenPoints--;
                break;
            case 9:
                _yellowPoints--;
                break;
        }
    }

    
}
