using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class ConfettiTimer : MonoBehaviour
{
    private float timer = 5;

    
    void Start()
    {
        StartCoroutine(DestroySelf());
    }


    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }
}
