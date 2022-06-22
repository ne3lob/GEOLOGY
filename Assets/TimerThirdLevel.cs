using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerThirdLevel : MonoBehaviour
{

    float timer = 0;
    float timerNextLevel = 0;
    [SerializeField] float timerEndLevel = 230f;
    public OVROverlay overlay;
    public OVROverlay text;
    [SerializeField] bool levelChanged = false;

    void Start()
    {
       
            overlay.hidden = true;
            text.hidden = true;
        levelChanged = false;

    }

    void Update()
    {
        timerNextLevel += Time.deltaTime;
        if(timerNextLevel > timerEndLevel && !levelChanged)
        {
            overlay.hidden = false;
            text.hidden = false;
            StartCoroutine(LoadYourAsyncScene());
            levelChanged = true;
        }
    }


    IEnumerator LoadYourAsyncScene()
    {
        yield return new WaitForSeconds(3);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Start");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        levelChanged = false;
    }
}
