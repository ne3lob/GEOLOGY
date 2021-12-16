using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Illuminate : MonoBehaviour
{

    [SerializeField] GameObject pointLight;
    Light light;
    // Update is called once per frame

    private void Start()
    {
        light = pointLight.GetComponent<Light>(); ;
        
    }
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
       
            pointLight.SetActive(true);
        
        
    }
    private void OnTriggerExit(Collider other)
    {
        pointLight.SetActive(false);
    }
}
