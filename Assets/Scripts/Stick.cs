using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    [SerializeField] private Transform[] pointToStickmanTransforms;
    [SerializeField] private bool[] isPointsFree;
    [SerializeField] private GameObject contactSound;
    [SerializeField] private GameObject destroySound;
    [SerializeField] private float delayToDestroy = 5f;
    private List<GameObject> stikemansOnStick = new List<GameObject>();
    // private List<bool> isPointsFree;
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
        if(isHitSomething) return;
        var stickFeeler =  other.gameObject.GetComponent<StickFeeler>();
        if (stickFeeler != null)
        {
            var stickFollower = stickFeeler.ReturnStickFollower();
            if (!stickFollower.IsFollow())
            {
                var pointTransform = GetFreePoint();
                if(pointTransform == null) return;
                stickFollower.SetTarget(pointTransform);
                CreateContactSoundGO();
                stikemansOnStick.Add(other.transform.root.gameObject);
                // Debug.Log(other.gameObject.GetComponent<DestroyScript>());
                // Destroy(other.gameObject.GetComponent<DestroyScript>());
                DestroyScript destroyScript;
                Debug.Log("IsDestroyScriptExist "+ other.transform.root.gameObject.TryGetComponent(out destroyScript));
                Destroy(destroyScript);
                return;
            }
            return;
        }
        var environment = other.gameObject.GetComponent<Environment>();
        if (environment != null)
        {
            StickIn();
            Debug.Log("stikemansOnStick.Count " + stikemansOnStick.Count);
            if (stikemansOnStick.Count > 0)
            {
                Invoke(nameof(DestroyStickAndStickMans), delayToDestroy);
                return;
            }
            Destroy(gameObject, 3f);
        }

    }

    void StickIn()
    {
        isHitSomething = true;
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    Transform GetFreePoint()
    {
        for (int i = 0; i < isPointsFree.Length; i++)
        {
            if (isPointsFree[i])
            {
                isPointsFree[i] = false;
                return pointToStickmanTransforms[i];
            }
        }
        return null;
    }

    void CreateContactSoundGO()
    {
        Instantiate(contactSound);
    }

    void DestroyStickAndStickMans()
    {
        CreateDestroySoundGO();
        foreach (var stickman in stikemansOnStick)
        {
            if(stickman == null) continue;
            Destroy(stickman);
        }
        Destroy(gameObject);
    }

    void CreateDestroySoundGO()
    {
        Instantiate(destroySound);
    }
}
