using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject simulateGo;
    [SerializeField] private Joystick joystick;
    [SerializeField] private TrajectoryRenderer trajectoryRenderer;
    [SerializeField] private GameObject stickPref;
    [SerializeField] private float powerOfShoot = 0.4f;
    private Camera mainCamera;
    private bool isLoaded = false;
    private Vector3 speed = Vector3.zero;


    private Transform selfTransform;
    private int widthScreen;
    
    [SerializeField] private float maxX = 8;
    [SerializeField] private float minX = -8;
    [SerializeField] private float minPower = 0.27f;
    [SerializeField] private float maxPower = 0.8f;
    
    
    [SerializeField] public float speedOfMoveStick = 10f;
    [SerializeField] public float speedOfPowerUp = 0.03f;
    [SerializeField] private float startingPosition;

    private float moveX;
    private float powerY;
    
    // Start is called before the first frame update
    void Start()
    {
        selfTransform = transform;
        widthScreen = Screen.width;
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        JoystickControl();
    }
    
    void JoystickControl()
    {
        moveX = joystick.Horizontal;
        powerY = joystick.Vertical;

        var step =  moveX * speedOfMoveStick * Time.deltaTime;
        var powerStep = powerY * speedOfPowerUp * Time.deltaTime;
        
        // Debug.Log("step " + step);
        if (step + transform.position.x > minX && step + transform.position.x < maxX)
        {
            transform.Translate(new Vector3(step, 0, 0));
        }

        if (powerStep + powerOfShoot > minPower && powerStep + powerOfShoot < maxPower)
        {
            powerOfShoot += powerStep;
        }
        
        

        if (Input.touchCount > 0)
        {
            isLoaded = true;
            // var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            var ray = new Ray(transform.position, transform.forward);
            new Plane(-Vector3.forward, new Vector3(transform.position.x, transform.position.y, transform.position.z + 70)).Raycast(ray, out var enter);
            var mouseInWorldPos = ray.GetPoint(enter);
        
            speed = (mouseInWorldPos - transform.position) * powerOfShoot;
            // transform.rotation = Quaternion.LookRotation(speed);
            
            trajectoryRenderer.ShowTrajectory(transform.position, speed);
        }
        
        if (isLoaded && Input.touchCount < 1)
        {
            isLoaded = false;
            var stickRigidbody = Instantiate(stickPref, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            stickRigidbody.AddForce(speed, ForceMode.VelocityChange);
            trajectoryRenderer.DeleteTrajectory();
        }
    }
}
