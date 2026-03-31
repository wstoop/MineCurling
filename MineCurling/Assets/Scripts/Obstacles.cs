using System.Diagnostics;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<CurlingStoneController>(out var player))
        {
            UnityEngine.Debug.Log(player.Body.linearVelocity);
            player.Body.linearVelocity *= 0.5f;
            UnityEngine.Debug.Log(player.Body.linearVelocity);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        
    }
}
