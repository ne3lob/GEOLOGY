using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadSetVelocity : MonoBehaviour
{
    // Start is called before the first frame update
    public float headsetVelocity;
    Vector3 lastHeadsetPosition;
    public Transform headset;
    public GameObject WireframeObject;

    public void Update()
    {
        headsetVelocity = (headset.position - lastHeadsetPosition).magnitude / Time.deltaTime;
        lastHeadsetPosition = headset.position;
        Debug.Log(headsetVelocity);
        // if (headsetVelocity > 0.7f)
        // {
        //     ChangingWireframe(WireframeObject, 200);
        // }
    }

    private float timeStep = 0f;
    private float val = 0f;
    public float timeWire;

    void ChangingWireframe(GameObject gameObject, float fromValue)
    {
        val = Mathf.Lerp(fromValue, 0f, timeStep);
        gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_MaxTriSize", val);
        timeStep += Time.deltaTime * timeWire;
    }
}