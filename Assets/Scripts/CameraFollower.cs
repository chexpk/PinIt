using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    // [SerializeField] private Transform target;
    [SerializeField] private float moveSpeed = 20;
    // // [SerializeField] private float rotationSpeed;
    //
    // Quaternion startRotation;
    Vector3 offset;
    //
    private void Start()
    {
    offset = transform.position - targetObject.position;
    //     // startRotation = transform.rotation;
    }
    //
    // private void LateUpdate()
    // {
    //     // transform.position = Vector3.Lerp(transform.position, target.position + target.rotation * offset, moveSpeed * Time.fixedDeltaTime);
    //     transform.position = Vector3.Lerp(transform.position, target.position + offset, moveSpeed * Time.fixedDeltaTime);
    //     // transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation * startRotation, rotationSpeed * Time.fixedDeltaTime);
    // }
    
    [SerializeField] private Transform targetObject;
    
    void LateUpdate()
    {
        // transform.position = new Vector3(targetObject.position.x, transform.position.y, transform.position.z);
        // var step =  moveSpeed * Time.deltaTime;
        // transform.position = Vector3.MoveTowards(transform.position, targetObject.position + offset, step);
        transform.position = Vector3.Lerp(transform.position, targetObject.position + offset, moveSpeed * Time.deltaTime);
    }
}
