using System;
using UnityEngine;

[RequireComponent(typeof(CurlingStoneController), typeof(Rigidbody))]
public class CurlingFeedback : MonoBehaviour
{

    [Header("Visuals:")]
    [SerializeField]
    private GameObject _broom = null;
    [SerializeField]
    private GameObject _stone = null;


    [Header("Brooming:")]
    [SerializeField, Min(1f)]
    private float _broomSpeed = 5f;
    [SerializeField]
    private Transform _leftSweepTarget = null;
    [SerializeField]
    private Transform _rightSweepTarget = null;

    private Transform _currentTarget = null;

    private Vector3 _initialStoneRotation = Vector3.zero;
    private Rigidbody _body = null;

    private void Start()
    {
        if (_broom == null)
        {
            Debug.LogError("Broom GameObject reference is not set in the inspector.");
        }
        else
        {
            _broom.SetActive(false);
        }

        if(_stone == null)
        {
            Debug.LogError("Stone GameObject reference is not set in the inspector.");
        }

        if (_leftSweepTarget == null)
        {
            Debug.LogError("Left Sweep Target reference is not set in the inspector.");
        }

        if (_rightSweepTarget == null)
        {
            Debug.LogError("Right Sweep Target reference is not set in the inspector.");
        }

        var curlingStone = GetComponent<CurlingStoneController>();

        if(curlingStone != null)
        {
            curlingStone.OnSweepCallback += HandleSweep;
            curlingStone.OnTurnCallback += HandleTurn;
        }
        else
        {
            Debug.LogError("CurlingStoneController component not found on the GameObject.");
        }

        _body = GetComponent<Rigidbody>();

        if (_body == null)
        {
            Debug.LogError("Rigidbody component not found on the GameObject.");
        }
    }

    private void Update()
    {
        MoveBroom();

        if (_initialStoneRotation == Vector3.zero && _stone != null)
        {
            _initialStoneRotation = _stone.transform.rotation.eulerAngles;
        }

        _stone.transform.rotation = Quaternion.Euler(_initialStoneRotation);
    }

    private void MoveBroom()
    {
        if (_currentTarget == null) return;
        if (_broom == null) return;

        _broom.transform.position = Vector3.Lerp(_broom.transform.position, _currentTarget.position, Time.deltaTime * _broomSpeed);
    }

    private void HandleTurn(CurlingStoneController.TurnDirection direction)
    {
        //Debug.Log($"Received turn direction: {direction}");
        _currentTarget = null;

        _broom.SetActive(_currentTarget != null);
    }

    private void HandleSweep(CurlingStoneController.SweepDirection direction)
    {
        Debug.Log($"Received sweep direction: {direction}");
        switch (direction)
        {
            case CurlingStoneController.SweepDirection.None:
                _currentTarget = null;
                break;

            case CurlingStoneController.SweepDirection.Left:
                _currentTarget = _leftSweepTarget;
                break;

            case CurlingStoneController.SweepDirection.Right:
                _currentTarget = _rightSweepTarget;
                break;
        }

        if(_broom == null) return;

        _broom.SetActive(_currentTarget != null);
    }
}
