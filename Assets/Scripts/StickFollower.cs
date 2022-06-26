using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickFollower : MonoBehaviour
{
    private bool isFollow = false;
    private Transform targetTransform;
    private Vector3 correctToPosition;

    void Update()
    {
        if (isFollow)
        {
            if(targetTransform == null) return;
            transform.position = targetTransform.position;
        }
    }

    public void SetTarget(Transform target)
    {
        targetTransform = target;
        isFollow = true;
        correctToPosition = transform.position - new Vector3(targetTransform.position.x, targetTransform.position.y,
            0);
    }

    public bool IsFollow()
    {
        return isFollow;
    }
}
