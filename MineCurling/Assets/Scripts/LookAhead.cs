using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class LookAhead : MonoBehaviour
{
    private Transform _endPoint;

    private CinemachineTargetGroup _targetGroup;

    private List<Transform> _targets = new();

    private void Start()
    {
        _endPoint = GameObject.Find("EndPoint").transform;
        _targetGroup = GetComponent<CinemachineTargetGroup>();
    }

    private void Update()
    {
        _targets.Sort((a, b) =>
        (a.position - _endPoint.position).sqrMagnitude
            .CompareTo((b.position - _endPoint.position).sqrMagnitude));
    }

    private void LateUpdate()
    {
        for(int i = 0; i < _targets.Count; i++)
        {
            int groupIndex = _targetGroup.FindMember(_targets[i]);

            if(i == 0)
            {
                _targetGroup.Targets[groupIndex].Radius = 20;
            }
            else if(i == _targets.Count - 1)
            {
                _targetGroup.Targets[groupIndex].Radius = 15;
            }
            else
            {
                _targetGroup.Targets[groupIndex].Radius = 1;
            }
        }
    }

    public void AssignTarget(Transform target)
    {
        _targets.Add(target);
    }
}
