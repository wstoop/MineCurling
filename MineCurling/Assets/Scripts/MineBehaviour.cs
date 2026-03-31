using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.VFX;

public class MineBehaviour : MonoBehaviour
{

    [SerializeField]
    private float _force = 1;
    [SerializeField]
    private GameObject _explosionVFX;

    private List<Rigidbody> playersRB = new List<Rigidbody>();
    private SphereCollider sphereCollider;
    private BoxCollider boxCollider;


    private void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
        boxCollider = GetComponent<BoxCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
       if(other.CompareTag("Player"))
       {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if(rb != null)
            {
                if (playersRB.Contains(rb))
                {
                    return;
                }
                playersRB.Add(rb);
            }
       }
    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            if (other.CompareTag("Player"))
            {
                playersRB.Remove(rb);
            }
            StartCoroutine(Explode());
        }
    }

    IEnumerator Explode()
    {
        boxCollider.enabled = false;
        sphereCollider.enabled = true;
        yield return new WaitForSeconds(0.05f);
        //Debug.Log("Explode start");
        //Debug.Log("Applying force to " + playersRB.Count + " players");
        foreach (var playerRB in playersRB)
        {
            Vector3 direction = (playerRB.transform.position - transform.position).normalized;
            playerRB.AddForce(direction * _force, ForceMode.Impulse);

        }
        //Debug.Log("Explode End");
        Instantiate(_explosionVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
