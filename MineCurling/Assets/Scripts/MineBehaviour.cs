using Unity.VisualScripting;
using UnityEngine;

public class MineBehaviour : MonoBehaviour
{

    [SerializeField]
    private float _force = 1;
    [SerializeField]
    private float _radius = 6;
    [SerializeField]
    private GameObject _explosionVFX;
    [SerializeField]
    private GameObject _explosionDecal;
    [SerializeField]
    CinemachineShake _shake;
    [SerializeField]
    private float _shakeIntensity;
    [SerializeField]
    private float _shakeDuration;

    [SerializeField]
    private AudioSource _beep;

    private void Awake()
    {
        _shake = GameObject.FindAnyObjectByType<CinemachineShake>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _beep.Play();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Explode();
        }
    }

    private void Explode()
    {
        _beep.Stop();
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radius);
        foreach (var collider in colliders)
        {
            Rigidbody rb;
            if (collider.gameObject.CompareTag("Player") && collider.gameObject.TryGetComponent(out rb))
            {
                Vector3 direction = (rb.transform.position - transform.position).normalized;
                direction.y = 0;
                rb.AddForce(direction * _force, ForceMode.VelocityChange);
            }
        }
        if (_explosionVFX != null)
            Instantiate(_explosionVFX, transform.position, Quaternion.identity);
        if (_explosionDecal != null)
        {
            Instantiate(_explosionDecal, transform.position, Quaternion.identity);
        }
        gameObject.SetActive(false);
        _shake.ShakeCamera(_shakeDuration,_shakeIntensity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
