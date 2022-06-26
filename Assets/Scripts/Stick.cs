using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
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

        var stickFeeler =  other.gameObject.GetComponent<StickFeeler>();
        if (stickFeeler != null)
        {
            // Debug.Log("stickFeeler is detected");
            var stickFollower = stickFeeler.ReturnStickFollower();
            if (!stickFollower.IsFollow())
            {
                // Debug.Log("Stick ask man to follow");
                stickFollower.SetTarget(transform);
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
        // StickIn();
        // Destroy(gameObject, 5f);
    }


    void StickIn()
    {
        isHitSomething = true;
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }
}
