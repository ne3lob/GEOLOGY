using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CaveTimer: MonoBehaviour
{

    float timer = 0;
    [SerializeField] float timerNextLevel = 0;
    [SerializeField] float timerEndLevel = 230f;
    [SerializeField] float movementDurationBeforeFinishing = 150f;
    public OVROverlay overlay;
    public OVROverlay text;
    public bool movementStarted;
    [SerializeField] bool levelChanged = false;
    [SerializeField] ButtonOpener endingScript;
    
    // Start is called before the first frame update
    void Start()
    {

        overlay.hidden = true;
        text.hidden = true;
        levelChanged = false;
        movementStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(endingScript.erasionEnded)
        {
            timerNextLevel += Time.deltaTime;

            if (timerNextLevel > timerEndLevel && !movementStarted)
            {
                //ActivateMovement();
                movementStarted = true;

                timerNextLevel = 0;
            }
            if (movementStarted)
            {
                if (timerNextLevel > movementDurationBeforeFinishing && !levelChanged)
                {
                    FinishLevel();
                    levelChanged = true;
                }
            }
        }
       
    }
    void FinishLevel()
    {
        overlay.hidden = false;
        text.hidden = false;
        StartCoroutine(LoadYourAsyncScene());
        
    }
    IEnumerator LoadYourAsyncScene()
    {
        yield return new WaitForSeconds(3);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Scene3");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        levelChanged = false;
    }
}
