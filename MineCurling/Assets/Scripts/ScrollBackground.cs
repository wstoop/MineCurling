using UnityEngine;
using UnityEngine.UI;

public class ScrollBackground : MonoBehaviour
{
    public float ScrollX = 0.5f;
    public float ScrollY = 0.5f;

    private void Update()
    {
        float OffsetX = Time.time * ScrollX;
        float OffseY = Time.time * ScrollY;
        
    }
}
