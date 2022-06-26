using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeRotation : MonoBehaviour
{
    [SerializeField] private GameObject bodyGO;
    

    void Update()
    {
        bodyGO.transform.localRotation = Quaternion.AngleAxis(90, Vector3.up);
        
    }
}
