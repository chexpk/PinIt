using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyScript : MonoBehaviour
{
    [SerializeField] private float delay = 5f;

    private float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        // Destroy(gameObject, delay);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > delay)
        {
            Destroy(gameObject);
        }
    }
}
