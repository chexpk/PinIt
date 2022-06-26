using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    [SerializeField] private Transform[] pointToStickmanTransforms;
    [SerializeField] private bool[] isPointsFree;

    private List<GameObject> stikemansOnStick;
    // private List<bool> isPointsFree;
    Rigidbody rb;
    bool isHitSomething = false;
    private Vector3 rbVelocity = Vector3.zero;
    

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        rbVelocity = rb.velocity;
        if(rbVelocity == Vector3.zero) return;
        transform.rotation = Quaternion.LookRotation(rbVelocity);
    }

    void Update()
    {
        RotateByMoveDirection();
    }

    void RotateByMoveDirection()
    {
        if (isHitSomething) return;
        rbVelocity = rb.velocity;
        if(rbVelocity == Vector3.zero) return;
        transform.rotation = Quaternion.LookRotation(rbVelocity);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(isHitSomething) return;
        var stickFeeler =  other.gameObject.GetComponent<StickFeeler>();
        if (stickFeeler != null)
        {
            var stickFollower = stickFeeler.ReturnStickFollower();
            if (!stickFollower.IsFollow())
            {
                var pointTransform = GetFreePoint();
                if(pointTransform == null) return;
                stickFollower.SetTarget(pointTransform);
                return;
            }
            return;
        }
        var environment = other.gameObject.GetComponent<Environment>();
        if (environment != null)
        {
            StickIn();
            Destroy(gameObject, 5f);
        }

    }

    void StickIn()
    {
        isHitSomething = true;
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    Transform GetFreePoint()
    {
        for (int i = 0; i < isPointsFree.Length; i++)
        {
            if (isPointsFree[i])
            {
                isPointsFree[i] = false;
                return pointToStickmanTransforms[i];
            }
        }
        return null;
    }
}
