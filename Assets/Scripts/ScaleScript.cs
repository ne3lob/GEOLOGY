using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScaleScript : MonoBehaviour
{
    private Transform targetPointOne;


    public Camera camGameObject;
    private GameObject o;

    private float timer = 0.0f;
    private int seconds;
    public bool _goToPlayer=false;


    void Start()
    {
        o = gameObject;
        targetPointOne = o.transform;
    }

    void Update()
    {
        TargetObjectOne(targetPointOne);
    }

    private void TargetObjectOne(Transform transformTarget)
    {
        TargetPointsOne(new Vector3(), transformTarget, false);
    }

    [SerializeField] public float speedGrowing = 0.1f;
    [SerializeField] public float maxGrowth = 3f;
    [SerializeField] public float minGrowth = 1f;


    void TargetPointsOne(Vector3 vector, Transform transformTarget, bool b)
    {
        vector = camGameObject.WorldToViewportPoint(transformTarget.position);
        b = vector.z > 0 && vector.x > 0 && vector.x < 1 && vector.y > 0 && vector.y < 1;


        if (b)
        {
            o.transform.localScale = Vector3.Lerp(o.transform.localScale, new Vector3(maxGrowth, maxGrowth, maxGrowth), speedGrowing * Time.deltaTime);
            timer += Time.deltaTime;
            seconds = (int)(timer % 60f);


            if (timer > 5f)
            {
                _goToPlayer = true;
                timer = 0;
                Debug.Log("BUM");
            }
        }
        else
        {
            timer = 0;
            o.transform.localScale = Vector3.Lerp(o.transform.localScale, new Vector3(minGrowth, minGrowth, minGrowth), speedGrowing * Time.deltaTime);
        }
    }
}