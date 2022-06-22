using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportToplayerLeft : MonoBehaviour
{
    private GameObject cam;

    private float xPos;
    private float yPos;
    private float zPos;

    [SerializeField] public bool stopMoveToCenter = false;
    public Vector3 desiredPos;

    // Start is called before the first frame update
    void Start()
    {
        xPos = Random.Range(0f, 0.5f);
        yPos = Random.Range(-0.2f, 0.2f);
        zPos = Random.Range(0f, 0.5f);
        cam = GameObject.FindGameObjectWithTag("LeftHand");
        desiredPos = new Vector3(cam.transform.position.x + xPos, cam.transform.position.y + yPos,
            cam.transform.position.z + zPos);
    }

    // Update is called once per frame
    void Update()
    {
        desiredPos = new Vector3(cam.transform.position.x + xPos, cam.transform.position.y + yPos,
            cam.transform.position.z + zPos);
        transform.position = Vector3.Lerp(transform.position, desiredPos, 2 * Time.deltaTime);
    }
}