using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Illuminate : MonoBehaviour
{

    [SerializeField] GameObject pointLight;
    Light light;
    // Update is called once per frame
    GameObject wall;
    private void Start()
    {
        light = pointLight.GetComponent<Light>(); ;
        
    }
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
       if(other.gameObject.CompareTag("Wall"))
        { 
            pointLight.SetActive(true);
        }


    }
    private void OnTriggerExit(Collider other)
    {
        
            pointLight.SetActive(false);
        
    }
}
