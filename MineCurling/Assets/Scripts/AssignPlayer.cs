using UnityEngine;
using Unity.Cinemachine;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;

public class AssignPlayer : MonoBehaviour
{
    [SerializeField]
    static private List<Material> _colors;
    void Start()
    {
        var targetGroup = GameObject.Find("MainCamera").transform.Find("Target Group");

        CinemachineTargetGroup targetGroupComponent = targetGroup.GetComponent<CinemachineTargetGroup>();

        if (targetGroupComponent != null)
        {
            targetGroupComponent.AddMember(this.transform, 1, 10);
        }

        LookAhead lookAhead = targetGroup.GetComponent<LookAhead>();

        if (lookAhead != null)
        {
            lookAhead.AssignTarget(this.transform);
        }

        SetColor();
    }

    public void SetColor()
    {
        
    }
}
