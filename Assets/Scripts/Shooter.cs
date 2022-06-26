using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private TrajectoryRenderer trajectoryRenderer;
    [SerializeField] private GameObject stickPref;
    [SerializeField] private float powerOfShoot = 0.4f;
    private Camera mainCamera;
    private bool isLoaded = false;
    private Vector3 speed = Vector3.zero;


    private Transform selfTransform;
    private int widthScreen;
    
    private float maxX = 8;
    private float minX = -8;
    private float minPower = 0.27f;
    private float maxPower = 0.6f;
    
    
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
        // if (Input.touchCount > 0)
        // {
        //     isLoaded = true;
        //     var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        //     new Plane(-Vector3.forward, new Vector3(transform.position.x, transform.position.y, transform.position.z + 500)).Raycast(ray, out var enter);
        //     var mouseInWorldPos = ray.GetPoint(enter);
        //
        //     speed = (mouseInWorldPos - transform.position) * powerOfShoot;
        //     transform.rotation = Quaternion.LookRotation(speed);
        //     
        //     trajectoryRenderer.ShowTrajectory(transform.position, speed);
        // }
        //
        // if (isLoaded && Input.touchCount < 1)
        // {
        //     isLoaded = false;
        //     var stickRigidbody = Instantiate(stickPref, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        //     stickRigidbody.AddForce(speed, ForceMode.VelocityChange);
        //     trajectoryRenderer.DeleteTrajectory();
        // }
        // Test();
        TestJoystick();
    }

    void Test()
    {
        if (Input.touchCount > 0)
        {
            float xPos;
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startingPosition = touch.position.x;
                    // xPos = Mathf.Lerp(minX, maxX, startingPosition / widthScreen);
                    // selfTransform.position = new Vector3(xPos, selfTransform.position.y, selfTransform.position.z);
                    break;
                case TouchPhase.Moved:
                    float touchPos = touch.position.x/widthScreen;
                    xPos = Mathf.Lerp(minX, maxX, touchPos);
                    var targetPos = new Vector3(xPos, selfTransform.position.y, selfTransform.position.z);
                    selfTransform.position = new Vector3(xPos, selfTransform.position.y, selfTransform.position.z);
                    var step =  speedOfMoveStick * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
                    break;
                case TouchPhase.Ended:
                    Debug.Log("Touch Phase Ended.");
                    break;
                case TouchPhase.Stationary:
                    startingPosition = touch.position.x;
                    break;
            }
        }
    }

    void TestJoystick()
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
