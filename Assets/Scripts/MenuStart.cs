using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuStart : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject NextLevel;
    public bool go = false;
    public OVROverlay overlay;
    public OVROverlay text;
    


    private void Start()
    {
        overlay.hidden = true;
        text.hidden = true;
    }

    void Update()
    {
        // Press the space key to start coroutine
        if (go == true)
        {
            // Use a coroutine to load the Scene in the background
            StartCoroutine(LoadYourAsyncScene());
        }
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

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Open")
        {
            overlay.hidden = false;
            text.hidden = false;
            NextLevel.SetActive(true);
            go = true;
            other.gameObject.SetActive(false);
            
        }
    }
}