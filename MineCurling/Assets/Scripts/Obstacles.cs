using System.Diagnostics;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    [SerializeField]
    private GameObject _explosionVFX;

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
