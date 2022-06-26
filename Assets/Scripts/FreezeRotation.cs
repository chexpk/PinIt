using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeRotation : MonoBehaviour
{
    [SerializeField] private GameObject bodyGO;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // bodyGO.transform.rotation = Quaternion.Euler(new Vector3(bodyGO.transform.rotation.x, 90, bodyGO.transform.rotation.z));
        bodyGO.transform.rotation = Quaternion.AngleAxis(90, Vector3.up);
    }
}
