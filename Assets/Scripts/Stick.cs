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

    void OnCollisionEnter(Collision other)
    {
        // Debug.Log(other.gameObject);
        StickIn();
        Destroy(gameObject, 5f);
    }
    

    void StickIn()
    {
        isHitSomething = true;
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }
}
