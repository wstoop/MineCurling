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

    [SerializeField] PointCircle _threePoint;
    [SerializeField] PointCircle _twoPoint;
    [SerializeField] PointCircle _onePoint;

    public EndPointData GetData()
    {
        _data.RedPoints = _threePoint.RedPoints + _twoPoint.RedPoints + _onePoint.RedPoints;
        _data.BluePoints = _threePoint.BluePoints + _twoPoint.BluePoints + _onePoint.BluePoints;
        _data.GreenPoints = _threePoint.GreenPoints + _twoPoint.GreenPoints + _onePoint.GreenPoints;
        _data.YellowPoints = _threePoint.YellowPoints + _twoPoint.YellowPoints + _onePoint.YellowPoints;

        return _data;
    }
}
