using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportToplayer : MonoBehaviour
{
    private GameObject cam;

    public float xPos;
    public float yPos;
    public float zPos;

    [SerializeField] public bool stopMoveToCenter = false;


    public Vector3 desiredPos;

    // Start is called before the first frame update
    void Start()
    {
        xPos = Random.Range(0f, 0.5f);
        yPos = Random.Range(1f, 1.5f);
        zPos = Random.Range(0f, 0.5f);
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        desiredPos = new Vector3(cam.transform.position.x + xPos, cam.transform.position.y + yPos,
            cam.transform.position.z + zPos);
      
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, desiredPos, 2 * Time.deltaTime);
    }
}