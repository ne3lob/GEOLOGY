using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class SpawnObject : MonoBehaviour
{
    public GameObject[] objects;
    public Vector3 center;

    public Vector3 size;
    public int objectsCount;
    public int objectslimit = 10;
    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (objectsCount > objectslimit)
        {
            return;
        }

        timer += Time.deltaTime;
        if (timer > 2f)
        {
            SpawnObjects();
            timer = 0f;
            objectsCount++;
        }
    }

    public void SpawnObjects()
    {
        Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2),
            Random.Range(-size.z / 2, size.z / 2));
        Instantiate(objects[Random.Range(0, objects.Length)], pos, Quaternion.Euler(new Vector3(90, Random.Range(0, 360), 0)));
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.localPosition + center, size);
    }
}