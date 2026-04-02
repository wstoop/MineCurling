using UnityEngine;

public class PointCircle : MonoBehaviour
{
    [SerializeField] private EndPoint _endPoint;
    [SerializeField] private int _pointsToAdd;

    private void OnTriggerEnter(Collider other)
    {
        _endPoint.AddPoint(other.gameObject.layer, _pointsToAdd);
        _endPoint.UpdateText();
    }

    private void OnTriggerExit(Collider other)
    {
        _endPoint.RemovePoint(other.gameObject.layer, _pointsToAdd);
        _endPoint.UpdateText();
    }
}
