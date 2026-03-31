using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MineBehaviour : MonoBehaviour
{

    [SerializeField]
    private float _force = 1;
    [SerializeField]
    private GameObject _explosionVFX;

    private List<GameObject> players = new List<GameObject>();
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
            if (players.Contains(other.gameObject))
            {
                return;
            }
            players.Add(other.gameObject);
       }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            players.Remove(other.gameObject);
        }
        StartCoroutine(Explode());
    }

    IEnumerator Explode()
    {
        boxCollider.enabled = false;
        sphereCollider.enabled = true;
        yield return new WaitForSeconds(0.05f);
        Debug.Log("Explode start");
        Debug.Log("Applying force to " + players.Count + " players");
        foreach (var player in players)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            player.GetComponent<Rigidbody>().AddForce(direction * _force, ForceMode.Impulse);
        }
        Debug.Log("Explode End");
        Instantiate(_explosionVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
