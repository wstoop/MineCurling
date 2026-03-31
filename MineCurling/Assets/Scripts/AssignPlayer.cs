using UnityEngine;
using Unity.Cinemachine;

public class AssignPlayer : MonoBehaviour
{
    void Start()
    {
        CinemachineTargetGroup targetGroup = GameObject.Find("MainCamera").transform.Find("Target Group").GetComponent<CinemachineTargetGroup>();
        if (targetGroup != null)
        {
            targetGroup.AddMember(this.transform, 1, 10);
        }
    }
}
