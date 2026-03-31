using UnityEngine;


[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class CurlingStoneController : MonoBehaviour
{
    private Collider _collider = null;
    private Rigidbody _rigidbody = null;
    private PhysicsMaterial _physicsMaterial = null;

    public Rigidbody Body => _rigidbody;

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

    // Update is called once per frame
    void Update()
    {
        
    }


}
