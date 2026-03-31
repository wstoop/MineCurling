using System.Diagnostics;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        CurlingStoneController player = collision.collider.GetComponent<CurlingStoneController>();

        if (player == null)
        {
            CurlingStoneController.body.velocity *= 0.5;
        }
    }
}
