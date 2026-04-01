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

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Explode();
        }
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radius);
        foreach (var collider in colliders)
        {
            Rigidbody rb;
            if (collider.gameObject.CompareTag("Player") && collider.gameObject.TryGetComponent(out rb))
            {
                Vector3 direction = (rb.transform.position - transform.position).normalized;
                rb.AddForce(direction * _force, ForceMode.Impulse);
            }
        }
        if (_explosionVFX != null)
            Instantiate(_explosionVFX, transform.position, Quaternion.identity);
        if (_explosionDecal != null)
        {
            Instantiate(_explosionDecal, transform.position, Quaternion.identity);
        }
        gameObject.SetActive(false);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
