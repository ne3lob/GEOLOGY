using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;


public class SpawnObject : MonoBehaviour
{
    public GameObject[] objects;
    public Vector3 center;

    public Vector3 size;
    public int objectsCount;
    public int objectslimit =25;
    float timer = 0;
    private float timerEndLevel = 80f;
    public OVROverlay overlay;
    public OVROverlay text;

    // Start is called before the first frame update
    void Start()
    {
        overlay.hidden = true;
        text.hidden = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (objectsCount > objectslimit)
        {
            
            return;
        }

        timer += Time.deltaTime;
        timerEndLevel += Time.deltaTime;
        
        if (timer > 4f)
        {
            SpawnObjects();
            timer = 0f;
            
        }
        else if (timer > timerEndLevel)
        {
            overlay.hidden = false;
            text.hidden = false;
            StartCoroutine(LoadYourAsyncScene());
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
    IEnumerator LoadYourAsyncScene()
    {
        yield return new WaitForSeconds(3);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Scene1");
        

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}