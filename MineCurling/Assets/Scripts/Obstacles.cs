using System.Diagnostics;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<CurlingStoneController>(out var player))
        {
            player.Body.linearVelocity *= 0.5f;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        
    }
}
