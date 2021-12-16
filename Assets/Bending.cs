using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bending : MonoBehaviour
{
    public GameObject oneMat;
    public GameObject secondMat;
    public GameObject thirdMat;
    public GameObject fourthMat;
    public GameObject fifthMat;
    public GameObject sixMat;
    public Animator animBlend;
    public float timeOppacity;

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        ChangingOppacity(oneMat, 0f);
        ChangingOppacity(secondMat, -1f);
        ChangingOppacity(thirdMat, 0f);
        ChangingOppacity(fourthMat, 0.05f);
        ChangingOppacity(fifthMat, 0.05f);
        ChangingOppacity(sixMat, 0f);
    }

    // private float fromValue = 0.2f;
    // private float toValue = 2f;
    private float timeStep = 0f;
    private float val = 0f;

    void ChangingOppacity(GameObject gameObject, float fromValue)
    {
        val = Mathf.Lerp(fromValue, 2f, timeStep);
        gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("Oppacity", val);
        timeStep += Time.deltaTime * timeOppacity;
        if (val >= 2f)
        {
            print("bend");
            // animBlend.SetBool("TriggerBending", true);
        }
    }
}