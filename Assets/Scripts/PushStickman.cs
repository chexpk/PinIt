using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushStickman : MonoBehaviour
{
    [SerializeField] private Rigidbody allRigidbodie;
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
       
        
            allRigidbodie.isKinematic = isKinematic;
        
    }


    public void Push(Vector3 directionOfPush, float pushForce, float torqueForce)
    {
        SetKinematic(false);

            allRigidbodie.AddForce(directionOfPush * pushForce, ForceMode.Impulse);
            // allRigidbodie.AddTorque(new Vector3(0, 0, 0) * torqueForce, ForceMode.VelocityChange);
        
    }
}
