using System.Diagnostics;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    [SerializeField]
    private GameObject _explosionVFX;

    [SerializeField]
    private float _movementSpeed;
    [SerializeField]
    private float _rotationSpeed;
    [SerializeField]
    private float _lifeTime;

    private bool rotationPositive;

    private float lifetimeTimer;

    private void Update()
    {
        lifetimeTimer += Time.deltaTime;

        if(lifetimeTimer >= _lifeTime)
        {
            Destroy(gameObject);
        }

        transform.position += (transform.forward * _movementSpeed) * Time.deltaTime;
        transform.RotateAround(transform.position, transform.forward, (rotationPositive ? _rotationSpeed : -_rotationSpeed) * Time.deltaTime);

        if ((transform.rotation.z > 0.05) || (transform.rotation.z < -0.05))
        {
            rotationPositive = !rotationPositive;
            transform.rotation = Quaternion.Euler(0, 0, (rotationPositive ? -5 : 5));
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Rigidbody>().linearVelocity *= 0.5f;
            Instantiate(_explosionVFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
