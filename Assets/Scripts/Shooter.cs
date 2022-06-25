using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject stick;
    [SerializeField] private float powerOfShoot = 100;
    private Camera mainCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            new Plane(Vector3.forward, new Vector3(transform.position.x, transform.position.y, transform.position.z + 10)).Raycast(ray, out var enter);
            var mouseInWorldPos = ray.GetPoint(enter);

            var speed = (mouseInWorldPos - transform.position) * powerOfShoot;
            transform.rotation = Quaternion.LookRotation(speed);
        }

        if (Input.GetMouseButtonUp(0))
        {
            
        }
    }
}
