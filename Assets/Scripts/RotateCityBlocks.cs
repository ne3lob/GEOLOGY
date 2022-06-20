using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCityBlocks : MonoBehaviour
{
    [SerializeField] private float rotationMultiplier;

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, transform.up, Time.deltaTime * rotationMultiplier);
    }
}