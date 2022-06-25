using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    Rigidbody rb;
    bool isHitSomething = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        transform.rotation = Quaternion.LookRotation(rb.velocity);
    }

    void Update()
    {
        RotateByMoveDirection();
    }

    void RotateByMoveDirection()
    {
        if (isHitSomething) return;
        transform.rotation = Quaternion.LookRotation(rb.velocity);
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject);
        StickIn();
        Destroy(gameObject, 5f);
    }
    

    void StickIn()
    {
        isHitSomething = true;
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }
}
