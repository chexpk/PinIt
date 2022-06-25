using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject stickPref;
    [SerializeField] private float powerOfShoot = 100;
    private Camera mainCamera;
    private bool isLoaded = false;
    private Vector3 speed = Vector3.zero;
    
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
            isLoaded = true;
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            new Plane(-Vector3.forward, new Vector3(transform.position.x, transform.position.y, transform.position.z + 500)).Raycast(ray, out var enter);
            var mouseInWorldPos = ray.GetPoint(enter);

            speed = (mouseInWorldPos - transform.position) * powerOfShoot;
            transform.rotation = Quaternion.LookRotation(speed);
        }

        if (isLoaded && Input.touchCount < 1)
        {
            isLoaded = false;
            var stickRigidbody = Instantiate(stickPref, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            stickRigidbody.AddForce(speed, ForceMode.Impulse);
            
        }
        
    }
}
