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
    [SerializeField] GameObject startPositionCamera;
    Vector3 startPosition;

    public Vector3 size;
    public int objectsCount;
    public int objectslimit =25;
    float timer = 0;
    float timerNextLevel = 0;
    public float timerEndLevel = 230f;
    public OVROverlay overlay;
    public OVROverlay text;
    [SerializeField] bool levelChanged = false;

    // Start is called before the first frame update
    void Start()
    {
        overlay.hidden = true;
        text.hidden = true;
        levelChanged = false;
       // startPosition = startPositionCamera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (objectsCount > objectslimit)
        {
            return;
        }

        timer += Time.deltaTime;
        timerNextLevel+= Time.deltaTime;
        
        if (timer > 4f)
        {
            SpawnObjects();
            timer = 0f;
            
        }
        else if (timerNextLevel > timerEndLevel && !levelChanged)
        {
           /* overlay.hidden = false;
            text.hidden = false;*/
            startPositionCamera.SetActive(true);
          //  StartCoroutine(LoadYourAsyncScene()); I moved this script to finishing indicator, so after it is setted active, it will use collider to finish, Baris
            levelChanged = true;
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
    /*  moved this script to finishing indicator, so after it is setted active, it will use collider to finish, Baris
    IEnumerator LoadYourAsyncScene()
    {
        yield return new WaitForSeconds(3);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Scene1");
        
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        levelChanged = false;
    }*/
}