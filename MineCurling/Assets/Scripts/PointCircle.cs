using UnityEngine;

public class PointCircle : MonoBehaviour
{
    [SerializeField] private EndPoint _endPoint;

    private void OnTriggerEnter(Collider other)
    {
        _endPoint.AddPoint(other.gameObject.layer);
        _endPoint.UpdateText();
    }

    private void OnTriggerExit(Collider other)
    {
        _endPoint.RemovePoint(other.gameObject.layer);
        _endPoint.UpdateText();
    }
}
