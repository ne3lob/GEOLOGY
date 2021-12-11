using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ParticleRibbonCount : MonoBehaviour
{
    private ParticleSystem ps;
    private ParticleSystem.NoiseModule _noiseModule;

    void Start()
    {
        ps = this.GetComponent<ParticleSystem>();
        _noiseModule = ps.noise;
    }

    public Camera camGameObject;

    private void Update()
    {
        TargetObjectOne(this.transform);
    }

    private void TargetObjectOne(Transform transformTarget)
    {
        TargetPointsOne(new Vector3(), transformTarget, false);
    }

    void TargetPointsOne(Vector3 vector, Transform transformTarget, bool b)
    {
        vector = camGameObject.WorldToViewportPoint(transformTarget.position);
        b = vector.z > 0 && vector.x > 0 && vector.x < 1 && vector.y > 0 && vector.y < 1;


        if (b)
        {
            _noiseModule.positionAmount = 10f;
        }
        else
        {
            _noiseModule.positionAmount = 1f;
        }
    }
}