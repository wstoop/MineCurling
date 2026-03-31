using System;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Collider), typeof(Rigidbody), typeof(PlayerInput))]
public class CurlingStoneController : MonoBehaviour
{
    [SerializeField, Min(1f)]
    private float _sweepForceMultiplier = 1.1f;

    [SerializeField, Min(0.1f)]
    private float _minimumVelocityThreshold = 1f;

    enum SweepDirection { None, Left, Right }

    private Collider _collider = null;
    private Rigidbody _rigidbody = null;
    private PhysicsMaterial _physicsMaterial = null;

    public Rigidbody Body => _rigidbody;

    private float _lastXValue = 0.0f;
    private SweepDirection _lastDirection = SweepDirection.None;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
}
