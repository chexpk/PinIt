using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushStickman : MonoBehaviour
{
    [SerializeField] private Rigidbody[] allRigidbodies;
    // [SerializeField] private float pushForce = 10f;
    // [SerializeField] private float torqueForce = 10f;
    
    void Start()
    {
        // SetKinematic(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetKinematic(bool isKinematic)
    {
        foreach (var rb in allRigidbodies)
        {
            rb.isKinematic = isKinematic;
        }
    }


    public void Push(Vector3 directionOfPush, float pushForce)
    {
        SetKinematic(false);
        foreach (var rb in allRigidbodies)
        {
            rb.AddForce(directionOfPush * pushForce, ForceMode.Impulse);
            // rb.AddTorque(new Vector3(0, 0, 90) * torqueForce, ForceMode.Impulse);
        }
    }
}
