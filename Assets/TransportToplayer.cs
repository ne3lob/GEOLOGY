using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportToplayer : MonoBehaviour
{
   
    
    private GameObject cam;
   
    public float xPos;
    public Vector3 desiredPos;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        xPos = Random.Range(-4.5f, 4.5f);
        desiredPos = new Vector3(cam.transform.position.x+xPos, cam.transform.position.y, cam.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
        
        transform.position = Vector3.Lerp(transform.position,  desiredPos, 20 * Time.deltaTime);
    }
}

