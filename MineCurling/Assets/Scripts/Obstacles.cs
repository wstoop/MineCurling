using System.Collections;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
using UnityEngine.VFX;

public class Obstacles : MonoBehaviour
{
    [SerializeField]
    private VisualEffect _explosionVFX;

    [SerializeField]
    private float _movementSpeed;
    [SerializeField]
    private float _rotationSpeed;
    [SerializeField]
    private float _lifeTime;
    [SerializeField]
    private bool _hasLifetime;
    [SerializeField]
    private float _jumpSpeed;
    [SerializeField]
    private float _jumpHeight;

    private bool rotationPositive;
    private float lifetimeTimer;
    private float deltaJump;
    private bool isJumping;
    private bool hasJumped;

    const string CALLONHIT = "OnHit";
    private void Update()
    {
        if(_hasLifetime)
        {
            lifetimeTimer += Time.deltaTime;

            if (lifetimeTimer >= _lifeTime)
            {
                Destroy(gameObject);
            }
        }

        transform.position += (transform.forward * _movementSpeed) * Time.deltaTime;
        transform.RotateAround(transform.position, transform.forward, (rotationPositive ? _rotationSpeed : -_rotationSpeed) * Time.deltaTime);

        if ((transform.rotation.z > 0.05) || (transform.rotation.z < -0.05))
        {
            rotationPositive = !rotationPositive;
            transform.rotation = Quaternion.Euler(0, 0, (rotationPositive ? -5 : 5));
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1f))
        {
            if (hit.collider.CompareTag("Wall"))
            {
                isJumping = true;
            }
        }

        if(isJumping)
        {
            if (deltaJump < _jumpHeight && !hasJumped)
            {
                transform.position += (transform.up * _jumpSpeed) * Time.deltaTime;
                deltaJump += _jumpSpeed * Time.deltaTime;
                if(deltaJump >= _jumpHeight)
                {
                    hasJumped = true;
                }
            }
            else if (deltaJump > 0)
            {
                transform.position -= (transform.up * _jumpSpeed) * Time.deltaTime;
                deltaJump -= _jumpSpeed * Time.deltaTime;
            }
        }
        
        if (deltaJump <= 0)
        {
            isJumping = false;
            hasJumped = false;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.CompareTag("Sheet") && hasJumped)
        {
            isJumping = false;
            hasJumped = false;
        }
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Rigidbody>().linearVelocity /= 2;
            if (_explosionVFX != null)
            {
                var temp = Instantiate(_explosionVFX, transform.position, Quaternion.identity);

                if (temp == null)
                {
                    UnityEngine.Debug.Log("null");
                }

            }

            Destroy(gameObject);
        }
    }
}
