using System.Collections;
using UnityEngine;

public class VFXTimer : MonoBehaviour
{
    private float timer = 0;

    private void Awake()
    {
        foreach (var ps in GetComponentsInChildren<ParticleSystem>())
        {
            if(timer < ps.main.duration)
            {
                timer = ps.main.duration;
            }
        }
    }
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
