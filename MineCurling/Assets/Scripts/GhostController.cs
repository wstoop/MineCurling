using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GhostController : MonoBehaviour
{
    private Collider _collider = null;
    private Rigidbody _rigidbody = null;
    private PhysicsMaterial _physicsMaterial = null;
    private float _slowdownFactor = 0.75f;


    public Rigidbody Body => _rigidbody;

    void Start()
    {
        _physicsMaterial = new PhysicsMaterial
        {
            name = "CurlingStonePhysicsMaterial",
            dynamicFriction = 0.6f,
            staticFriction = 0.6f,
            bounciness = 1.0f,
            frictionCombine = PhysicsMaterialCombine.Minimum,
            bounceCombine = PhysicsMaterialCombine.Multiply
        };

        _collider = GetComponent<BoxCollider>();

        if (_collider != null)
        {
            _collider.material = _physicsMaterial;
        }

        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 noGrav = new Vector3(_rigidbody.linearVelocity.x, 0f, _rigidbody.linearVelocity.z);

        var newVelocity = noGrav.magnitude * _rigidbody.transform.forward;

        _rigidbody.linearVelocity = new Vector3(newVelocity.x, _rigidbody.linearVelocity.y, newVelocity.z);
    }

    private void Update()
    {
        _rigidbody.linearVelocity *= Mathf.Pow(0.9f, Time.deltaTime);
        _rigidbody.angularVelocity *= Mathf.Pow(_slowdownFactor, Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Sheet")) return;

        _rigidbody.angularVelocity = new Vector3(0f, _rigidbody.angularVelocity.y, 0f);

        var dir = Vector3.Reflect(transform.forward, collision.contacts[0].normal);

        dir.y = 0f;
        dir.Normalize();

        transform.rotation = Quaternion.LookRotation(dir, Vector3.up);

        //_rigidbody.angularVelocity *= 0.75f;
        _rigidbody.angularVelocity = Vector3.zero;

        gameObject.transform.position += collision.contacts[0].normal * 0.05f;
    }
}
