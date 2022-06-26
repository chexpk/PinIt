using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickFollower : MonoBehaviour
{
    private bool isFollow = false;
    private Transform targetTransform;

    void Update()
    {
        if (isFollow)
        {
            transform.position = targetTransform.position;
        }
    }

    public void SetTarget(Transform target)
    {
        // Debug.Log("!!!!!!!!!!! follow");
        targetTransform = target;
        isFollow = true;
    }

    public bool IsFollow()
    {
        return isFollow;
    }
}
