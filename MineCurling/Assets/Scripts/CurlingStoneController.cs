using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Collider), typeof(Rigidbody), typeof(PlayerInput))]
public class CurlingStoneController : MonoBehaviour
{
    [Header("Sweep Params")]
    [SerializeField, Min(1f)]
    private float _sweepForceMultiplier = 1.1f;

    [SerializeField, Min(0.1f)]
    private float _minimumVelocityThreshold = 1f;

    [Header("Turning Params")]
    [SerializeField]
    private float _addedAngularVelocity = 1f;

    enum SweepDirection { None, Left, Right }
    enum TurnDirection { None, CW, CCW };

    private Collider _collider = null;
    private Rigidbody _rigidbody = null;
    private PhysicsMaterial _physicsMaterial = null;


    private float _lastXValue = 0.0f;
    private float _currentAngle = 0.0f;
    private Vector2 _lastTurnInput = Vector2.zero;
    private SweepDirection _lastDirection = SweepDirection.None;

    public Rigidbody Body => _rigidbody;

    void Start()
    {
        _physicsMaterial = new PhysicsMaterial
        {
            name = "CurlingStonePhysicsMaterial",
            dynamicFriction = 0.6f,
            staticFriction = 0.6f,
            bounciness = 0.0f,
            frictionCombine = PhysicsMaterialCombine.Minimum,
            bounceCombine = PhysicsMaterialCombine.Minimum
        };

        _collider = GetComponent<Collider>();

        if (_collider != null)
        {
            _collider.material = _physicsMaterial;
        }

        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.AddForce(Vector3.forward * _minimumVelocityThreshold, ForceMode.Impulse);
    }

    private void Update()
    {
        if (_rigidbody.linearVelocity.sqrMagnitude < _minimumVelocityThreshold)
        {
            var dir = new Vector3(_rigidbody.linearVelocity.x, 0f, _rigidbody.linearVelocity.z).normalized;
            _rigidbody.linearVelocity = dir * _minimumVelocityThreshold;
        }


        _rigidbody.linearVelocity = _rigidbody.linearVelocity.magnitude * _rigidbody.transform.forward.normalized;
    }

    public void OnSweep(InputAction.CallbackContext context)
    {
        var xValue = context.ReadValue<Vector2>().x;

        if (_lastXValue < xValue)       // to the right
        {
            Sweep(SweepDirection.Right);
        }
        else if (_lastXValue > xValue)  // to the left
        {
            Sweep(SweepDirection.Left);
        }
    }

    public void OnTurn(InputAction.CallbackContext context)
    {
        var inputVector = context.ReadValue<Vector2>();

        float angle = Vector2.SignedAngle(_lastTurnInput, inputVector);

        if (inputVector.sqrMagnitude <= 0)
        {
            _lastTurnInput = Vector2.zero;
            return;
        }

        if (_lastTurnInput == Vector2.zero)
        {
            _lastTurnInput = inputVector;
        }

        if (MathF.Sign(angle) != MathF.Sign(_currentAngle) && _currentAngle != 0f)
        {
            _currentAngle = 0f;
            return;
        }

        if (MathF.Abs(angle) < 10f) return;

        if (MathF.Abs(angle) > 45f)
        {
            _lastTurnInput = inputVector;
            return;
        }

        _currentAngle += angle;

        if (MathF.Abs(_currentAngle) >= 360f)
        {
            _currentAngle = 0f;

            if (angle < 0f)
            {
                Turn(TurnDirection.CW);
            }
            else if (angle > 0f)
            {
                Turn(TurnDirection.CCW);
            }

            Debug.Log("Full rotation completed!");
        }

        _lastTurnInput = inputVector;

    }

    private void Sweep(SweepDirection direction)
    {
        if (_lastDirection == direction) return;

        _rigidbody.linearVelocity *= _sweepForceMultiplier;
        _lastDirection = direction;

        // maybe for player feedback, but not for physics calculations,
        // as the physics material should be doing that for us
        switch (direction)
        {
            case SweepDirection.None:
                break;

            case SweepDirection.Left:
                break;

            case SweepDirection.Right:
                break;
        }
    }

    private void Turn(TurnDirection direction)
    {
        switch (direction)
        {
            case TurnDirection.CW:
                Debug.Log("Turning Clockwise");

                _rigidbody.AddTorque(Vector3.up * _addedAngularVelocity, ForceMode.Force);
                break;
            case TurnDirection.CCW:
                Debug.Log("Turning Counter-Clockwise");
                _rigidbody.AddTorque(Vector3.down * _addedAngularVelocity, ForceMode.Force);
                break;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward);

        if (_rigidbody != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + _rigidbody.linearVelocity.normalized);
        }
    }
}
