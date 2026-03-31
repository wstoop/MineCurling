using System;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Collider), typeof(Rigidbody), typeof(PlayerInput))]
public class CurlingStoneController : MonoBehaviour
{
    private Collider _collider = null;
    private Rigidbody _rigidbody = null;
    private PhysicsMaterial _physicsMaterial = null;

    public Rigidbody Body => _rigidbody;

    //private _last

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
    }

    public void OnSweep(InputAction.CallbackContext context)
    {
        var xValue = context.ReadValue<Vector2>().x;


    }

    public void OnTurn(InputAction.CallbackContext context)
    {
        var inputVector = context.ReadValue<Vector2>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
