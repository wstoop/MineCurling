using System.Diagnostics;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        CurlingStoneController player = collision.collider.GetComponent<CurlingStoneController>();

        if (player == null)
        {
            player.Body.linearVelocity *= 0.5f;
        }
    }
}
