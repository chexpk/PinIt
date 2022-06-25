using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowStickman : MonoBehaviour
{
    [SerializeField] private Transform[] transformOfThrowPoint;
    [SerializeField] private GameObject stickmanPref;
    [SerializeField] private float pushForce = 10f;
    private float time = 0;
    [SerializeField] private float delayTime = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > delayTime)
        {
            time = 0;
            Throw();
        }
    }

    [ContextMenu("Throw")]
    void Throw()
    {
        foreach (var pointTransform in transformOfThrowPoint)
        {
            var stickman = Instantiate(stickmanPref, pointTransform.position, Quaternion.identity);
            stickman.GetComponent<PushStickman>().Push(pointTransform.forward, pushForce);
        }
    }
}
